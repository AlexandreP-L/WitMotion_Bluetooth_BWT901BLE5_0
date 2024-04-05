using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Foundation.Collections;
using Wit.SDK.Modular.WitSensorApi.Modular.BWT901BLE;

namespace Wit.Example_BWT901BLE
{
    public partial class SensorMainData : UserControl
    {
        public SensorMainData()
        {
            InitializeComponent();
            this.sensorCompass.DrawLabel += SensorCompass_DrawLabel;
            this.sensorCompass.Value = 0;
        }

        public string AngleX 
        { 
            get
            {
                return this.angleXTextBox.Text;
            }
            set
            {
                this.angleXTextBox.Text = value;
            }
        }

        public string AngleY
        {
            get
            {
                return this.angleYTextBox.Text;
            }
            set
            {
                this.angleYTextBox.Text = value;
            }
        }

        public string AngleZ
        {
            get
            {
                return this.angleZTextBox.Text;
            }
            set
            {
                this.angleZTextBox.Text = value;
            }
        }

        public string SensorName
        {
            get
            {
                return this.groupBox1.Text;
            }
            set
            {
                this.groupBox1.Text = value;
            }
        }

        public Bwt901ble bwt901Ble { get; set; }

        public void SetCompassStartAngle(double angleZ)
        {
            if (angleZ < 0)
            {
                angleZ = Math.Abs(angleZ);
            }
            else
            {
                angleZ = 360 - angleZ;
            }

            this.sensorCompass.StartAngle = (int)angleZ + 270;
        }

        private void angleReferenceButton_Click(object sender, EventArgs e)
        {
            if (bwt901Ble.IsOpen() == false)
            {
                return;
            }
            try
            {
                // Unlock register and send command
                bwt901Ble.UnlockReg();
                bwt901Ble.SetAngleReference();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SensorCompass_DrawLabel(object sender, Syncfusion.Windows.Forms.Gauge.DrawLabelEventArgs e)
        {
            e.Handled = true;
            if (e.LabelType == Syncfusion.Windows.Forms.Gauge.LabelType.Scale)
            {
                switch (e.Text)
                {
                    case "45":
                        e.Text = "N/E";
                        break;
                    case "90":
                        e.Text = "E";
                        break;
                    case "135":
                        e.Text = "E/S";
                        break;
                    case "180":
                        e.Text = "S";
                        break;
                    case "225":
                        e.Text = "S/W";
                        break;
                    case "270":
                        e.Text = "W";
                        break;
                    case "315":
                        e.Text = "N/W";
                        break;
                    case "0":
                    case "360":
                        e.Text = "N";
                        break;
                }
            }
                //e.Text += " °C";
            //e.LabelAlignment = Syncfusion.Windows.Forms.Gauge.LabelAlignment.Default;
            //e.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
    }
}
