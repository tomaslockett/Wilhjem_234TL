namespace GUI_234TL
{
    partial class FormPrincipal_234TL
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal_234TL));
            flowLayoutPanel1 = new FlowLayoutPanel();
            SesionButton = new Button();
            IniciarSesionButton = new Button();
            CerrarSesionButton = new Button();
            CambiarContraseñabutton = new Button();
            GestionUsuariobutton = new Button();
            PerfilesButton = new Button();
            button2 = new Button();
            pictureBox1 = new PictureBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(SesionButton);
            flowLayoutPanel1.Controls.Add(IniciarSesionButton);
            flowLayoutPanel1.Controls.Add(CerrarSesionButton);
            flowLayoutPanel1.Controls.Add(CambiarContraseñabutton);
            flowLayoutPanel1.Controls.Add(GestionUsuariobutton);
            flowLayoutPanel1.Controls.Add(PerfilesButton);
            flowLayoutPanel1.Controls.Add(button2);
            flowLayoutPanel1.Location = new Point(12, 12);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(238, 564);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // SesionButton
            // 
            SesionButton.Location = new Point(3, 3);
            SesionButton.Name = "SesionButton";
            SesionButton.Size = new Size(176, 38);
            SesionButton.TabIndex = 1;
            SesionButton.Text = "Sesion";
            SesionButton.UseVisualStyleBackColor = true;
            SesionButton.Click += SesionButton_Click;
            SesionButton.MouseLeave += SesionButton_MouseLeave;
            SesionButton.MouseHover += SesionButton_MouseHover;
            // 
            // IniciarSesionButton
            // 
            IniciarSesionButton.Location = new Point(3, 47);
            IniciarSesionButton.Name = "IniciarSesionButton";
            IniciarSesionButton.Size = new Size(176, 38);
            IniciarSesionButton.TabIndex = 2;
            IniciarSesionButton.Text = "Iniciar Sesion";
            IniciarSesionButton.UseVisualStyleBackColor = true;
            IniciarSesionButton.Click += IniciarSesionButton_Click;
            IniciarSesionButton.MouseLeave += IniciarSesionButton_MouseLeave;
            IniciarSesionButton.MouseHover += IniciarSesionButton_MouseHover;
            // 
            // CerrarSesionButton
            // 
            CerrarSesionButton.Location = new Point(3, 91);
            CerrarSesionButton.Name = "CerrarSesionButton";
            CerrarSesionButton.Size = new Size(176, 38);
            CerrarSesionButton.TabIndex = 3;
            CerrarSesionButton.Text = "Cerrar Sesion";
            CerrarSesionButton.UseVisualStyleBackColor = true;
            CerrarSesionButton.Click += CerrarSesionButton_Click;
            CerrarSesionButton.MouseLeave += CerrarSesionButton_MouseLeave;
            CerrarSesionButton.MouseHover += CerrarSesionButton_MouseHover;
            // 
            // CambiarContraseñabutton
            // 
            CambiarContraseñabutton.Location = new Point(3, 135);
            CambiarContraseñabutton.Name = "CambiarContraseñabutton";
            CambiarContraseñabutton.Size = new Size(176, 38);
            CambiarContraseñabutton.TabIndex = 2;
            CambiarContraseñabutton.Text = "Cambiar Contraseña";
            CambiarContraseñabutton.UseVisualStyleBackColor = true;
            CambiarContraseñabutton.Click += CambiarContraseñabutton_Click;
            CambiarContraseñabutton.MouseLeave += CambiarContraseñabutton_MouseLeave;
            CambiarContraseñabutton.MouseHover += CambiarContraseñabutton_MouseHover;
            // 
            // GestionUsuariobutton
            // 
            GestionUsuariobutton.Location = new Point(3, 179);
            GestionUsuariobutton.Name = "GestionUsuariobutton";
            GestionUsuariobutton.Size = new Size(176, 38);
            GestionUsuariobutton.TabIndex = 5;
            GestionUsuariobutton.Text = "Gestion Usuario";
            GestionUsuariobutton.UseVisualStyleBackColor = true;
            GestionUsuariobutton.Click += GestionUsuariobutton_Click;
            GestionUsuariobutton.MouseLeave += GestionUsuariobutton_MouseLeave;
            GestionUsuariobutton.MouseHover += GestionUsuariobutton_MouseHover;
            // 
            // PerfilesButton
            // 
            PerfilesButton.Location = new Point(3, 223);
            PerfilesButton.Name = "PerfilesButton";
            PerfilesButton.Size = new Size(176, 38);
            PerfilesButton.TabIndex = 4;
            PerfilesButton.Text = "Perfiles";
            PerfilesButton.UseVisualStyleBackColor = true;
            PerfilesButton.Click += PerfilesButton_Click;
            PerfilesButton.MouseLeave += PerfilesButton_MouseLeave;
            PerfilesButton.MouseHover += PerfilesButton_MouseHover;
            // 
            // button2
            // 
            button2.Location = new Point(3, 267);
            button2.Name = "button2";
            button2.Size = new Size(176, 38);
            button2.TabIndex = 6;
            button2.Text = ".";
            button2.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1348, 441);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(266, 245);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 676);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1626, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(118, 17);
            toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FormPrincipal_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1626, 698);
            Controls.Add(statusStrip1);
            Controls.Add(pictureBox1);
            Controls.Add(flowLayoutPanel1);
            Name = "FormPrincipal_234TL";
            Text = "Form1";
            flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button SesionButton;
        private Button IniciarSesionButton;
        private Button CerrarSesionButton;
        private Button PerfilesButton;
        private Button CambiarContraseñabutton;
        private Button GestionUsuariobutton;
        private Button button2;
        private PictureBox pictureBox1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
    }
}
