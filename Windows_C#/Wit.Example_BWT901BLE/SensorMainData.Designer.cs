namespace Wit.Example_BWT901BLE
{
    partial class SensorMainData
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.sensorLbl = new System.Windows.Forms.Label();
            this.sensorCompass = new Syncfusion.Windows.Forms.Gauge.RadialGauge();
            this.angleReferenceButton = new System.Windows.Forms.Button();
            this.angleZTextBox = new System.Windows.Forms.TextBox();
            this.angleYTextBox = new System.Windows.Forms.TextBox();
            this.angleXTextBox = new System.Windows.Forms.TextBox();
            this.angleZLabel = new System.Windows.Forms.Label();
            this.angleYLabel = new System.Windows.Forms.Label();
            this.angleXLabel = new System.Windows.Forms.Label();
            this.radialGauge1 = new Syncfusion.Windows.Forms.Gauge.RadialGauge();
            this.signalStrength_lbl = new System.Windows.Forms.Label();
            this.signalStrengthValue = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.signalStrengthValue);
            this.groupBox1.Controls.Add(this.signalStrength_lbl);
            this.groupBox1.Controls.Add(this.sensorLbl);
            this.groupBox1.Controls.Add(this.sensorCompass);
            this.groupBox1.Controls.Add(this.angleReferenceButton);
            this.groupBox1.Controls.Add(this.angleZTextBox);
            this.groupBox1.Controls.Add(this.angleYTextBox);
            this.groupBox1.Controls.Add(this.angleXTextBox);
            this.groupBox1.Controls.Add(this.angleZLabel);
            this.groupBox1.Controls.Add(this.angleYLabel);
            this.groupBox1.Controls.Add(this.angleXLabel);
            this.groupBox1.Location = new System.Drawing.Point(0, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(537, 298);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // sensorLbl
            // 
            this.sensorLbl.AutoSize = true;
            this.sensorLbl.Location = new System.Drawing.Point(356, 67);
            this.sensorLbl.Name = "sensorLbl";
            this.sensorLbl.Size = new System.Drawing.Size(125, 13);
            this.sensorLbl.TabIndex = 8;
            this.sensorLbl.Text = "Orientation of the sensor:";
            // 
            // sensorCompass
            // 
            this.sensorCompass.ArcThickness = 2F;
            this.sensorCompass.BackgroundGradientEndColor = System.Drawing.Color.White;
            this.sensorCompass.BackgroundGradientStartColor = System.Drawing.Color.White;
            this.sensorCompass.EnableCustomNeedles = false;
            this.sensorCompass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(180)))), ((int)(((byte)(205)))));
            this.sensorCompass.FrameThickness = 12;
            this.sensorCompass.GaugeLabel = "";
            this.sensorCompass.GaugeLableFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sensorCompass.GaugeValueFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sensorCompass.Location = new System.Drawing.Point(313, 80);
            this.sensorCompass.MajorDifference = 45F;
            this.sensorCompass.MaximumValue = 360F;
            this.sensorCompass.MinimumSize = new System.Drawing.Size(125, 125);
            this.sensorCompass.MinorDifference = 5F;
            this.sensorCompass.Name = "sensorCompass";
            this.sensorCompass.NeedleColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(180)))), ((int)(((byte)(205)))));
            this.sensorCompass.NeedleStyle = Syncfusion.Windows.Forms.Gauge.NeedleStyle.Advanced;
            this.sensorCompass.OuterFrameGradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(180)))), ((int)(((byte)(205)))));
            this.sensorCompass.OuterFrameGradientStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(180)))), ((int)(((byte)(205)))));
            this.sensorCompass.ShowTicks = true;
            this.sensorCompass.Size = new System.Drawing.Size(218, 218);
            this.sensorCompass.StartAngle = 270;
            this.sensorCompass.SweepAngle = 360;
            this.sensorCompass.TabIndex = 7;
            this.sensorCompass.ThemeName = "Metro";
            this.sensorCompass.ThemeStyle.ArcThickness = 20F;
            this.sensorCompass.VisualStyle = Syncfusion.Windows.Forms.Gauge.ThemeStyle.Metro;
            // 
            // angleReferenceButton
            // 
            this.angleReferenceButton.Location = new System.Drawing.Point(388, 23);
            this.angleReferenceButton.Name = "angleReferenceButton";
            this.angleReferenceButton.Size = new System.Drawing.Size(121, 23);
            this.angleReferenceButton.TabIndex = 6;
            this.angleReferenceButton.Text = "Angle Reference";
            this.angleReferenceButton.UseVisualStyleBackColor = true;
            this.angleReferenceButton.Click += new System.EventHandler(this.angleReferenceButton_Click);
            // 
            // angleZTextBox
            // 
            this.angleZTextBox.Location = new System.Drawing.Point(66, 103);
            this.angleZTextBox.Name = "angleZTextBox";
            this.angleZTextBox.Size = new System.Drawing.Size(229, 20);
            this.angleZTextBox.TabIndex = 5;
            this.angleZTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // angleYTextBox
            // 
            this.angleYTextBox.Location = new System.Drawing.Point(66, 64);
            this.angleYTextBox.Name = "angleYTextBox";
            this.angleYTextBox.Size = new System.Drawing.Size(229, 20);
            this.angleYTextBox.TabIndex = 4;
            this.angleYTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // angleXTextBox
            // 
            this.angleXTextBox.Location = new System.Drawing.Point(66, 25);
            this.angleXTextBox.Name = "angleXTextBox";
            this.angleXTextBox.Size = new System.Drawing.Size(229, 20);
            this.angleXTextBox.TabIndex = 3;
            this.angleXTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // angleZLabel
            // 
            this.angleZLabel.AutoSize = true;
            this.angleZLabel.Location = new System.Drawing.Point(6, 106);
            this.angleZLabel.Name = "angleZLabel";
            this.angleZLabel.Size = new System.Drawing.Size(44, 13);
            this.angleZLabel.TabIndex = 2;
            this.angleZLabel.Text = "Angle Z";
            // 
            // angleYLabel
            // 
            this.angleYLabel.AutoSize = true;
            this.angleYLabel.Location = new System.Drawing.Point(6, 67);
            this.angleYLabel.Name = "angleYLabel";
            this.angleYLabel.Size = new System.Drawing.Size(44, 13);
            this.angleYLabel.TabIndex = 1;
            this.angleYLabel.Text = "Angle Y";
            // 
            // angleXLabel
            // 
            this.angleXLabel.AutoSize = true;
            this.angleXLabel.Location = new System.Drawing.Point(6, 28);
            this.angleXLabel.Name = "angleXLabel";
            this.angleXLabel.Size = new System.Drawing.Size(44, 13);
            this.angleXLabel.TabIndex = 0;
            this.angleXLabel.Text = "Angle X";
            // 
            // radialGauge1
            // 
            this.radialGauge1.ArcThickness = 2F;
            this.radialGauge1.EnableCustomNeedles = false;
            this.radialGauge1.FillColor = System.Drawing.Color.DarkGray;
            this.radialGauge1.FrameThickness = 12;
            this.radialGauge1.GaugeLabel = "";
            this.radialGauge1.GaugeLableFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radialGauge1.GaugeValueFont = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radialGauge1.Location = new System.Drawing.Point(0, 0);
            this.radialGauge1.MajorDifference = 10F;
            this.radialGauge1.MaximumValue = 80F;
            this.radialGauge1.MinimumSize = new System.Drawing.Size(125, 125);
            this.radialGauge1.Name = "radialGauge1";
            this.radialGauge1.ShowTicks = true;
            this.radialGauge1.Size = new System.Drawing.Size(230, 230);
            this.radialGauge1.StartAngle = 270;
            this.radialGauge1.SweepAngle = 360;
            this.radialGauge1.TabIndex = 0;
            // 
            // signalStrength_lbl
            // 
            this.signalStrength_lbl.AutoSize = true;
            this.signalStrength_lbl.Location = new System.Drawing.Point(6, 146);
            this.signalStrength_lbl.Name = "signalStrength_lbl";
            this.signalStrength_lbl.Size = new System.Drawing.Size(82, 13);
            this.signalStrength_lbl.TabIndex = 9;
            this.signalStrength_lbl.Text = "Signal Strength:";
            this.signalStrength_lbl.Visible = false;
            // 
            // signalStrengthValue
            // 
            this.signalStrengthValue.AutoSize = true;
            this.signalStrengthValue.Location = new System.Drawing.Point(95, 146);
            this.signalStrengthValue.Name = "signalStrengthValue";
            this.signalStrengthValue.Size = new System.Drawing.Size(0, 13);
            this.signalStrengthValue.TabIndex = 10;
            this.signalStrengthValue.Visible = false;
            // 
            // SensorMainData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBox1);
            this.Name = "SensorMainData";
            this.Size = new System.Drawing.Size(540, 304);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox angleYTextBox;
        private System.Windows.Forms.TextBox angleXTextBox;
        private System.Windows.Forms.Label angleZLabel;
        private System.Windows.Forms.Label angleYLabel;
        private System.Windows.Forms.Label angleXLabel;
        private System.Windows.Forms.TextBox angleZTextBox;
        private System.Windows.Forms.Button angleReferenceButton;
        private Syncfusion.Windows.Forms.Gauge.RadialGauge radialGauge1;
        private Syncfusion.Windows.Forms.Gauge.RadialGauge sensorCompass;
        private System.Windows.Forms.Label sensorLbl;
        private System.Windows.Forms.Label signalStrengthValue;
        private System.Windows.Forms.Label signalStrength_lbl;
    }
}
