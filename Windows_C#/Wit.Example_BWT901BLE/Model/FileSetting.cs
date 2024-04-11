namespace Wit.Example_BWT901BLE
{
    public class FileSetting
    {
        public FileSetting() { }

        public FileSetting(bool isEnabled, string name, int packetNumber, bool isSeparatedByDevices, string description)
        {
            IsEnabled = isEnabled;
            Name = name;
            PacketNumber = packetNumber;
            IsSeparatedByDevices = isSeparatedByDevices;
            Description = description;
        }

        public bool IsEnabled { get; set; }

        public string Name { get; set; }

        public int PacketNumber { get; set; }

        public bool IsSeparatedByDevices { get; set; }

        public string  Description { get; set; }
    }
}