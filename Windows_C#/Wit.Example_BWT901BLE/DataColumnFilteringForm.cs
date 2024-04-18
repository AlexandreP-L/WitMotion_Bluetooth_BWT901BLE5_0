using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wit.Example_BWT901BLE
{
    public partial class DataColumnFilteringForm : Form
    {
        private List<DataFilterColumn> tempFilterColumns;

        public DataColumnFilteringForm(DataFilterColumn[] dataFilterColumns)
        {
            InitializeComponent();
            this.tempFilterColumns = dataFilterColumns.ToList();

            ((ListBox)this.listColumn_ckl).DataSource = this.tempFilterColumns;
            ((ListBox)this.listColumn_ckl).DisplayMember = "Name";
            ((ListBox)this.listColumn_ckl).ValueMember = "IsChecked";

            for (int i = 0; i < listColumn_ckl.Items.Count; i++)
            {
                DataFilterColumn obj = (DataFilterColumn)listColumn_ckl.Items[i];
                listColumn_ckl.SetItemChecked(i, obj.IsChecked);
            }
            listColumn_ckl.ItemCheck += OnItemChecked;
        }

        private void OnItemChecked(object sender, ItemCheckEventArgs e)
        {
            DataFilterColumn selectedItem = ((CheckedListBox)sender).SelectedItem as DataFilterColumn;
            var selectedColumn = this.tempFilterColumns.FirstOrDefault(c => c.Name == selectedItem.Name);
            selectedColumn.IsChecked = e.NewValue == CheckState.Checked;
        }

        public DataFilterColumn[] DataFilterColumns { get; set; }

        private void save_btn_Click(object sender, EventArgs e)
        {
            this.DataFilterColumns = this.tempFilterColumns.ToArray();
            this.CleanSubscription();
            this.Close();
        }

        private void CleanSubscription()
        {
            listColumn_ckl.ItemCheck -= OnItemChecked;
        }

        private void closeForm_btn_Click(object sender, EventArgs e)
        {
            this.CleanSubscription();
            this.Close();
        }
    }
}
