using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Wit.SDK.Modular.Sensor.Modular.DataProcessor.Constant;
using Wit.SDK.Modular.WitSensorApi.Modular.BWT901BLE;
using Wit.SDK.Device.Device.Device.DKey;
using Wit.Bluetooth.WinBlue.Utils;
using Wit.Bluetooth.WinBlue.Interface;
using Syncfusion.Windows.Forms.Chart;
using log4net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;
using System.Drawing;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;
using KBCsv;
using Syncfusion.Windows.Forms.Chart.SvgBase;
using System.Reflection;
using Wit.SDK.Sensor.Device.Enum;
using System.Xml.Serialization;
using System.Data;

namespace Wit.Example_BWT901BLE
{   
    /// <summary>
    /// 程序主窗口
    /// 说明：
    /// 1.本程序是维特智能开发的BWT901BLE九轴传感器示例程序
    /// 2.适用示例程序前请咨询技术支持,询问本示例程序是否支持您的传感器
    /// 3.使用前请了解传感器的通信协议
    /// 4.本程序只有一个窗口,所有逻辑都在这里
    /// 
    /// Program Main Window
    /// Explanation:
    /// 1. This program is an example program for the BWT901BLE nine axis sensor developed by Weite Intelligence
    /// 2. Before applying the sample program, please consult technical support and ask if this sample program supports your sensor
    /// 3. Please understand the communication protocol of the sensor before use
    /// 4. This program only has one window, all logic is here
    /// </summary>
    public partial class Form1 : Form
    {
        private const string AccelerationXSerieName = "Acceleration X";
        private const string AccelerationYSerieName = "Acceleration Y";
        private const string AccelerationZSerieName = "Acceleration Z";

        private const string AngleXSerieName = "Angle X";
        private const string AngleYSerieName = "Angle Y";
        private const string AngleZSerieName = "Angle Z";

        private bool doRecord = false;

        private string RootRecordPath => RecordDataSettings.RootRecordDirectoryPath;
        private string currentTimeFolderPath;

        private Log4netTraceListener myListener;
        private readonly ILog logs = LogManager.GetLogger(typeof(Form1));
        private readonly Stopwatch stopwatch = new Stopwatch();
        private RecordDataSettings recordDataSettings;
        private IEnumerable<KeyValuePair<string, DataTable>> sensorDataTable = new List<KeyValuePair<string, DataTable>>();


        /// <summary>
        /// 蓝牙管理器
        /// Bluetooth manager
        /// </summary>
        private IWinBlueManager WitBluetoothManager = WinBlueFactory.GetInstance();

        /// <summary>
        /// 找到的设备
        /// Found device
        /// </summary>
        private Dictionary<string, Bwt901ble> FoundDeviceDict = new Dictionary<string, Bwt901ble>();

        /// <summary>
        /// 控制自动刷新数据线程是否工作
        /// Control whether the automatic refresh data thread works
        /// </summary>
        public bool EnableRefreshDataTh { get; private set; }

        /// <summary>
        /// 构造
        /// Structure
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            myListener = new Log4netTraceListener(logs);
            Trace.Listeners.Add(myListener);
            this.record_btn.Enabled = false;
            XmlSerializer serializer = new XmlSerializer(typeof(RecordDataSettings));

            using (Stream reader = new FileStream(RecordDataSettings.RecordSettingsFilePath, FileMode.Open))
            {
                this.recordDataSettings = (RecordDataSettings)serializer.Deserialize(reader);
            }

        }

        /// <summary>
        /// 窗体加载时
        /// When the form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 开启数据刷新线程
            // Enable data refresh thread
            Thread thread = new Thread(RefreshDataTh);
            thread.IsBackground = true;
            EnableRefreshDataTh = true;
            thread.Start();
        }

        /// <summary>
        /// 窗体关闭时
        /// When the form is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 关闭刷新数据线程
            // Close refresh data thread
            EnableRefreshDataTh = false;
            // 关闭蓝牙搜索
            // Turn off Bluetooth search
            stopScanButton_Click(null, null);
            Process.GetCurrentProcess().Kill();
        }

        /// <summary>
        /// 开始搜索
        /// Starting the Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startScanButton_Click(object sender, EventArgs e)
        {
            // 清除找到的设备
            // Clear found devices
            FoundDeviceDict.Clear();

            // 关闭之前打开的设备
            // Close previously opened devices
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;
                bWT901BLE.Close();
            }

            WitBluetoothManager.OnDeviceFound += this.WitBluetoothManager_OnDeviceFound;
            WitBluetoothManager.StartScan();
        }

        /// <summary>
        /// 当搜索到蓝牙设备时会回调这个方法
        /// Call back this method when Bluetooth devices are found
        /// </summary>
        /// <param name="mac"></param>
        /// <param name="deviceName"></param>
        private void WitBluetoothManager_OnDeviceFound(string mac, string deviceName)
        {
            // 名称过滤
            // Name filtering
            if (deviceName != null && deviceName.Contains("WT"))
            {
                if (!FoundDeviceDict.ContainsKey(mac))
                {
                    Bwt901ble bWT901BLE = new Bwt901ble(mac,deviceName);
                    FoundDeviceDict.Add(mac, bWT901BLE);

                    if (this.InvokeRequired)
                    {
                        Invoke((MethodInvoker)delegate ()
                        {
                            SensorMainData device = new SensorMainData();
                            device.SensorName = bWT901BLE.GetDeviceName();
                            device.bwt901Ble = bWT901BLE;
                            this.tabMain.Controls.Add(device);
                            this.record_btn.Enabled = true;
                        });
                    }
                    
                    // 打开这个设备
                    // Open this device
                    bWT901BLE.Open();
                    bWT901BLE.OnRecord += BWT901BLE_OnRecord;
                }
            }
        }

        /// <summary>
        /// 当传感器数据刷新时会调用这里，您可以在这里记录数据
        /// This will be called when the sensor data is refreshed, where you can record the data
        /// </summary>
        /// <param name="BWT901BLE"></param>
        private void BWT901BLE_OnRecord(Bwt901ble BWT901BLE)
        {
            if (this.InvokeRequired)
            {
                Invoke((MethodInvoker)delegate ()
                {
                    // Apply sensor angle of the mainTab
                    if (this.tabMain.Controls[0] is SensorMainData mainData)
                    {
                        var angleZ = BWT901BLE.GetDeviceData(WitSensorKey.AngleZ);
                        mainData.AngleX = BWT901BLE.GetDeviceData(WitSensorKey.AngleX).ToString() + "°";
                        mainData.AngleY = BWT901BLE.GetDeviceData(WitSensorKey.AngleY).ToString() + "°";
                        mainData.AngleZ = angleZ.ToString() + "°";

                        // Update compass start angle with the z angle. 
                        if (angleZ.HasValue)
                        {
                            mainData.SetCompassStartAngle(angleZ.Value);                            
                        }

                    }

                    // Update data of the acceleration chart 
                    var sensorTime = BWT901BLE.GetDeviceData(WitSensorKey.ChipTime);

                    var seriesX = GetSeriesByName(this.AccelerationChart.Series, AccelerationXSerieName);
                    if (seriesX != null && BWT901BLE.GetDeviceData(WitSensorKey.AccX).HasValue)
                    {
                        seriesX.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AccX).Value);
                    }

                    var seriesY = GetSeriesByName(this.AccelerationChart.Series, AccelerationYSerieName);
                    if (seriesY != null && BWT901BLE.GetDeviceData(WitSensorKey.AccY).HasValue)
                    {
                        seriesY.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AccY).Value);
                    }

                    var seriesZ = GetSeriesByName(this.AccelerationChart.Series, AccelerationZSerieName);
                    if (seriesZ != null && BWT901BLE.GetDeviceData(WitSensorKey.AccZ).HasValue)
                    {
                        seriesZ.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AccZ).Value);
                    }

                    // Update data of the angle Chart
                    var serieAngleX = GetSeriesByName(this.angleChartControl.Series, AngleXSerieName);
                    if (serieAngleX != null && BWT901BLE.GetDeviceData(WitSensorKey.AngleX).HasValue)
                    {
                        serieAngleX.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AngleX).Value);
                    }

                    var serieAngleY = GetSeriesByName(this.angleChartControl.Series, AngleYSerieName);
                    if (serieAngleY != null && BWT901BLE.GetDeviceData(WitSensorKey.AngleY).HasValue)
                    {
                        serieAngleY.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AngleY).Value);
                    }

                    var serieAngleZ = GetSeriesByName(this.angleChartControl.Series, AngleZSerieName);
                    if (serieAngleZ != null && BWT901BLE.GetDeviceData(WitSensorKey.AngleZ).HasValue)
                    {
                        serieAngleZ.Points.Add(sensorTime, BWT901BLE.GetDeviceData(WitSensorKey.AngleZ).Value);
                    }
                });
            }
            myListener.Flush();
        }

        /// <summary>
        /// 停止搜索
        /// stop searching
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopScanButton_Click(object sender, EventArgs e)
        {
            // 让蓝牙管理器停止搜索
            // Stop Bluetooth Manager Search
            WitBluetoothManager.StopScan();
        }

        /// <summary>
        /// 设备状态发生时会调这个方法
        /// This method will be called when the device status occurs
        /// </summary>
        /// <param name="macAddr"></param>
        /// <param name="mType"></param>
        /// <param name="sMsg"></param>
        private void OnDeviceStatu(string macAddr, int mType, string sMsg)
        {
            if (mType == 20)
            {
                // 断开连接
                // Disconnect
                Debug.WriteLine(macAddr + "Disconnect");
            }

            if (mType == 11)
            {
                // 连接失败
                // Connect failed
                Debug.WriteLine(macAddr + "Connect failed");
            }

            if (mType == 10)
            {
                // 连接成功
                // Successfully connected
                Debug.WriteLine(macAddr + "Successfully connected");
            }
        }

        /// <summary>
        /// 刷新数据线程
        /// Refresh Data Thread
        /// </summary>
        private void RefreshDataTh()
        {
            while (EnableRefreshDataTh)
            {
                // 多设备的展示数据
                // Display data for multiple devices
                string DeviceData = "";
                string rawData = "";
                Thread.Sleep(100);
                // 刷新所有连接设备的数据
                // Refresh data for all connected devices
                for (int i = 0; i < FoundDeviceDict.Count; i++)
                {
                    var keyValue = FoundDeviceDict.ElementAt(i);
                    Bwt901ble bWT901BLE = keyValue.Value;
                    if (bWT901BLE.IsOpen())
                    {
                        DeviceData += GetDeviceData(bWT901BLE) + "\r\n";

                        // Get the raw data Hex send and receive
                        rawData += bWT901BLE.GetDeviceData("RawData");
                    }
                }
                dataRichTextBox.Invoke(new Action(() =>
                {
                    dataRichTextBox.Text = DeviceData;
                }));

                // write the raw Data in the richTextBox
                richTextBoxRawData.Invoke(new Action(() =>
                {
                    richTextBoxRawData.AppendText(rawData);
                }));
            }
        }

        /// <summary>
        /// 获得设备的数据
        /// Obtaining device data
        /// </summary>
        private string GetDeviceData(Bwt901ble BWT901BLE)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(BWT901BLE.GetDeviceName()).Append("\n");
            // 加速度
            // Acc
            builder.Append("AccX").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AccX)).Append("g \t");
            builder.Append("AccY").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AccY)).Append("g \t");
            builder.Append("AccZ").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AccZ)).Append("g \n");
            // 角速度
            // Gyro
            builder.Append("GyroX").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AsX)).Append("°/s \t");
            builder.Append("GyroY").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AsY)).Append("°/s \t");
            builder.Append("GyroZ").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AsZ)).Append("°/s \n");
            // 角度
            // Angle
            builder.Append("AngleX").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AngleX)).Append("° \t");
            builder.Append("AngleY").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AngleY)).Append("° \t");
            builder.Append("AngleZ").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.AngleZ)).Append("° \n");
            // 磁场
            // Mag
            builder.Append("MagX").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.HX)).Append("uT \t");
            builder.Append("MagY").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.HY)).Append("uT \t");
            builder.Append("MagZ").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.HZ)).Append("uT \n");
            // 版本号
            // VersionNumber
            builder.Append("VersionNumber").Append(":").Append(BWT901BLE.GetDeviceData(WitSensorKey.VersionNumber)).Append("\n");
            return builder.ToString();
        }

        /// <summary>
        /// 加计校准
        /// Acceleration calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void appliedCalibrationButton_Click(object sender, EventArgs e)
        {
            // 所有连接的蓝牙设备都加计校准
            // All connected Bluetooth devices are calibrated
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }

                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.AppliedCalibration();

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x01, 0x01, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        /// <summary>
        /// 读取03寄存器
        /// Read 03 register
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void readReg03Button_Click(object sender, EventArgs e)
        {
            string reg03Value = "";
            // 读取所有连接的蓝牙设备的03寄存器
            // Read the 03 register of all connected Bluetooth devices
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 等待时长
                    // Waiting time
                    int waitTime = 3000;
                    // 发送读取命令，并且等待传感器返回数据，如果没读上来可以将 waitTime 延长，或者多读几次
                    // Send a read command and wait for the sensor to return data. If it is not read, the waitTime can be extended or read several more times
                    bWT901BLE.SendReadReg(0x03, waitTime);

                    // 下面这行和上面等价推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x27, 0x03, 0x00 }, waitTime);

                    // 拿到所有连接的蓝牙设备的值
                    // Get the values of all connected Bluetooth devices
                    reg03Value += bWT901BLE.GetDeviceName() + "的寄存器03值为 :" + bWT901BLE.GetDeviceData(new ShortKey("03")) + "\r\n";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            MessageBox.Show(reg03Value);
        }

        /// <summary>
        /// 设置回传速率10Hz
        /// Set the return rate to 10Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnRate10_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.SetReturnRate(0x06);

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x03, 0x06, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 设置回传速率50Hz
        /// Set the return rate to 50Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void returnRate50_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.SetReturnRate(0x08);

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x03, 0x08, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 设置带宽20Hz
        /// Set bandwidth of 20Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandWidth20_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.SetBandWidth(0x04);

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x1F, 0x04, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 设置带宽256Hz
        /// Set bandwidth of 256Hz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bandWidth256_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.SetBandWidth(0x00);

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x1F, 0x00, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 开始磁场校准
        /// Start magnetic field calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startFieldCalibrationButton_Click(object sender, EventArgs e)
        {
            // 开始所有连接的蓝牙设备的磁场校准
            // Start magnetic field calibration for all connected Bluetooth devices
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.StartFieldCalibration();

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x01, 0x07, 0x00 });
                    MessageBox.Show("开始磁场校准,请绕传感器XYZ三轴各转一圈,转完以后点击【结束磁场校准】");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// 结束磁场校准
        /// End magnetic field calibration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void endFieldCalibrationButton_Click(object sender, EventArgs e)
        {

            // 结束所有连接的蓝牙设备的磁场校准
            // End the magnetic field calibration of all connected Bluetooth devices
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }
                try
                {
                    // 解锁寄存器并发送命令
                    // Unlock register and send command
                    bWT901BLE.UnlockReg();
                    bWT901BLE.EndFieldCalibration();

                    // 下面两行与上面等价,推荐使用上面的
                    // The following two lines are equivalent to the above, and it is recommended to use the above one
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x69, 0x88, 0xb5 });
                    //bWT901BLE.SendProtocolData(new byte[] { 0xff, 0xaa, 0x01, 0x00, 0x00 });
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private ChartSeries GetSeriesByName(ChartSeriesCollection series, string name)
        {
            for (var i = 0; i < series.Count; i++)
            {
                if (series[i].Name == name)
                {
                    return series[i];
                }
            }
            return null;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.stopwatch.IsRunning)
            {
                var timerString = string.Empty;
                var time = this.stopwatch.Elapsed;
                if (time.Days > 0)
                {
                    timerString += $"{time.Days} days and ";
                }
                if (time.Hours > 0)
                {
                    timerString += $"{time.Hours} hours and ";
                }
                if (time.Minutes > 0)
                {
                    timerString += $"{time.Minutes} minutes and ";
                }
                if (time.Seconds > 0)
                {
                    timerString += $"{time.Seconds} seconds";
                }

                Regex regex = new Regex("(\\s+(and)\\s*)$");

                timerString = regex.Replace(timerString, "");

                this.recordTimer_lbl.Text = timerString;
            }
        }

        private void record_btn_Click(object sender, EventArgs e)
        {
            this.doRecord = !this.doRecord;
            if (this.doRecord)
            {
                this.timer1.Interval = 1000;
                this.timer1.Start();
                this.stopwatch.Restart();
                this.timer1.Tick += timer1_Tick;

                // Update recording button
                this.record_btn.Text = "Stop Recording";
                this.record_btn.BackColor = Color.Red;


                StartRecording();
            }
            else
            {
                this.timer1.Stop();
                this.recordFrequencyTimer.Stop();
                StopRecording();

                if (MessageBox.Show("Do you want to open the record folder?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        Process.Start(this.currentTimeFolderPath);
                    }
                    catch (Exception ex)
                    {

                    }
                }

                // Update recording button
                this.record_btn.Text = "Start Recording";
                this.record_btn.BackColor = Color.Chartreuse;

                this.recordTimer_lbl.Text = string.Empty;
            }

        }

        private void openRecordFolder_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Directory.Exists(this.RootRecordPath))
                {
                    Directory.CreateDirectory(this.RootRecordPath);
                }
                Process.Start(this.RootRecordPath);
            }
            catch (Win32Exception win32Exception)
            {
                //The system cannot find the file specified...
                Console.WriteLine(win32Exception.Message);
            }
        }

        private void StartRecording()
        {
            var dateFolderPath = Path.Combine(this.RootRecordPath, DateTime.Now.ToString("yyyy-MM-dd"));
            currentTimeFolderPath = Path.Combine(dateFolderPath, DateTime.Now.ToString("HH-mm-ss-fff"));

            if (!Directory.Exists(currentTimeFolderPath))
            {
                Directory.CreateDirectory(currentTimeFolderPath);
            }

            // Deserialize the record setting
            XmlSerializer serializer = new XmlSerializer(typeof(RecordDataSettings));

            using (Stream reader = new FileStream(RecordDataSettings.RecordSettingsFilePath, FileMode.Open))
            {
                this.recordDataSettings = (RecordDataSettings)serializer.Deserialize(reader);
            }

            foreach (DataFilterColumn column in this.recordDataSettings.DataFilterColumns)
            {
                if (column.IsChecked)
                {
                }
                
            }
            


            this.recordFrequencyTimer.Interval = this.recordDataSettings.RecordFrequency;
            this.recordFrequencyTimer.Start();

        }

        private void WriteRecord(Bwt901ble bWT901BLE, System.Data.DataTable data)
        {
            var deviceName = bWT901BLE.GetDeviceName().Replace(':', '.');
            using (var writer = new FileStream(currentTimeFolderPath + $"\\{deviceName}" + ".csv", FileMode.Create))
            {
                using (var csvWriter = new CsvWriter(writer))
                {
                    var dataHeader = new string[data.Columns.Count];
                    for (int i = 0; i < data.Columns.Count; i++)
                    {
                        dataHeader.SetValue(data.Columns[i].ColumnName, i);
                    }
                    csvWriter.WriteRecord(dataHeader);

                    foreach (var row in data.Rows)
                    {
                        var dataRecord = new string[data.Columns.Count];
                        for (int i = 0; i < data.Columns.Count; i++)
                        {
                            dataRecord.SetValue(row.ToString(), i);
                        }

                        csvWriter.WriteRecord(dataRecord);
                    }
                }
            }
        }

        // Enregistre les données dans les fichiers du settings.
        private void StopRecording()
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }

                var deviceName = bWT901BLE.GetDeviceName().Replace(':', '.');

                foreach (var fileSetting in recordDataSettings.FileSettings)
                {
                    if (fileSetting.IsEnabled)
                    {
                        if (fileSetting.IsSeparatedByDevices)
                        {
                            // TODO write the data in multiple file
                            var table = sensorDataTable.FirstOrDefault(s => s.Key == keyValue.Key).Value;
                            if (table != null)
                            {
                                // For csv file
                                using (var writer = new FileStream(currentTimeFolderPath + $"\\{deviceName}" + ".csv", FileMode.Create))
                                {
                                    using (var csvWriter = new CsvWriter(writer))
                                    {
                                        var dataHeader = new string[table.Columns.Count];
                                        for (int j = 0; j < table.Columns.Count; j++)
                                        {
                                            dataHeader.SetValue(table.Columns[j].ColumnName, j);
                                        }
                                        csvWriter.WriteRecord(dataHeader);

                                        foreach (DataRow row in table.Rows)
                                        {
                                            var dataRecord = new string[table.Columns.Count];
                                            for (int j = 0; j < table.Columns.Count; j++)
                                            {
                                                dataRecord.SetValue(row.ItemArray[j].ToString(), j);
                                            }

                                            csvWriter.WriteRecord(dataRecord);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // TODO write the data in a single file
                        }
                    }
                }
            }
        }

        private void RecordSettings_btn_Click(object sender, EventArgs e)
        {
            RecordSettingsForm recordSettings = new RecordSettingsForm(this.recordDataSettings);
            recordSettings.ShowDialog();
        }

        private void RecordFrequencyTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < FoundDeviceDict.Count; i++)
            {
                var keyValue = FoundDeviceDict.ElementAt(i);
                Bwt901ble bWT901BLE = keyValue.Value;

                if (bWT901BLE.IsOpen() == false)
                {
                    return;
                }

                var table = sensorDataTable.FirstOrDefault(s => s.Key == keyValue.Key).Value;

                if (table == null)
                {
                    table = new DataTable();
                    foreach (var column in this.recordDataSettings.DataFilterColumns)
                    {
                        if (column.IsChecked)
                        {
                            table.Columns.Add(column.Name);
                        }
                    }
                    sensorDataTable = sensorDataTable.Append(new KeyValuePair<string, DataTable>(keyValue.Key, table));
                }

                var row = table.NewRow();
                foreach (var column in this.recordDataSettings.DataFilterColumns)
                {
                    if (column.IsChecked)
                    {
                        row[column.Name] = bWT901BLE.GetDeviceData(column.Key);
                    }
                }

                table.Rows.Add(row);
            }
        }
    }
}
