namespace Responder
{
    partial class OrderForm
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
            this.pulsanteInserisci = new System.Windows.Forms.Button();
            this.pulsanteAnnulla = new System.Windows.Forms.Button();
            this.pulsanteEmetti = new System.Windows.Forms.Button();
            this.textGuidOrder = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // pulsanteInserisci
            // 
            this.pulsanteInserisci.Location = new System.Drawing.Point(12, 12);
            this.pulsanteInserisci.Name = "pulsanteInserisci";
            this.pulsanteInserisci.Size = new System.Drawing.Size(75, 23);
            this.pulsanteInserisci.TabIndex = 0;
            this.pulsanteInserisci.Text = "Insert Order";
            this.pulsanteInserisci.UseVisualStyleBackColor = true;
            this.pulsanteInserisci.Click += new System.EventHandler(this.button1_Click);
            // 
            // pulsanteAnnulla
            // 
            this.pulsanteAnnulla.Location = new System.Drawing.Point(12, 50);
            this.pulsanteAnnulla.Name = "pulsanteAnnulla";
            this.pulsanteAnnulla.Size = new System.Drawing.Size(75, 23);
            this.pulsanteAnnulla.TabIndex = 1;
            this.pulsanteAnnulla.Text = "Annulla Ordine";
            this.pulsanteAnnulla.UseVisualStyleBackColor = true;
            this.pulsanteAnnulla.Click += new System.EventHandler(this.pulsanteAnnulla_Click);
            // 
            // pulsanteEmetti
            // 
            this.pulsanteEmetti.Location = new System.Drawing.Point(12, 93);
            this.pulsanteEmetti.Name = "pulsanteEmetti";
            this.pulsanteEmetti.Size = new System.Drawing.Size(131, 23);
            this.pulsanteEmetti.TabIndex = 2;
            this.pulsanteEmetti.Text = "Emissione Bolla";
            this.pulsanteEmetti.UseVisualStyleBackColor = true;
            // 
            // textGuidOrder
            // 
            this.textGuidOrder.Location = new System.Drawing.Point(144, 50);
            this.textGuidOrder.Name = "textGuidOrder";
            this.textGuidOrder.Size = new System.Drawing.Size(243, 20);
            this.textGuidOrder.TabIndex = 3;
            // 
            // OrderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 262);
            this.Controls.Add(this.textGuidOrder);
            this.Controls.Add(this.pulsanteEmetti);
            this.Controls.Add(this.pulsanteAnnulla);
            this.Controls.Add(this.pulsanteInserisci);
            this.Name = "OrderForm";
            this.Text = "OrderForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button pulsanteInserisci;
        private System.Windows.Forms.Button pulsanteAnnulla;
        private System.Windows.Forms.Button pulsanteEmetti;
        private System.Windows.Forms.TextBox textGuidOrder;
    }
}