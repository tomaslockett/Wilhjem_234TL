namespace GUI_234TL
{
    partial class FormCambiarContraseña_234TL
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
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            Ingresarbutton = new Button();
            MostrarButton = new CheckBox();
            MostarButton2 = new CheckBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(122, 91);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(275, 23);
            textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(122, 156);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(275, 23);
            textBox2.TabIndex = 1;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(122, 220);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(275, 23);
            textBox3.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(149, 36);
            label1.Name = "label1";
            label1.Size = new Size(210, 30);
            label1.TabIndex = 3;
            label1.Text = "Cambiar Contraseña";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 91);
            label2.Name = "label2";
            label2.Size = new Size(104, 15);
            label2.TabIndex = 4;
            label2.Text = "Contraseña Actual";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 159);
            label3.Name = "label3";
            label3.Size = new Size(104, 15);
            label3.TabIndex = 5;
            label3.Text = "Contraseña Nueva";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 228);
            label4.Name = "label4";
            label4.Size = new Size(104, 15);
            label4.TabIndex = 6;
            label4.Text = "Contraseña Nueva";
            // 
            // Ingresarbutton
            // 
            Ingresarbutton.Location = new Point(188, 302);
            Ingresarbutton.Name = "Ingresarbutton";
            Ingresarbutton.Size = new Size(134, 32);
            Ingresarbutton.TabIndex = 9;
            Ingresarbutton.Text = "Ingresar";
            Ingresarbutton.UseVisualStyleBackColor = true;
            Ingresarbutton.Click += Ingresarbutton_Click;
            // 
            // MostrarButton
            // 
            MostrarButton.AutoSize = true;
            MostrarButton.Location = new Point(403, 158);
            MostrarButton.Name = "MostrarButton";
            MostrarButton.Size = new Size(67, 19);
            MostrarButton.TabIndex = 12;
            MostrarButton.Text = "Mostrar";
            MostrarButton.UseVisualStyleBackColor = true;
            MostrarButton.CheckedChanged += MostrarButton_CheckedChanged;
            // 
            // MostarButton2
            // 
            MostarButton2.AutoSize = true;
            MostarButton2.Location = new Point(403, 222);
            MostarButton2.Name = "MostarButton2";
            MostarButton2.Size = new Size(63, 19);
            MostarButton2.TabIndex = 13;
            MostarButton2.Text = "Mostar";
            MostarButton2.UseVisualStyleBackColor = true;
            MostarButton2.CheckedChanged += MostarButton2_CheckedChanged;
            // 
            // FormCambiarContraseña_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(536, 422);
            Controls.Add(MostarButton2);
            Controls.Add(MostrarButton);
            Controls.Add(Ingresarbutton);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "FormCambiarContraseña_234TL";
            Text = "CambiarContraseña_234TL";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button Ingresarbutton;
        private CheckBox MostrarButton;
        private CheckBox MostarButton2;
    }
}