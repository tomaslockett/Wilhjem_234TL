namespace GUI_234TL.Negocio_234TL
{
    partial class FormPdf_234TL
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
            FacturadataGridView = new DataGridView();
            Facturaslabel = new Label();
            Comprobantelabel = new Label();
            ComprobantedataGridView = new DataGridView();
            Volverbutton = new Button();
            ((System.ComponentModel.ISupportInitialize)FacturadataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ComprobantedataGridView).BeginInit();
            SuspendLayout();
            // 
            // FacturadataGridView
            // 
            FacturadataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            FacturadataGridView.Location = new Point(12, 62);
            FacturadataGridView.Name = "FacturadataGridView";
            FacturadataGridView.Size = new Size(776, 182);
            FacturadataGridView.TabIndex = 0;
            FacturadataGridView.CellDoubleClick += FacturadataGridView_CellDoubleClick;
            // 
            // Facturaslabel
            // 
            Facturaslabel.AutoSize = true;
            Facturaslabel.Font = new Font("Segoe UI Semibold", 26.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Facturaslabel.Location = new Point(12, 12);
            Facturaslabel.Name = "Facturaslabel";
            Facturaslabel.Size = new Size(154, 47);
            Facturaslabel.TabIndex = 1;
            Facturaslabel.Text = "Facturas";
            // 
            // Comprobantelabel
            // 
            Comprobantelabel.AutoSize = true;
            Comprobantelabel.Font = new Font("Segoe UI Semibold", 26.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            Comprobantelabel.Location = new Point(12, 252);
            Comprobantelabel.Name = "Comprobantelabel";
            Comprobantelabel.Size = new Size(234, 47);
            Comprobantelabel.TabIndex = 3;
            Comprobantelabel.Text = "Comprobante";
            // 
            // ComprobantedataGridView
            // 
            ComprobantedataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ComprobantedataGridView.Location = new Point(12, 302);
            ComprobantedataGridView.Name = "ComprobantedataGridView";
            ComprobantedataGridView.Size = new Size(776, 182);
            ComprobantedataGridView.TabIndex = 2;
            ComprobantedataGridView.CellContentClick += ComprobantedataGridView_CellContentClick;
            // 
            // Volverbutton
            // 
            Volverbutton.Location = new Point(794, 451);
            Volverbutton.Name = "Volverbutton";
            Volverbutton.Size = new Size(98, 33);
            Volverbutton.TabIndex = 4;
            Volverbutton.Text = "Volver";
            Volverbutton.UseVisualStyleBackColor = true;
            Volverbutton.Click += Volverbutton_Click;
            // 
            // FormPdf_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 495);
            Controls.Add(Volverbutton);
            Controls.Add(Comprobantelabel);
            Controls.Add(ComprobantedataGridView);
            Controls.Add(Facturaslabel);
            Controls.Add(FacturadataGridView);
            Name = "FormPdf_234TL";
            Text = "FormPdf_234TL";
            ((System.ComponentModel.ISupportInitialize)FacturadataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)ComprobantedataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView FacturadataGridView;
        private Label Facturaslabel;
        private Label Comprobantelabel;
        private DataGridView ComprobantedataGridView;
        private Button Volverbutton;
    }
}