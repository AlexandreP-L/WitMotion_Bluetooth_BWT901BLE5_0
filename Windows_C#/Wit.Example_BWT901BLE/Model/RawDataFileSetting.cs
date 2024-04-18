using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Example_BWT901BLE.Model
{
    public class RawDataFileSetting : FileSetting
    {
        public override bool IsEnabled { get; set; } = false;

        public override string Name => "raw-data file";

        public override int PacketNumber { get; set; } = 100000;
        public override bool IsSeparatedByDevices { get; set; } = false;

        public override string Description => "Record the data output by the device";

        public override void SaveRecord(string currentTimeFolderPath, DataTable table, int packetNumber, string deviceName = null)
        {
            throw new NotImplementedException();
        }
    }
}
