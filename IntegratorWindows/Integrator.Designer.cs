namespace IntegratorWindows
{
    partial class Integrator
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
            this.btnProcessRepositories = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProcessRepositories
            // 
            this.btnProcessRepositories.Location = new System.Drawing.Point(12, 12);
            this.btnProcessRepositories.Name = "btnProcessRepositories";
            this.btnProcessRepositories.Size = new System.Drawing.Size(145, 23);
            this.btnProcessRepositories.TabIndex = 0;
            this.btnProcessRepositories.Text = "Process Repositories";
            this.btnProcessRepositories.UseVisualStyleBackColor = true;
            this.btnProcessRepositories.Click += new System.EventHandler(this.btnProcessRepositories_Click);
            // 
            // Integrator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnProcessRepositories);
            this.Name = "Integrator";
            this.Text = "frmIntegrator";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnProcessRepositories;
    }
}

