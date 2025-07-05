namespace GUI_234TL
{
    partial class FormFacturaYComprobante
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
            dataGridViewReparaciones = new DataGridView();
            CrearFacturaButton = new Button();
            CrearComprobanteButton = new Button();
            CobrarDiagnosticoButton = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReparaciones).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewReparaciones
            // 
            dataGridViewReparaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReparaciones.Location = new Point(12, 22);
            dataGridViewReparaciones.Name = "dataGridViewReparaciones";
            dataGridViewReparaciones.Size = new Size(877, 258);
            dataGridViewReparaciones.TabIndex = 0;
            // 
            // CrearFacturaButton
            // 
            CrearFacturaButton.Location = new Point(523, 315);
            CrearFacturaButton.Name = "CrearFacturaButton";
            CrearFacturaButton.Size = new Size(180, 54);
            CrearFacturaButton.TabIndex = 2;
            CrearFacturaButton.Text = "Crear Factura";
            CrearFacturaButton.UseVisualStyleBackColor = true;
            CrearFacturaButton.Click += CrearFacturaButton_Click;
            // 
            // CrearComprobanteButton
            // 
            CrearComprobanteButton.Location = new Point(709, 315);
            CrearComprobanteButton.Name = "CrearComprobanteButton";
            CrearComprobanteButton.Size = new Size(180, 54);
            CrearComprobanteButton.TabIndex = 3;
            CrearComprobanteButton.Text = "Crear Comprobante";
            CrearComprobanteButton.UseVisualStyleBackColor = true;
            CrearComprobanteButton.Click += CrearComprobanteButton_Click;
            // 
            // CobrarDiagnosticoButton
            // 
            CobrarDiagnosticoButton.Location = new Point(12, 315);
            CobrarDiagnosticoButton.Name = "CobrarDiagnosticoButton";
            CobrarDiagnosticoButton.Size = new Size(180, 54);
            CobrarDiagnosticoButton.TabIndex = 4;
            CobrarDiagnosticoButton.Text = "CobrarDiagnostico";
            CobrarDiagnosticoButton.UseVisualStyleBackColor = true;
            CobrarDiagnosticoButton.Click += CobrarDiagnosticoButton_Click;
            // 
            // FormFacturaYComprobante
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 402);
            Controls.Add(CobrarDiagnosticoButton);
            Controls.Add(CrearComprobanteButton);
            Controls.Add(CrearFacturaButton);
            Controls.Add(dataGridViewReparaciones);
            Name = "FormFacturaYComprobante";
            Text = "FormFacturaYComprobante";
            FormClosed += FormFacturaYComprobante_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridViewReparaciones).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewReparaciones;
        private Button CrearFacturaButton;
        private Button CrearComprobanteButton;
        private Button CobrarDiagnosticoButton;
    }
}