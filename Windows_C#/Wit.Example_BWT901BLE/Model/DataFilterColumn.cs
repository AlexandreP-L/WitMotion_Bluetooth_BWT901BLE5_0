namespace Wit.Example_BWT901BLE
{
    public class DataFilterColumn
    {
        public DataFilterColumn()
        {

        }

        public DataFilterColumn(string name, string key, bool isChecked = true)
        {
            Name = name;
            Key = key;
            IsChecked = isChecked;
        }

        public string Name { get; set; }

        public string Key { get; set; }

        public bool IsChecked { get; set; }
    }
}