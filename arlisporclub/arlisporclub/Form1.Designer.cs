namespace arlisporclub
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.yeni_kayit = new System.Windows.Forms.Button();
            this.aidat = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // yeni_kayit
            // 
            this.yeni_kayit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.yeni_kayit.Location = new System.Drawing.Point(58, 50);
            this.yeni_kayit.Name = "yeni_kayit";
            this.yeni_kayit.Size = new System.Drawing.Size(153, 40);
            this.yeni_kayit.TabIndex = 0;
            this.yeni_kayit.Text = "Yeni Kayıt";
            this.yeni_kayit.UseVisualStyleBackColor = true;
            this.yeni_kayit.Click += new System.EventHandler(this.yeni_kayit_Click);
            // 
            // aidat
            // 
            this.aidat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.aidat.Location = new System.Drawing.Point(58, 123);
            this.aidat.Name = "aidat";
            this.aidat.Size = new System.Drawing.Size(153, 40);
            this.aidat.TabIndex = 1;
            this.aidat.Text = "Üye Listesi";
            this.aidat.UseVisualStyleBackColor = true;
            this.aidat.Click += new System.EventHandler(this.aidat_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(158, 213);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "copyright by S4H";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 237);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aidat);
            this.Controls.Add(this.yeni_kayit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Fitline Fitness Center";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button yeni_kayit;
        private System.Windows.Forms.Button aidat;
        private System.Windows.Forms.Label label1;

    }
}

