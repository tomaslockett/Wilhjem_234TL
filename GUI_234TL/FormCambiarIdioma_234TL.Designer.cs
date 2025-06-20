namespace GUI_234TL
{
    partial class FormCambiarIdioma_234TL
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
            EspañolButton = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            ItalianoButton = new Button();
            InglesButton = new Button();
            VolverButton = new Button();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // EspañolButton
            // 
            EspañolButton.Location = new Point(3, 3);
            EspañolButton.Name = "EspañolButton";
            EspañolButton.Size = new Size(176, 38);
            EspañolButton.TabIndex = 0;
            EspañolButton.Text = "Español";
            EspañolButton.UseVisualStyleBackColor = true;
            EspañolButton.Click += EspañolButton_Click;
            EspañolButton.MouseLeave += EspañolButton_MouseLeave;
            EspañolButton.MouseHover += EspañolButton_MouseHover;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(EspañolButton);
            flowLayoutPanel1.Controls.Add(ItalianoButton);
            flowLayoutPanel1.Controls.Add(InglesButton);
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(241, 159);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // ItalianoButton
            // 
            ItalianoButton.Location = new Point(3, 47);
            ItalianoButton.Name = "ItalianoButton";
            ItalianoButton.Size = new Size(176, 38);
            ItalianoButton.TabIndex = 2;
            ItalianoButton.Text = "Italiano";
            ItalianoButton.UseVisualStyleBackColor = true;
            ItalianoButton.Click += ItalianoButton_Click;
            ItalianoButton.MouseLeave += ItalianoButton_MouseLeave;
            ItalianoButton.MouseHover += ItalianoButton_MouseHover;
            // 
            // InglesButton
            // 
            InglesButton.Location = new Point(3, 91);
            InglesButton.Name = "InglesButton";
            InglesButton.Size = new Size(176, 38);
            InglesButton.TabIndex = 1;
            InglesButton.Text = "Ingles";
            InglesButton.UseVisualStyleBackColor = true;
            InglesButton.Click += InglesButton_Click;
            InglesButton.MouseLeave += InglesButton_MouseLeave;
            InglesButton.MouseHover += InglesButton_MouseHover;
            // 
            // VolverButton
            // 
            VolverButton.Location = new Point(259, 133);
            VolverButton.Name = "VolverButton";
            VolverButton.Size = new Size(176, 38);
            VolverButton.TabIndex = 3;
            VolverButton.Text = "Volver";
            VolverButton.UseVisualStyleBackColor = true;
            VolverButton.Click += VolverButton_Click;
            // 
            // FormCambiarIdioma_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(444, 180);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(VolverButton);
            Name = "FormCambiarIdioma_234TL";
            Text = "FormCambiarIdioma_234TL";
            FormClosed += FormCambiarIdioma_234TL_FormClosed;
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button EspañolButton;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button InglesButton;
        private Button ItalianoButton;
        private Button VolverButton;
    }
}