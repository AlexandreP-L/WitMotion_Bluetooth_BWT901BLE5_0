using KBCsv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace Wit.Example_BWT901BLE.Model
{
    public class CsvFileSetting : FileSetting
    {
        public override bool IsEnabled { get; set; } = true;
        public override string Name => "csv file";
        public override int PacketNumber { get; set; } = 100000;
        public override bool IsSeparatedByDevices { get; set; } = true;

        public override string Description => "Record data in CSV table format";

        public override void SaveRecord(string currentTimeFolderPath, DataTable table, int packetNumber, string deviceName = null)
        {
            var sortedData = table.AsEnumerable().OrderBy(r => r["Time"]);
            
            deviceName = deviceName ?? "data";
            if (packetNumber < 0 || packetNumber > table.Rows.Count) {
                packetNumber = table.Rows.Count;
            }
            int folderNumber = 0;
            for (int i = 0; i < table.Rows.Count; folderNumber++)
            {
                using (var writer = new FileStream(currentTimeFolderPath + $"\\{deviceName}_{folderNumber}" + ".csv", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var csvWriter = new CsvWriter(writer))
                    {
                        var dataHeader = new string[table.Columns.Count];
                        for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                        {
                            dataHeader.SetValue(table.Columns[columnIndex].ColumnName, columnIndex);
                        }
                        csvWriter.WriteRecord(dataHeader);

                        for (int j = 0; j < packetNumber && i < table.Rows.Count; j++, i++)
                        {
                            var dataRecord = new string[table.Columns.Count];
                            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                            {
                                dataRecord.SetValue(sortedData.ElementAt(i).ItemArray[columnIndex].ToString(), columnIndex);
                            }

                            csvWriter.WriteRecord(dataRecord);
                        }
                    }
                }
            }
        }
    }
}
