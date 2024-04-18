using System.Data;

namespace Wit.Example_BWT901BLE
{
    public abstract class FileSetting
    {
        public abstract bool IsEnabled { get; set; }

        public abstract string Name { get; }

        public abstract int PacketNumber { get; set; }

        public abstract bool IsSeparatedByDevices { get; set; }

        public abstract string Description { get; }

        public abstract void SaveRecord(string currentTimeFolderPath, DataTable table,int packetNumber, string deviceName = null);
    }
}