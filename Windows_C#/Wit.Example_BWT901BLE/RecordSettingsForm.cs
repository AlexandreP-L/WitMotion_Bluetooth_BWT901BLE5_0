using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
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
            DataTable table = new DataTable();
            try
            {
                DataSet ds = new DataSet();
                ds.ReadXml(RecordDataSettings.RecordSettingsFilePath);
                table = ds.Tables["FileSetting"];
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            this.dataGridView1.DataSource = table;
            this.recordDataSettings = recordDataSettings;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            // TODO Update the config File

            this.Close();
        }

        private void fileDataItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataColumnFilteringForm dataColumnFilteringForm = new DataColumnFilteringForm(recordDataSettings.DataFilterColumns);
            dataColumnFilteringForm.ShowDialog();
        }
    }
}
