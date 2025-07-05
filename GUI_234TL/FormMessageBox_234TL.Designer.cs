namespace GUI_234TL
{
    partial class FormMessageBox_234TL
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
            Boton1 = new Button();
            Boton2 = new Button();
            Boton3 = new Button();
            Mensaje = new Label();
            SuspendLayout();
            // 
            // Boton1
            // 
            Boton1.Location = new Point(44, 89);
            Boton1.Name = "Boton1";
            Boton1.Size = new Size(75, 23);
            Boton1.TabIndex = 0;
            Boton1.Text = "Boton1";
            Boton1.UseVisualStyleBackColor = true;
            Boton1.Click += Boton1_Click;
            // 
            // Boton2
            // 
            Boton2.Location = new Point(154, 89);
            Boton2.Name = "Boton2";
            Boton2.Size = new Size(75, 23);
            Boton2.TabIndex = 1;
            Boton2.Text = "Boton2";
            Boton2.UseVisualStyleBackColor = true;
            Boton2.Click += Boton2_Click;
            // 
            // Boton3
            // 
            Boton3.Location = new Point(275, 89);
            Boton3.Name = "Boton3";
            Boton3.Size = new Size(75, 23);
            Boton3.TabIndex = 2;
            Boton3.Text = "Boton3";
            Boton3.UseVisualStyleBackColor = true;
            Boton3.Click += Boton3_Click;
            // 
            // Mensaje
            // 
            Mensaje.AutoSize = true;
            Mensaje.Location = new Point(166, 32);
            Mensaje.Name = "Mensaje";
            Mensaje.Size = new Size(51, 15);
            Mensaje.TabIndex = 3;
            Mensaje.Text = "Mensaje";
            // 
            // FormMessageBox_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(392, 124);
            Controls.Add(Mensaje);
            Controls.Add(Boton3);
            Controls.Add(Boton2);
            Controls.Add(Boton1);
            Name = "FormMessageBox_234TL";
            Text = "FormMessageBox_234TL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button Boton1;
        private Button Boton2;
        private Button Boton3;
        private Label Mensaje;
    }
}