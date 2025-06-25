namespace GUI_234TL
{
    partial class FormPerfiles
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
            PermisosLabel = new Label();
            FamiliaLabel = new Label();
            ContenidoPerfiles = new Label();
            PermisosListbox = new ListBox();
            FamiliasTreeView = new TreeView();
            PerfilesTreeView = new TreeView();
            AgregarAFamiliaPermisoButton = new Button();
            AgregarAPerfilPermisoButton = new Button();
            EliminarDeFamiliaButton = new Button();
            EliminarDePerfilButton = new Button();
            EliminarFamiliaDefinitivoButton = new Button();
            EliminarPerfilDefinitivoButton = new Button();
            CrearFamiliaButton = new Button();
            CrearPerfilButton = new Button();
            NombreFamiliaLabel = new Label();
            NombrePerfilLabel = new Label();
            NombreFamiliaTextbox = new TextBox();
            NombrePerfilTextbox = new TextBox();
            AgregarAFamiliaFamiliaButton = new Button();
            AgregarAPerfilFamiliaButton = new Button();
            PermisoSeleccionadoLabel = new Label();
            FamiliaSeleccionadaLabel = new Label();
            PerfilSeleccionadoLabel = new Label();
            SeleccionarFamiliaButton = new Button();
            DeseleccionarFamiliaButton = new Button();
            SuspendLayout();
            // 
            // PermisosLabel
            // 
            PermisosLabel.AutoSize = true;
            PermisosLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PermisosLabel.Location = new Point(12, 12);
            PermisosLabel.Name = "PermisosLabel";
            PermisosLabel.Size = new Size(117, 32);
            PermisosLabel.TabIndex = 7;
            PermisosLabel.Text = "Permisos";
            // 
            // FamiliaLabel
            // 
            FamiliaLabel.AutoSize = true;
            FamiliaLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FamiliaLabel.Location = new Point(321, 12);
            FamiliaLabel.Name = "FamiliaLabel";
            FamiliaLabel.Size = new Size(105, 32);
            FamiliaLabel.TabIndex = 8;
            FamiliaLabel.Text = "Familias";
            // 
            // ContenidoPerfiles
            // 
            ContenidoPerfiles.AutoSize = true;
            ContenidoPerfiles.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ContenidoPerfiles.Location = new Point(870, 9);
            ContenidoPerfiles.Name = "ContenidoPerfiles";
            ContenidoPerfiles.Size = new Size(218, 32);
            ContenidoPerfiles.TabIndex = 11;
            ContenidoPerfiles.Text = "ContenidoPerfiles";
            // 
            // PermisosListbox
            // 
            PermisosListbox.FormattingEnabled = true;
            PermisosListbox.ItemHeight = 15;
            PermisosListbox.Location = new Point(9, 47);
            PermisosListbox.Name = "PermisosListbox";
            PermisosListbox.Size = new Size(182, 229);
            PermisosListbox.TabIndex = 12;
            PermisosListbox.SelectedIndexChanged += PermisosListbox_SelectedIndexChanged;
            // 
            // FamiliasTreeView
            // 
            FamiliasTreeView.Location = new Point(321, 47);
            FamiliasTreeView.Name = "FamiliasTreeView";
            FamiliasTreeView.Size = new Size(302, 341);
            FamiliasTreeView.TabIndex = 13;
            // 
            // PerfilesTreeView
            // 
            PerfilesTreeView.Location = new Point(870, 47);
            PerfilesTreeView.Name = "PerfilesTreeView";
            PerfilesTreeView.Size = new Size(302, 341);
            PerfilesTreeView.TabIndex = 14;
            PerfilesTreeView.AfterSelect += PerfilesTreeView_AfterSelect;
            // 
            // AgregarAFamiliaPermisoButton
            // 
            AgregarAFamiliaPermisoButton.Location = new Point(629, 86);
            AgregarAFamiliaPermisoButton.Name = "AgregarAFamiliaPermisoButton";
            AgregarAFamiliaPermisoButton.Size = new Size(142, 36);
            AgregarAFamiliaPermisoButton.TabIndex = 15;
            AgregarAFamiliaPermisoButton.Text = "AgregarAFamiliaPermiso";
            AgregarAFamiliaPermisoButton.UseVisualStyleBackColor = true;
            AgregarAFamiliaPermisoButton.Click += AgregarAFamiliaPermisoButton_Click;
            // 
            // AgregarAPerfilPermisoButton
            // 
            AgregarAPerfilPermisoButton.Location = new Point(1178, 86);
            AgregarAPerfilPermisoButton.Name = "AgregarAPerfilPermisoButton";
            AgregarAPerfilPermisoButton.Size = new Size(142, 36);
            AgregarAPerfilPermisoButton.TabIndex = 16;
            AgregarAPerfilPermisoButton.Text = "AgregarAPerfilPermiso";
            AgregarAPerfilPermisoButton.UseVisualStyleBackColor = true;
            AgregarAPerfilPermisoButton.Click += AgregarAPerfilPermisoButton_Click;
            // 
            // EliminarDeFamiliaButton
            // 
            EliminarDeFamiliaButton.Location = new Point(629, 128);
            EliminarDeFamiliaButton.Name = "EliminarDeFamiliaButton";
            EliminarDeFamiliaButton.Size = new Size(142, 36);
            EliminarDeFamiliaButton.TabIndex = 17;
            EliminarDeFamiliaButton.Text = "EliminarDeFamilia";
            EliminarDeFamiliaButton.UseVisualStyleBackColor = true;
            EliminarDeFamiliaButton.Click += EliminarDeFamiliaButton_Click;
            // 
            // EliminarDePerfilButton
            // 
            EliminarDePerfilButton.Location = new Point(1178, 128);
            EliminarDePerfilButton.Name = "EliminarDePerfilButton";
            EliminarDePerfilButton.Size = new Size(142, 36);
            EliminarDePerfilButton.TabIndex = 18;
            EliminarDePerfilButton.Text = "EliminarDePerfil";
            EliminarDePerfilButton.UseVisualStyleBackColor = true;
            EliminarDePerfilButton.Click += EliminarDePerfilButton_Click;
            // 
            // EliminarFamiliaDefinitivoButton
            // 
            EliminarFamiliaDefinitivoButton.Location = new Point(629, 170);
            EliminarFamiliaDefinitivoButton.Name = "EliminarFamiliaDefinitivoButton";
            EliminarFamiliaDefinitivoButton.Size = new Size(142, 36);
            EliminarFamiliaDefinitivoButton.TabIndex = 19;
            EliminarFamiliaDefinitivoButton.Text = "EliminarFamiliaDefinitivo";
            EliminarFamiliaDefinitivoButton.UseVisualStyleBackColor = true;
            EliminarFamiliaDefinitivoButton.Click += EliminarFamiliaDefinitivoButton_Click;
            // 
            // EliminarPerfilDefinitivoButton
            // 
            EliminarPerfilDefinitivoButton.Location = new Point(1178, 170);
            EliminarPerfilDefinitivoButton.Name = "EliminarPerfilDefinitivoButton";
            EliminarPerfilDefinitivoButton.Size = new Size(142, 36);
            EliminarPerfilDefinitivoButton.TabIndex = 20;
            EliminarPerfilDefinitivoButton.Text = "EliminarPerfilDefinitivo";
            EliminarPerfilDefinitivoButton.UseVisualStyleBackColor = true;
            EliminarPerfilDefinitivoButton.Click += EliminarPerfilDefinitivoButton_Click;
            // 
            // CrearFamiliaButton
            // 
            CrearFamiliaButton.Location = new Point(629, 212);
            CrearFamiliaButton.Name = "CrearFamiliaButton";
            CrearFamiliaButton.Size = new Size(142, 36);
            CrearFamiliaButton.TabIndex = 21;
            CrearFamiliaButton.Text = "CrearFamilia";
            CrearFamiliaButton.UseVisualStyleBackColor = true;
            CrearFamiliaButton.Click += CrearFamiliaButton_Click;
            // 
            // CrearPerfilButton
            // 
            CrearPerfilButton.Location = new Point(1178, 212);
            CrearPerfilButton.Name = "CrearPerfilButton";
            CrearPerfilButton.Size = new Size(142, 36);
            CrearPerfilButton.TabIndex = 22;
            CrearPerfilButton.Text = "CrearPerfil";
            CrearPerfilButton.UseVisualStyleBackColor = true;
            CrearPerfilButton.Click += CrearPerfilButton_Click;
            // 
            // NombreFamiliaLabel
            // 
            NombreFamiliaLabel.AutoSize = true;
            NombreFamiliaLabel.Location = new Point(629, 251);
            NombreFamiliaLabel.Name = "NombreFamiliaLabel";
            NombreFamiliaLabel.Size = new Size(89, 15);
            NombreFamiliaLabel.TabIndex = 23;
            NombreFamiliaLabel.Text = "NombreFamilia";
            // 
            // NombrePerfilLabel
            // 
            NombrePerfilLabel.AutoSize = true;
            NombrePerfilLabel.Location = new Point(1178, 251);
            NombrePerfilLabel.Name = "NombrePerfilLabel";
            NombrePerfilLabel.Size = new Size(78, 15);
            NombrePerfilLabel.TabIndex = 24;
            NombrePerfilLabel.Text = "NombrePerfil";
            // 
            // NombreFamiliaTextbox
            // 
            NombreFamiliaTextbox.Location = new Point(629, 269);
            NombreFamiliaTextbox.Name = "NombreFamiliaTextbox";
            NombreFamiliaTextbox.Size = new Size(100, 23);
            NombreFamiliaTextbox.TabIndex = 25;
            // 
            // NombrePerfilTextbox
            // 
            NombrePerfilTextbox.Location = new Point(1178, 269);
            NombrePerfilTextbox.Name = "NombrePerfilTextbox";
            NombrePerfilTextbox.Size = new Size(100, 23);
            NombrePerfilTextbox.TabIndex = 26;
            // 
            // AgregarAFamiliaFamiliaButton
            // 
            AgregarAFamiliaFamiliaButton.Location = new Point(629, 44);
            AgregarAFamiliaFamiliaButton.Name = "AgregarAFamiliaFamiliaButton";
            AgregarAFamiliaFamiliaButton.Size = new Size(142, 36);
            AgregarAFamiliaFamiliaButton.TabIndex = 27;
            AgregarAFamiliaFamiliaButton.Text = "AgregarAFamiliaFamilia";
            AgregarAFamiliaFamiliaButton.UseVisualStyleBackColor = true;
            AgregarAFamiliaFamiliaButton.Click += AgregarAFamiliaFamiliaButton_Click;
            // 
            // AgregarAPerfilFamiliaButton
            // 
            AgregarAPerfilFamiliaButton.Location = new Point(1178, 47);
            AgregarAPerfilFamiliaButton.Name = "AgregarAPerfilFamiliaButton";
            AgregarAPerfilFamiliaButton.Size = new Size(142, 36);
            AgregarAPerfilFamiliaButton.TabIndex = 28;
            AgregarAPerfilFamiliaButton.Text = "AgregarAPerfilFamilia";
            AgregarAPerfilFamiliaButton.UseVisualStyleBackColor = true;
            AgregarAPerfilFamiliaButton.Click += AgregarAPerfilFamiliaButton_Click;
            // 
            // PermisoSeleccionadoLabel
            // 
            PermisoSeleccionadoLabel.AutoSize = true;
            PermisoSeleccionadoLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PermisoSeleccionadoLabel.Location = new Point(9, 279);
            PermisoSeleccionadoLabel.Name = "PermisoSeleccionadoLabel";
            PermisoSeleccionadoLabel.Size = new Size(99, 20);
            PermisoSeleccionadoLabel.TabIndex = 29;
            PermisoSeleccionadoLabel.Text = "Seleccionado";
            // 
            // FamiliaSeleccionadaLabel
            // 
            FamiliaSeleccionadaLabel.AutoSize = true;
            FamiliaSeleccionadaLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            FamiliaSeleccionadaLabel.Location = new Point(321, 391);
            FamiliaSeleccionadaLabel.Name = "FamiliaSeleccionadaLabel";
            FamiliaSeleccionadaLabel.Size = new Size(99, 20);
            FamiliaSeleccionadaLabel.TabIndex = 30;
            FamiliaSeleccionadaLabel.Text = "Seleccionado";
            // 
            // PerfilSeleccionadoLabel
            // 
            PerfilSeleccionadoLabel.AutoSize = true;
            PerfilSeleccionadoLabel.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PerfilSeleccionadoLabel.Location = new Point(870, 391);
            PerfilSeleccionadoLabel.Name = "PerfilSeleccionadoLabel";
            PerfilSeleccionadoLabel.Size = new Size(99, 20);
            PerfilSeleccionadoLabel.TabIndex = 31;
            PerfilSeleccionadoLabel.Text = "Seleccionado";
            // 
            // SeleccionarFamiliaButton
            // 
            SeleccionarFamiliaButton.Location = new Point(629, 298);
            SeleccionarFamiliaButton.Name = "SeleccionarFamiliaButton";
            SeleccionarFamiliaButton.Size = new Size(142, 36);
            SeleccionarFamiliaButton.TabIndex = 32;
            SeleccionarFamiliaButton.Text = "SeleccionarFamilia";
            SeleccionarFamiliaButton.UseVisualStyleBackColor = true;
            SeleccionarFamiliaButton.Click += SeleccionarFamiliaButton_Click;
            // 
            // DeseleccionarFamiliaButton
            // 
            DeseleccionarFamiliaButton.Location = new Point(629, 340);
            DeseleccionarFamiliaButton.Name = "DeseleccionarFamiliaButton";
            DeseleccionarFamiliaButton.Size = new Size(142, 36);
            DeseleccionarFamiliaButton.TabIndex = 33;
            DeseleccionarFamiliaButton.Text = "DeseleccionarFamilia";
            DeseleccionarFamiliaButton.UseVisualStyleBackColor = true;
            DeseleccionarFamiliaButton.Click += DeseleccionarFamiliaButton_Click;
            // 
            // FormPerfiles
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1337, 492);
            Controls.Add(DeseleccionarFamiliaButton);
            Controls.Add(SeleccionarFamiliaButton);
            Controls.Add(PerfilSeleccionadoLabel);
            Controls.Add(FamiliaSeleccionadaLabel);
            Controls.Add(PermisoSeleccionadoLabel);
            Controls.Add(AgregarAPerfilFamiliaButton);
            Controls.Add(AgregarAFamiliaFamiliaButton);
            Controls.Add(NombrePerfilTextbox);
            Controls.Add(NombreFamiliaTextbox);
            Controls.Add(NombrePerfilLabel);
            Controls.Add(NombreFamiliaLabel);
            Controls.Add(CrearPerfilButton);
            Controls.Add(CrearFamiliaButton);
            Controls.Add(EliminarPerfilDefinitivoButton);
            Controls.Add(EliminarFamiliaDefinitivoButton);
            Controls.Add(EliminarDePerfilButton);
            Controls.Add(EliminarDeFamiliaButton);
            Controls.Add(AgregarAPerfilPermisoButton);
            Controls.Add(AgregarAFamiliaPermisoButton);
            Controls.Add(PerfilesTreeView);
            Controls.Add(FamiliasTreeView);
            Controls.Add(PermisosListbox);
            Controls.Add(ContenidoPerfiles);
            Controls.Add(FamiliaLabel);
            Controls.Add(PermisosLabel);
            Name = "FormPerfiles";
            Text = "OrdenesLabel";
            FormClosed += FormPerfiles_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label PermisosLabel;
        private Label FamiliaLabel;
        private Label ContenidoPerfiles;
        private ListBox PermisosListbox;
        private TreeView FamiliasTreeView;
        private TreeView PerfilesTreeView;
        private Button AgregarAFamiliaPermisoButton;
        private Button AgregarAPerfilPermisoButton;
        private Button EliminarDeFamiliaButton;
        private Button EliminarDePerfilButton;
        private Button EliminarFamiliaDefinitivoButton;
        private Button EliminarPerfilDefinitivoButton;
        private Button CrearFamiliaButton;
        private Button CrearPerfilButton;
        private Label NombreFamiliaLabel;
        private Label NombrePerfilLabel;
        private TextBox NombreFamiliaTextbox;
        private TextBox NombrePerfilTextbox;
        private Button AgregarAFamiliaFamiliaButton;
        private Button AgregarAPerfilFamiliaButton;
        private Label PermisoSeleccionadoLabel;
        private Label FamiliaSeleccionadaLabel;
        private Label PerfilSeleccionadoLabel;
        private Button SeleccionarFamiliaButton;
        private Button DeseleccionarFamiliaButton;
    }
}