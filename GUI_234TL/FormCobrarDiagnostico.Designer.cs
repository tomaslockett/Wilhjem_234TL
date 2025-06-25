namespace GUI_234TL
{
    partial class FormCobrarDiagnostico
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
            CobrarButton = new Button();
            NumeroTarjetaLabel = new Label();
            NumeroTarjetaTextBox = new TextBox();
            CodigoSeguridadTextBox = new TextBox();
            VencimientoLabel = new Label();
            VencimientoTextBox = new TextBox();
            CodigoSeguridadLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewReparaciones).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewReparaciones
            // 
            dataGridViewReparaciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewReparaciones.Location = new Point(30, 31);
            dataGridViewReparaciones.Name = "dataGridViewReparaciones";
            dataGridViewReparaciones.Size = new Size(697, 291);
            dataGridViewReparaciones.TabIndex = 0;
            // 
            // CobrarButton
            // 
            CobrarButton.Location = new Point(274, 328);
            CobrarButton.Name = "CobrarButton";
            CobrarButton.Size = new Size(170, 59);
            CobrarButton.TabIndex = 1;
            CobrarButton.Text = "Cobrar ";
            CobrarButton.UseVisualStyleBackColor = true;
            CobrarButton.Click += CobrarButton_Click;
            // 
            // NumeroTarjetaLabel
            // 
            NumeroTarjetaLabel.AutoSize = true;
            NumeroTarjetaLabel.Location = new Point(29, 329);
            NumeroTarjetaLabel.Name = "NumeroTarjetaLabel";
            NumeroTarjetaLabel.Size = new Size(89, 15);
            NumeroTarjetaLabel.TabIndex = 3;
            NumeroTarjetaLabel.Text = "Numero Tarjeta";
            // 
            // NumeroTarjetaTextBox
            // 
            NumeroTarjetaTextBox.Location = new Point(125, 329);
            NumeroTarjetaTextBox.Name = "NumeroTarjetaTextBox";
            NumeroTarjetaTextBox.Size = new Size(132, 23);
            NumeroTarjetaTextBox.TabIndex = 4;
            // 
            // CodigoSeguridadTextBox
            // 
            CodigoSeguridadTextBox.Location = new Point(125, 358);
            CodigoSeguridadTextBox.Name = "CodigoSeguridadTextBox";
            CodigoSeguridadTextBox.Size = new Size(132, 23);
            CodigoSeguridadTextBox.TabIndex = 8;
            // 
            // VencimientoLabel
            // 
            VencimientoLabel.AutoSize = true;
            VencimientoLabel.Location = new Point(30, 387);
            VencimientoLabel.Name = "VencimientoLabel";
            VencimientoLabel.Size = new Size(73, 15);
            VencimientoLabel.TabIndex = 9;
            VencimientoLabel.Text = "Vencimiento";
            // 
            // VencimientoTextBox
            // 
            VencimientoTextBox.Location = new Point(125, 384);
            VencimientoTextBox.Name = "VencimientoTextBox";
            VencimientoTextBox.Size = new Size(132, 23);
            VencimientoTextBox.TabIndex = 10;
            // 
            // CodigoSeguridadLabel
            // 
            CodigoSeguridadLabel.AutoSize = true;
            CodigoSeguridadLabel.Location = new Point(16, 361);
            CodigoSeguridadLabel.Name = "CodigoSeguridadLabel";
            CodigoSeguridadLabel.Size = new Size(102, 15);
            CodigoSeguridadLabel.TabIndex = 11;
            CodigoSeguridadLabel.Text = "Codigo Seguridad";
            // 
            // FormCobrarDiagnostico
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(769, 429);
            Controls.Add(CodigoSeguridadLabel);
            Controls.Add(VencimientoTextBox);
            Controls.Add(VencimientoLabel);
            Controls.Add(CodigoSeguridadTextBox);
            Controls.Add(NumeroTarjetaTextBox);
            Controls.Add(NumeroTarjetaLabel);
            Controls.Add(CobrarButton);
            Controls.Add(dataGridViewReparaciones);
            Name = "FormCobrarDiagnostico";
            Text = "FormCobrarDiagnostico";
            FormClosed += FormCobrarDiagnostico_FormClosed;
            ((System.ComponentModel.ISupportInitialize)dataGridViewReparaciones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridViewReparaciones;
        private Button CobrarButton;
        private Label NumeroTarjetaLabel;
        private TextBox NumeroTarjetaTextBox;
        private TextBox textBox2;
        private Label label2;
        private TextBox CodigoSeguridadTextBox;
        private Label label3;
        private TextBox textBox4;
        private Label VencimientoLabel;
        private TextBox VencimientoTextBox;
        private Label CodigoSeguridadLabel;
    }
}