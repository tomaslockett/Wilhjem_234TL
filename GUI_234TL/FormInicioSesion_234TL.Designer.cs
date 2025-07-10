namespace GUI_234TL
{
    partial class FormInicioSesion_234TL
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
            IngresarButton = new Button();
            VolverButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label4 = new Label();
            label5 = new Label();
            MostrarButton = new CheckBox();
            SuspendLayout();
            // 
            // IngresarButton
            // 
            IngresarButton.Location = new Point(143, 166);
            IngresarButton.Name = "IngresarButton";
            IngresarButton.Size = new Size(75, 23);
            IngresarButton.TabIndex = 0;
            IngresarButton.Text = "Ingresar";
            IngresarButton.UseVisualStyleBackColor = true;
            IngresarButton.Click += IngresarButton_Click;
            // 
            // VolverButton
            // 
            VolverButton.Location = new Point(316, 187);
            VolverButton.Name = "VolverButton";
            VolverButton.Size = new Size(75, 23);
            VolverButton.TabIndex = 1;
            VolverButton.Text = "Volver";
            VolverButton.UseVisualStyleBackColor = true;
            VolverButton.Click += VolverButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 76);
            label1.Name = "label1";
            label1.Size = new Size(37, 15);
            label1.TabIndex = 2;
            label1.Text = "Login";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(7, 124);
            label2.Name = "label2";
            label2.Size = new Size(59, 15);
            label2.TabIndex = 3;
            label2.Text = "PassWord";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(106, 9);
            label3.Name = "label3";
            label3.Size = new Size(144, 30);
            label3.TabIndex = 4;
            label3.Text = "Iniciar Sesion";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(72, 73);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(219, 23);
            textBox1.TabIndex = 5;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(72, 120);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(219, 23);
            textBox2.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(72, 55);
            label4.Name = "label4";
            label4.Size = new Size(109, 15);
            label4.TabIndex = 8;
            label4.Text = "Nombre + Apellido";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(72, 99);
            label5.Name = "label5";
            label5.Size = new Size(196, 15);
            label5.TabIndex = 9;
            label5.Text = "Apellido + 3 Ultimos Digitos Del Dni";
            label5.Click += label5_Click;
            // 
            // MostrarButton
            // 
            MostrarButton.AutoSize = true;
            MostrarButton.Location = new Point(297, 124);
            MostrarButton.Name = "MostrarButton";
            MostrarButton.Size = new Size(67, 19);
            MostrarButton.TabIndex = 10;
            MostrarButton.Text = "Mostrar";
            MostrarButton.UseVisualStyleBackColor = true;
            MostrarButton.CheckedChanged += MostrarButton_CheckedChanged;
            // 
            // FormInicioSesion_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(394, 222);
            Controls.Add(MostrarButton);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(VolverButton);
            Controls.Add(IngresarButton);
            Name = "FormInicioSesion_234TL";
            Text = "Iniciar Sesion";
            FormClosed += FormInicioSesion_234TL_FormClosed;
            Load += FormInicioSesion_234TL_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button IngresarButton;
        private Button VolverButton;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label4;
        private Label label5;
        private CheckBox MostrarButton;
    }
}