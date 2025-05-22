namespace GUI_234TL
{
    partial class FormUsuarios_234TL
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
            label5 = new Label();
            RolcomboBox = new ComboBox();
            EmailtextBox = new TextBox();
            label4 = new Label();
            ApellidotextBox = new TextBox();
            label3 = new Label();
            NombretextBox = new TextBox();
            label2 = new Label();
            DNItextBox = new TextBox();
            label1 = new Label();
            ConsultaButton = new Button();
            AplicarButton = new Button();
            ActDesactButton = new Button();
            ModificarButton = new Button();
            DesbloquearButton = new Button();
            CrearButton = new Button();
            TodosradioButton = new RadioButton();
            radioButtonActivo = new RadioButton();
            dataGridViewUsuarios = new DataGridView();
            Modo = new Label();
            label9 = new Label();
            Eliminarbutton = new Button();
            VerradioButton = new RadioButton();
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(119, 449);
            label5.Margin = new Padding(4, 0, 4, 0);
            label5.Name = "label5";
            label5.Size = new Size(45, 30);
            label5.TabIndex = 45;
            label5.Text = "Rol";
            // 
            // RolcomboBox
            // 
            RolcomboBox.FormattingEnabled = true;
            RolcomboBox.Items.AddRange(new object[] { "Usuario", "admin" });
            RolcomboBox.Location = new Point(172, 456);
            RolcomboBox.Margin = new Padding(4, 3, 4, 3);
            RolcomboBox.Name = "RolcomboBox";
            RolcomboBox.Size = new Size(219, 23);
            RolcomboBox.TabIndex = 44;
            // 
            // EmailtextBox
            // 
            EmailtextBox.Location = new Point(172, 418);
            EmailtextBox.Margin = new Padding(4, 3, 4, 3);
            EmailtextBox.Name = "EmailtextBox";
            EmailtextBox.Size = new Size(219, 23);
            EmailtextBox.TabIndex = 43;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(98, 413);
            label4.Margin = new Padding(4, 0, 4, 0);
            label4.Name = "label4";
            label4.Size = new Size(66, 30);
            label4.TabIndex = 42;
            label4.Text = "Email";
            // 
            // ApellidotextBox
            // 
            ApellidotextBox.Location = new Point(172, 389);
            ApellidotextBox.Margin = new Padding(4, 3, 4, 3);
            ApellidotextBox.Name = "ApellidotextBox";
            ApellidotextBox.Size = new Size(219, 23);
            ApellidotextBox.TabIndex = 41;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(70, 353);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(94, 30);
            label3.TabIndex = 40;
            label3.Text = "Nombre";
            // 
            // NombretextBox
            // 
            NombretextBox.Location = new Point(172, 360);
            NombretextBox.Margin = new Padding(4, 3, 4, 3);
            NombretextBox.Name = "NombretextBox";
            NombretextBox.Size = new Size(219, 23);
            NombretextBox.TabIndex = 39;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(68, 383);
            label2.Margin = new Padding(4, 0, 4, 0);
            label2.Name = "label2";
            label2.Size = new Size(96, 30);
            label2.TabIndex = 38;
            label2.Text = "Apellido";
            // 
            // DNItextBox
            // 
            DNItextBox.Location = new Point(172, 322);
            DNItextBox.Margin = new Padding(4, 3, 4, 3);
            DNItextBox.Name = "DNItextBox";
            DNItextBox.Size = new Size(219, 23);
            DNItextBox.TabIndex = 37;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(112, 315);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(52, 30);
            label1.TabIndex = 36;
            label1.Text = "DNI";
            // 
            // ConsultaButton
            // 
            ConsultaButton.Location = new Point(1119, 51);
            ConsultaButton.Margin = new Padding(4, 3, 4, 3);
            ConsultaButton.Name = "ConsultaButton";
            ConsultaButton.Size = new Size(189, 66);
            ConsultaButton.TabIndex = 35;
            ConsultaButton.Text = "Modo Consulta";
            ConsultaButton.UseVisualStyleBackColor = true;
            ConsultaButton.Click += ConsultaButton_Click;
            // 
            // AplicarButton
            // 
            AplicarButton.Location = new Point(1119, 485);
            AplicarButton.Margin = new Padding(4, 3, 4, 3);
            AplicarButton.Name = "AplicarButton";
            AplicarButton.Size = new Size(189, 66);
            AplicarButton.TabIndex = 33;
            AplicarButton.Text = "Aplicar";
            AplicarButton.UseVisualStyleBackColor = true;
            AplicarButton.Click += AplicarButton_Click;
            // 
            // ActDesactButton
            // 
            ActDesactButton.Location = new Point(1119, 413);
            ActDesactButton.Margin = new Padding(4, 3, 4, 3);
            ActDesactButton.Name = "ActDesactButton";
            ActDesactButton.Size = new Size(189, 66);
            ActDesactButton.TabIndex = 32;
            ActDesactButton.Text = "Act/Desact";
            ActDesactButton.UseVisualStyleBackColor = true;
            ActDesactButton.Click += ActDesactButton_Click;
            // 
            // ModificarButton
            // 
            ModificarButton.Location = new Point(1119, 268);
            ModificarButton.Margin = new Padding(4, 3, 4, 3);
            ModificarButton.Name = "ModificarButton";
            ModificarButton.Size = new Size(189, 66);
            ModificarButton.TabIndex = 31;
            ModificarButton.Text = "Modificar";
            ModificarButton.UseVisualStyleBackColor = true;
            ModificarButton.Click += ModificarButton_Click;
            // 
            // DesbloquearButton
            // 
            DesbloquearButton.Location = new Point(1119, 195);
            DesbloquearButton.Margin = new Padding(4, 3, 4, 3);
            DesbloquearButton.Name = "DesbloquearButton";
            DesbloquearButton.Size = new Size(189, 66);
            DesbloquearButton.TabIndex = 30;
            DesbloquearButton.Text = "Desbloquear";
            DesbloquearButton.UseVisualStyleBackColor = true;
            DesbloquearButton.Click += DesbloquearButton_Click;
            // 
            // CrearButton
            // 
            CrearButton.Location = new Point(1119, 123);
            CrearButton.Margin = new Padding(4, 3, 4, 3);
            CrearButton.Name = "CrearButton";
            CrearButton.Size = new Size(189, 66);
            CrearButton.TabIndex = 29;
            CrearButton.Text = "Crear";
            CrearButton.UseVisualStyleBackColor = true;
            CrearButton.Click += CrearButton_Click;
            // 
            // TodosradioButton
            // 
            TodosradioButton.AutoSize = true;
            TodosradioButton.Location = new Point(525, 30);
            TodosradioButton.Margin = new Padding(4, 3, 4, 3);
            TodosradioButton.Name = "TodosradioButton";
            TodosradioButton.Size = new Size(57, 19);
            TodosradioButton.TabIndex = 28;
            TodosradioButton.TabStop = true;
            TodosradioButton.Text = "Todos";
            TodosradioButton.UseVisualStyleBackColor = true;
            TodosradioButton.CheckedChanged += TodosradioButton_CheckedChanged;
            // 
            // radioButtonActivo
            // 
            radioButtonActivo.AutoSize = true;
            radioButtonActivo.Location = new Point(458, 29);
            radioButtonActivo.Margin = new Padding(4, 3, 4, 3);
            radioButtonActivo.Name = "radioButtonActivo";
            radioButtonActivo.Size = new Size(59, 19);
            radioButtonActivo.TabIndex = 27;
            radioButtonActivo.TabStop = true;
            radioButtonActivo.Text = "Activo";
            radioButtonActivo.UseVisualStyleBackColor = true;
            radioButtonActivo.CheckedChanged += radioButtonActivo_CheckedChanged;
            // 
            // dataGridViewUsuarios
            // 
            dataGridViewUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewUsuarios.Location = new Point(28, 69);
            dataGridViewUsuarios.Margin = new Padding(4, 3, 4, 3);
            dataGridViewUsuarios.Name = "dataGridViewUsuarios";
            dataGridViewUsuarios.Size = new Size(1006, 243);
            dataGridViewUsuarios.TabIndex = 26;
            dataGridViewUsuarios.SelectionChanged += dataGridViewUsuarios_SelectionChanged;
            // 
            // Modo
            // 
            Modo.AutoSize = true;
            Modo.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Modo.Location = new Point(831, 16);
            Modo.Name = "Modo";
            Modo.Size = new Size(96, 37);
            Modo.TabIndex = 53;
            Modo.Text = "label9";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(28, 29);
            label9.Name = "label9";
            label9.Size = new Size(272, 37);
            label9.TabIndex = 54;
            label9.Text = "Gestion de Usuarios";
            // 
            // Eliminarbutton
            // 
            Eliminarbutton.Location = new Point(1119, 340);
            Eliminarbutton.Margin = new Padding(4, 3, 4, 3);
            Eliminarbutton.Name = "Eliminarbutton";
            Eliminarbutton.Size = new Size(189, 66);
            Eliminarbutton.TabIndex = 55;
            Eliminarbutton.Text = "Eliminar";
            Eliminarbutton.UseVisualStyleBackColor = true;
            Eliminarbutton.Click += Eliminarbutton_Click;
            // 
            // VerradioButton
            // 
            VerradioButton.AutoSize = true;
            VerradioButton.Location = new Point(626, 32);
            VerradioButton.Margin = new Padding(4, 3, 4, 3);
            VerradioButton.Name = "VerradioButton";
            VerradioButton.Size = new Size(137, 19);
            VerradioButton.TabIndex = 56;
            VerradioButton.TabStop = true;
            VerradioButton.Text = "Ver Atributos Ocultos";
            VerradioButton.UseVisualStyleBackColor = true;
            VerradioButton.CheckedChanged += VerradioButton_CheckedChanged;
            // 
            // FormUsuarios_234TL
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1368, 594);
            Controls.Add(VerradioButton);
            Controls.Add(Eliminarbutton);
            Controls.Add(label9);
            Controls.Add(Modo);
            Controls.Add(label5);
            Controls.Add(RolcomboBox);
            Controls.Add(EmailtextBox);
            Controls.Add(label4);
            Controls.Add(ApellidotextBox);
            Controls.Add(label3);
            Controls.Add(NombretextBox);
            Controls.Add(label2);
            Controls.Add(DNItextBox);
            Controls.Add(label1);
            Controls.Add(ConsultaButton);
            Controls.Add(AplicarButton);
            Controls.Add(ActDesactButton);
            Controls.Add(ModificarButton);
            Controls.Add(DesbloquearButton);
            Controls.Add(CrearButton);
            Controls.Add(TodosradioButton);
            Controls.Add(radioButtonActivo);
            Controls.Add(dataGridViewUsuarios);
            Name = "FormUsuarios_234TL";
            Text = "FormUsuarios_234TL";
            ((System.ComponentModel.ISupportInitialize)dataGridViewUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private ComboBox RolcomboBox;
        private TextBox EmailtextBox;
        private Label label4;
        private TextBox ApellidotextBox;
        private Label label3;
        private TextBox NombretextBox;
        private Label label2;
        private TextBox DNItextBox;
        private Label label1;
        private Button ConsultaButton;
        private Button AplicarButton;
        private Button ActDesactButton;
        private Button ModificarButton;
        private Button DesbloquearButton;
        private Button CrearButton;
        private RadioButton TodosradioButton;
        private RadioButton radioButtonActivo;
        private DataGridView dataGridViewUsuarios;
        private Label Modo;
        private Label label9;
        private Button Eliminarbutton;
        private RadioButton VerradioButton;
    }
}