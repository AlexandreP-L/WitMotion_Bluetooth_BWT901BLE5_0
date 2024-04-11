using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Wit.Example_BWT901BLE
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                if (!Directory.Exists(RecordDataSettings.RootRecordDirectoryPath))
                {
                    Directory.CreateDirectory(RecordDataSettings.RootRecordDirectoryPath);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            try
            {
                if (!Directory.Exists(RecordDataSettings.RootConfigDirectoryPath))
                {
                    Directory.CreateDirectory(RecordDataSettings.RootConfigDirectoryPath);
                }
                if (!File.Exists(RecordDataSettings.RecordSettingsFilePath))
                {
                    using (FileStream writer = File.Create(RecordDataSettings.RecordSettingsFilePath))
                    {
                        RecordDataSettings recordDataSettings = new RecordDataSettings();
                        FileSetting[] newFileSettings = new FileSetting[]
                        {
                        new FileSetting(true, "csv file", 100000, true, "Record data in CSV table format"),
                        new FileSetting(true, "mat file", -1, false, "Record data in the form of a MATLAB data File"),
                        // new FileSettings(true, "wplay file", -1, true, "Record playable files in binary format"),
                        new FileSetting(false, "raw-data file", 100000, false, "Record the data output by the device"),
                        new FileSetting(false, "txt file", 100000, false, "Record data in txt table format"),

                        };

                        DataFilterColumn[] filterColumns = new DataFilterColumn[]
                        {
                        new DataFilterColumn("Chip Time", "ChipTime"),
                        new DataFilterColumn("Acceleration X", "AccX"),
                        new DataFilterColumn("Acceleration Y", "AccY"),
                        new DataFilterColumn("Acceleration Z", "AccZ"),
                        new DataFilterColumn("Angular Velocity X", "AsX"),
                        new DataFilterColumn("Angular Velocity Y", "AsY"),
                        new DataFilterColumn("Angular Velocity Z", "AsZ"),
                        new DataFilterColumn("Angle X", "AngleX"),
                        new DataFilterColumn("Angle Y", "AngleY"),
                        new DataFilterColumn("Angle Z", "AngleZ"),
                        new DataFilterColumn("Magnetic X", "HX"),
                        new DataFilterColumn("Magnetic Y", "HY"),
                        new DataFilterColumn("Magnetic Z", "HZ"),
                        new DataFilterColumn("Temperature", "T"),
                        new DataFilterColumn("Quaternions 0", "Q0"),
                        new DataFilterColumn("Quaternions 1", "Q1"),
                        new DataFilterColumn("Quaternions 2", "Q2"),
                        new DataFilterColumn("Quaternions 3", "Q3"),
                        };
                        recordDataSettings.DataFilterColumns = filterColumns;
                        recordDataSettings.FileSettings = newFileSettings;

                        XmlSerializer x = new XmlSerializer(typeof(RecordDataSettings));
                        x.Serialize(writer, recordDataSettings);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Application.Run(new Form1());
        }
    }
}
