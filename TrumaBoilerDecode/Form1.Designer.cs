
namespace TrumaBoilerDecode
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxAirTemp = new System.Windows.Forms.TextBox();
            this.textBoxWaterMode = new System.Windows.Forms.TextBox();
            this.textBoxPowerMode = new System.Windows.Forms.TextBox();
            this.textBoxVentMode = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(340, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxAirTemp
            // 
            this.textBoxAirTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxAirTemp.Location = new System.Drawing.Point(35, 64);
            this.textBoxAirTemp.Name = "textBoxAirTemp";
            this.textBoxAirTemp.Size = new System.Drawing.Size(100, 29);
            this.textBoxAirTemp.TabIndex = 2;
            // 
            // textBoxWaterMode
            // 
            this.textBoxWaterMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxWaterMode.Location = new System.Drawing.Point(177, 64);
            this.textBoxWaterMode.Name = "textBoxWaterMode";
            this.textBoxWaterMode.Size = new System.Drawing.Size(100, 29);
            this.textBoxWaterMode.TabIndex = 3;
            // 
            // textBoxPowerMode
            // 
            this.textBoxPowerMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPowerMode.Location = new System.Drawing.Point(324, 64);
            this.textBoxPowerMode.Name = "textBoxPowerMode";
            this.textBoxPowerMode.Size = new System.Drawing.Size(100, 29);
            this.textBoxPowerMode.TabIndex = 4;
            // 
            // textBoxVentMode
            // 
            this.textBoxVentMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxVentMode.Location = new System.Drawing.Point(481, 64);
            this.textBoxVentMode.Name = "textBoxVentMode";
            this.textBoxVentMode.Size = new System.Drawing.Size(100, 29);
            this.textBoxVentMode.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(137, 196);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBoxVentMode);
            this.Controls.Add(this.textBoxPowerMode);
            this.Controls.Add(this.textBoxWaterMode);
            this.Controls.Add(this.textBoxAirTemp);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxAirTemp;
        private System.Windows.Forms.TextBox textBoxWaterMode;
        private System.Windows.Forms.TextBox textBoxPowerMode;
        private System.Windows.Forms.TextBox textBoxVentMode;
        private System.Windows.Forms.Button button2;
    }
}

