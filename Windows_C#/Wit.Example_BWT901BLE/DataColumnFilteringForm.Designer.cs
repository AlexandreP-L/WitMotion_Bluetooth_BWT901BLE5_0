namespace Wit.Example_BWT901BLE
{
    partial class DataColumnFilteringForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listColumn_ckl = new System.Windows.Forms.CheckedListBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.save_btn = new System.Windows.Forms.Button();
            this.closeForm_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listColumn_ckl
            // 
            this.listColumn_ckl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listColumn_ckl.FormattingEnabled = true;
            this.listColumn_ckl.Location = new System.Drawing.Point(0, -1);
            this.listColumn_ckl.MultiColumn = true;
            this.listColumn_ckl.Name = "listColumn_ckl";
            this.listColumn_ckl.Size = new System.Drawing.Size(578, 199);
            this.listColumn_ckl.TabIndex = 0;
            // 
            // save_btn
            // 
            this.save_btn.Location = new System.Drawing.Point(324, 204);
            this.save_btn.Name = "save_btn";
            this.save_btn.Size = new System.Drawing.Size(92, 32);
            this.save_btn.TabIndex = 1;
            this.save_btn.Text = "Save";
            this.save_btn.UseVisualStyleBackColor = true;
            this.save_btn.Click += new System.EventHandler(this.save_btn_Click);
            // 
            // closeForm_btn
            // 
            this.closeForm_btn.Location = new System.Drawing.Point(157, 204);
            this.closeForm_btn.Name = "closeForm_btn";
            this.closeForm_btn.Size = new System.Drawing.Size(92, 32);
            this.closeForm_btn.TabIndex = 2;
            this.closeForm_btn.Text = "Close";
            this.closeForm_btn.UseVisualStyleBackColor = true;
            this.closeForm_btn.Click += new System.EventHandler(this.closeForm_btn_Click);
            // 
            // DataColumnFilteringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 244);
            this.Controls.Add(this.closeForm_btn);
            this.Controls.Add(this.save_btn);
            this.Controls.Add(this.listColumn_ckl);
            this.Name = "DataColumnFilteringForm";
            this.Text = "Data Column Filtering";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listColumn_ckl;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button save_btn;
        private System.Windows.Forms.Button closeForm_btn;
    }
}