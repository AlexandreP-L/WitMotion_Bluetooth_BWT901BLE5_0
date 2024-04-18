using KBCsv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Example_BWT901BLE.Model
{
    public class TextFileSetting : FileSetting
    {
        public override bool IsEnabled { get; set; } = false;

        public override string Name => "txt file";

        public override int PacketNumber { get; set; } = 100000;
        public override bool IsSeparatedByDevices { get; set; } = false;

        public override string Description => "Record data in txt table format";

        public override void SaveRecord(string currentTimeFolderPath, DataTable table, int packetNumber, string deviceName = null)
        {
            var sortedData = table.AsEnumerable().OrderBy(r => r["Time"]);

            deviceName = deviceName ?? "data";
            if (packetNumber < 0 || packetNumber > table.Rows.Count)
            {
                packetNumber = table.Rows.Count;
            }
            int folderNumber = 0;
            for (int i = 0; i < table.Rows.Count; folderNumber++)
            {
                using (var writer = new FileStream(currentTimeFolderPath + $"\\{deviceName}_{folderNumber}" + ".txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (var textWriter = new StreamWriter(writer))
                    {
                        for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                        {
                            textWriter.Write(table.Columns[columnIndex].ColumnName + "\t");
                        }
                        textWriter.WriteLine();

                        for (int j = 0; j < packetNumber && i < table.Rows.Count; j++, i++)
                        {
                            var dataRecord = new string[table.Columns.Count];
                            for (int columnIndex = 0; columnIndex < table.Columns.Count; columnIndex++)
                            {
                                textWriter.Write(sortedData.ElementAt(i).ItemArray[columnIndex].ToString() + "\t");
                            }

                            textWriter.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
