using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Wit.Example_BWT901BLE
{
    public partial class RecordSettingsForm : Form
    {
        public RecordDataSettings recordDataSettings;
        public RecordSettingsForm(RecordDataSettings recordDataSettings)
        {
            InitializeComponent();
            this.recordSettingsInfo_lbl.Text = "Enabled: When checked, the file will be recorded otherwise it will not be recorded \nPacket number: -1, do not subcontract, otherwise save the file according to the number of data pieces\nSub device: When checked, record one device per file";
            this.recordDataSettings = recordDataSettings;
            DataTable table = new DataTable();
            foreach (PropertyInfo info in typeof(FileSetting).GetProperties())
            {
                table.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }
            foreach(var setting in recordDataSettings.FileSettings)
            {
                var row = table.NewRow();
                row["IsEnabled"] = setting.IsEnabled;
                row["Name"] = setting.Name;
                row["PacketNumber"] = setting.PacketNumber;
                row["IsSeparatedByDevices"] = setting.IsSeparatedByDevices;
                row["Description"] = setting.Description;

                table.Rows.Add(row);
            }

            this.recordFrequence_num.Value = this.recordDataSettings.RecordFrequency;
            this.dataGridView1.DataSource = table;
            
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.DataSource is DataTable table)
            {
                foreach(DataRow row in table.Rows)
                {
                    var temp = this.recordDataSettings.FileSettings.FirstOrDefault(f => f.Name == row.ItemArray[1].ToString());
                    temp.IsEnabled = (bool)row.ItemArray[0];
                    temp.PacketNumber = (int)row.ItemArray[2];
                    temp.IsSeparatedByDevices = (bool)row.ItemArray[3];

                }
            }

            this.recordDataSettings.RecordFrequency = ((int)this.recordFrequence_num.Value);

            using (FileStream writer = File.Create(RecordDataSettings.RecordSettingsFilePath))
            {
                XmlSerializer x = new XmlSerializer(typeof(RecordDataSettings));
                x.Serialize(writer, recordDataSettings);
            }

            this.Close();
        }

        private void fileDataItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataColumnFilteringForm dataColumnFilteringForm = new DataColumnFilteringForm(recordDataSettings.DataFilterColumns.ToArray());
            //dataColumnFilteringForm.ShowDialog();
            if (dataColumnFilteringForm.ShowDialog() == DialogResult.OK)
            {
                this.recordDataSettings.DataFilterColumns = dataColumnFilteringForm.DataFilterColumns;
            }
            
        }
    }
}
