using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wit.Example_BWT901BLE.Model
{
    public class MatLabFileSetting : FileSetting
    {
        public override bool IsEnabled { get; set; } = true;

        public override string Name => "mat file";

        public override int PacketNumber { get; set; } = -1;
        public override bool IsSeparatedByDevices { get; set; } = false;

        public override string Description => "Record data in the form of a MATLAB data File";

        public override void SaveRecord(string currentTimeFolderPath, DataTable table, int packetNumber, string deviceName = null)
        {
            throw new NotImplementedException();
        }
    }
}
