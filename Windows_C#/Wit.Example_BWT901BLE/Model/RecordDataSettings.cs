using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Wit.Example_BWT901BLE.Model;

namespace Wit.Example_BWT901BLE
{
    [XmlRoot("RecordDataSettings", Namespace = "http://www.cpandl.com", IsNullable = false)]
    public class RecordDataSettings
    {
        public static string RootRecordDirectoryPath = Path.Combine(Application.StartupPath, "Record");
        public static string RootConfigDirectoryPath = Path.Combine(Application.StartupPath, "Config\\WitConfig");
        public static string RecordSettingsFilePath = Path.Combine(RootConfigDirectoryPath, "RecordDataSettings.xml");

        public RecordDataSettings()
        {
            FileSettings = new FileSetting[] { };
            DataFilterColumns = new DataFilterColumn[] { };
        }

        public RecordDataSettings(FileSetting[] fileSettings = null, DataFilterColumn[] dataFilterColumns = null)
        {
            FileSettings = fileSettings ?? new FileSetting[] { };
            DataFilterColumns = dataFilterColumns ?? new DataFilterColumn[] { };
        }

        [XmlArray("FileSettings")]
        [XmlArrayItem(typeof(CsvFileSetting))]
        [XmlArrayItem(typeof(MatLabFileSetting))]
        [XmlArrayItem(typeof(RawDataFileSetting))]
        [XmlArrayItem(typeof(WPlayFileSetting))]
        [XmlArrayItem(typeof(TextFileSetting))]
        public FileSetting[] FileSettings { get; set; }

        [XmlArray("DataFilterColumns")]
        public DataFilterColumn[] DataFilterColumns { get; set; }

        public int RecordFrequency { get; set; } = 1000;
    }
}
