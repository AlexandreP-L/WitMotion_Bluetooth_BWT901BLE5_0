using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wit.Example_BWT901BLE
{
    public partial class DataColumnFilteringForm : Form
    {
        public DataColumnFilteringForm(DataFilterColumn[] dataFilterColumns)
        {
            InitializeComponent();
            this.DataFilterColumns = dataFilterColumns;

            ((ListBox)this.listColumn_ckl).DataSource = dataFilterColumns.ToList();
            ((ListBox)this.listColumn_ckl).DisplayMember = "Name";
            ((ListBox)this.listColumn_ckl).ValueMember = "IsChecked";

            for (int i = 0; i < listColumn_ckl.Items.Count; i++)
            {
                DataFilterColumn obj = (DataFilterColumn)listColumn_ckl.Items[i];
                listColumn_ckl.SetItemChecked(i, obj.IsChecked);
            }
        }

        public DataFilterColumn[] DataFilterColumns { get; set; }

        private void save_btn_Click(object sender, EventArgs e)
        {
            // TODO save data
            this.Close();
        }

        private void closeForm_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
