using Servicios_234TL.Observer_234TL.Traducciones_234TL;

namespace Servicios_234TL.Observer_234TL.Traducciones_234TL
{
    public class TraduccionesClase_234TL
    {
        public Forms_234TL Forms { get; set; }
        public TraduccionesMensajes_234TL Messages { get; set; } 
        public TraduccionesCOMUNES_234TL Comunes { get; set; }
        public TraduccionModos_234TL Modos { get; set; }
        public TraduccionesEnums_234TL Enums { get; set; }
    }

    public class Forms_234TL
    {
        public FormCobrarDiagnostico_234TL CobrarDiagnostico { get; set; }
        public FormFacturaYComprobatne_234TL FacturaYComprobante { get; set; }
        public FormIngresoEquipo_234TL IngresoEquipo { get; set; }
        public FormOrdenIngreso_234TL OrdenIngreso { get; set; }
        public FormRegistroCliente_234TL RegistroCliente { get; set; }
        public FormCambiarContraseña_234TL CambiarContraseña { get; set; }
        public FormIniciaSesion_234TL IniciaSesion { get; set; }
        public FormPerfiles_234TL Perfiles { get; set; }
        public FormPrincipal_234TL Principal { get; set; }
        public FormCambiarIdioma_234TL CambiarIdioma { get; set; }
        public FormUsuarios_234TL Usuarios { get; set; }
        public FormTecnicos_234TL FormTecnicos { get; set; }
        public FormPdf_234TL FormPdf { get; set; }
    }

    public class TraduccionModos_234TL
    {
        public string ModoConsulta { get; set; }
        public string ModoCrear { get; set; }
        public string ModoModificar { get; set; }
        public string ModoEliminar { get; set; }
        public string ModoDesbloquear { get; set; }
    }

    public class TraduccionesEnums_234TL
    {
        public Dictionary<string, string> EstadoEquipo { get; set; }
        public Dictionary<string, string> EstadoReparacion { get; set; }
        public Dictionary<string, string> Especialidad { get; set; }
        public Dictionary<string, string> OpcionBool { get; set; }

        public Dictionary<string, string> MarcaEquipos { get; set; }
    }


    #region Guis
    public class TraduccionesForm_234TL
    {
        public string Title { get; set; }
    }

    public class FormTecnicos_234TL : TraduccionesForm_234TL
    {
        public string Label_Titulo { get; set; }
        public string Label_DNI { get; set; }
        public string Label_Nombre { get; set; }
        public string Label_Apellido { get; set; }
        public string Label_Telefono { get; set; }
        public string Label_Especialidad { get; set; }
        public string Label_Disponible { get; set; }
        public string Boton_Agregar { get; set; }
        public string Boton_Modificar { get; set; }
        public string Boton_Eliminar { get; set; }
        public string Columna_DNI { get; set; }
        public string Columna_Nombre { get; set; }
        public string Columna_Apellido { get; set; }
        public string Columna_Especialidad { get; set; }
        public string Columna_Disponible { get; set; }
    }

    public class FormCobrarDiagnostico_234TL : TraduccionesForm_234TL
    {
        public string Label_NumeroTarjeta { get; set; }
        public string Label_CodigoSeguridad { get; set; }
        public string Label_Vencimiento { get; set; }

        public string CobrarBotton { get; set; }

        public string Columna_NumeroReparacion { get; set; }
        public string Columna_Estado { get; set; }
        public string Columna_NumeroSerie { get; set; }
        public string Columna_NombreCliente { get; set; }
        public string Columna_DNICliente { get; set; }
        public string Columna_NombreTecnico { get; set; }
        public string Columna_EspecialidadTecnico { get; set; }
        public string Columna_Cobrado { get; set; }
        public string Columna_FacturaGenerada { get; set; }
        public string Columna_ComprobanteGenerado { get; set; }
    }


    public class FormFacturaYComprobatne_234TL : TraduccionesForm_234TL
    {
        public string CrearFacturaBotton { get; set; }
        public string CrearComprobanteBotton { get; set; }
        public string CobrarDiagnosticoBotton { get; set; }

        public string Columna_NumeroReparacion { get; set; }
        public string Columna_Estado { get; set; }
        public string Columna_NumeroSerie { get; set; }
        public string Columna_NombreCliente { get; set; }
        public string Columna_DNICliente { get; set; }
        public string Columna_NombreTecnico { get; set; }
        public string Columna_EspecialidadTecnico { get; set; }
        public string Columna_Cobrado { get; set; }
        public string Columna_FacturaGenerada { get; set; }
        public string Columna_ComprobanteGenerado { get; set; }
    }

    public class FormIngresoEquipo_234TL : TraduccionesForm_234TL
    {
        public string Label_NumeroSerie { get; set; }
        public string Label_Marca { get; set; }
        public string Label_Modelo { get; set; }
        public string Label_Estado { get; set; }
        public string Label_Falla { get; set; }
        public string Label_Desarmado { get; set; }
        public string Label_DañoVisible { get; set; }

        public string IngresarBotton { get; set; }
        public string EliminarBotton { get; set; }

        public string Columna_NumeroSerie { get; set; }
        public string Columna_Marca { get; set; }
        public string Columna_Modelo { get; set; }
        public string Columna_Estado { get; set; }
        public string Columna_Falla { get; set; }
        public string Columna_Desarmado { get; set; }
        public string Columna_DañoVisible { get; set; }
        public string Columna_FechaIngreso { get; set; }
    }
    public class FormOrdenIngreso_234TL : TraduccionesForm_234TL
    {
        public string Label_Clientes { get; set; }
        public string Label_Equipos { get; set; }
        public string Label_Tecnicos { get; set; }
        public string Label_Ordenes { get; set; }
        public string Label_Estado { get; set; }

        public string Label_ClienteSeleccionado { get; set; }
        public string Label_EquipoSeleccionado { get; set; }
        public string Label_TecnicoSeleccionado { get; set; }
        public string Label_OrdenSeleccionada { get; set; }

        public string NuevoEquipoBotton { get; set; }
        public string NuevoClienteBotton { get; set; }
        public string CrearOrdenBotton { get; set; }
        public string EliminarOrdenBotton { get; set; }

        public string Columna_DNI { get; set; }
        public string Columna_Nombre { get; set; }
        public string Columna_Apellido { get; set; }
        public string Columna_Telefono { get; set; }
        public string Columna_NumeroSerie { get; set; }
        public string Columna_Marca { get; set; }
        public string Columna_Modelo { get; set; }
        public string Columna_Estado { get; set; }
        public string Columna_Falla { get; set; }
        public string Columna_Especialidad { get; set; }
        public string Columna_Disponible { get; set; }
        public string Columna_NumeroReparacion { get; set; }
        public string Columna_NombreCliente { get; set; }
        public string Columna_DNICliente { get; set; }
        public string Columna_NombreTecnico { get; set; }
        public string Columna_EspecialidadTecnico { get; set; }
        public string Columna_Cobrado { get; set; }
        public string Columna_FacturaGenerada { get; set; }
        public string Columna_ComprobanteGenerado { get; set; }
        public string Columna_Desarmado { get; set; }
        public string Columna_DañoVisible { get; set; }
    }

    public class FormPdf_234TL : TraduccionesForm_234TL
    {
        public string Label_Facturas { get; set; }
        public string Label_Comprobantes { get; set; }
        public string Boton_Volver { get; set; }

        public string Columna_NumeroFactura { get; set; }
        public string Columna_Cliente { get; set; }
        public string Columna_Total { get; set; }
        public string Columna_NumeroReparacion { get; set; }

        public string Columna_NumeroIngreso { get; set; }
        public string Columna_Equipo { get; set; }
        public string Columna_HoraIngreso { get; set; }
    }

    public class FormRegistroCliente_234TL : TraduccionesForm_234TL
    {
        public string Label_Titulo { get; set; }
        public string Label_DNI { get; set; }
        public string Label_Nombre { get; set; }
        public string Label_Apellido { get; set; }
        public string Label_Telefono { get; set; }

        public string RegistrarBotton { get; set; }
        public string EliminarBotton { get; set; }

        public string Columna_DNI { get; set; }
        public string Columna_Nombre { get; set; }
        public string Columna_Apellido { get; set; }
        public string Columna_Telefono { get; set; }
    }

    public class FormCambiarContraseña_234TL : TraduccionesForm_234TL
    {
        public string Label_Titulo { get; set; }
        public string Label_Actual { get; set; }
        public string Label_Nueva { get; set; }
        public string Label_Confirmar { get; set; }
        public string CambiarBotton { get; set; }
        public string CheckBox_Mostrar { get; set; }
    }

    public class FormIniciaSesion_234TL : TraduccionesForm_234TL
    {

        public string Label_Titulo { get; set; }
        public string Label_Usuario { get; set; }
        public string Label_Contraseña { get; set; }
        public string Label_EjemploUsuario { get; set; }
        public string Label_EjemploContraseña { get; set; }
        public string IngresarBotton { get; set; }
        public string VolverBotton { get; set; }
        public string CheckBox_MostrarContraseña { get; set; }
    }

    public class FormPerfiles_234TL : TraduccionesForm_234TL
    {
        public string GroupBoxPermisos { get; set; }
        public string GroupBoxFamilias { get; set; }
        public string GroupBoxPerfiles { get; set; }
        public string LabelNombreFamilia { get; set; }
        public string CrearFamiliaBotton { get; set; }
        public string SeleccionarFamiliaBotton { get; set; }
        public string DeseleccionarFamiliaBotton { get; set; }
        public string AgregarPermisoAFamiliaBotton { get; set; }
        public string AgregarFamiliaAFamiliaBotton { get; set; }
        public string EliminarDeFamiliaBotton { get; set; }
        public string EliminarFamiliaDefinitivoBotton { get; set; }
        public string LabelNombrePerfil { get; set; }
        public string CrearPerfilBotton { get; set; }
        public string AgregarPermisoAPerfilBotton { get; set; }
        public string AgregarFamiliaAPerfilBotton { get; set; }
        public string EliminarDePerfilBotton { get; set; }
        public string EliminarPerfilDefinitivoBotton { get; set; }
        public string FamiliaSeleccionadaLabel { get; set; }
        public string PermisoSeleccionadoLabel { get; set; }
        public string PerfilSeleccionadoLabel { get; set; }
    }

    public class FormPrincipal_234TL : TraduccionesForm_234TL
    {
        public string SesionButton { get; set; }
        public string IniciarSesionButton { get; set; }
        public string CerrarSesionButton { get; set; }
        public string CambiarContraseñaButton { get; set; }
        public string CambiarIdiomaButton { get; set; }
        public string GestionAdminButton { get; set; }
        public string GestionUsuariosButton { get; set; }
        public string PerfilesButton { get; set; }
        public string BackupButton { get; set; }
        public string RestoreButton { get; set; }
        public string BitacoraEButton { get; set; }
        public string DigVerButton { get; set; }
        public string RecepcionButton { get; set; }
        public string CrearOrdenButton { get; set; }
        public string GeneralFacturaYComprobanteButton { get; set; }
        public string ToolStripStatusLabel { get; set; }
        public string ToolStripBienvenida { get; set; }
        public string ToolStripSinUsuario { get; set; }
        public string MaestrosButton { get; set; }
        public string TecnicosButton { get; set; }
        public string PdfButton { get; set; }
    }

    public class FormCambiarIdioma_234TL : TraduccionesForm_234TL
    {
        public string Boton_Español { get; set; }
        public string Boton_Italiano { get; set; }
        public string Boton_Ingles { get; set; }
        public string Boton_Volver { get; set; }
    }

    public class FormUsuarios_234TL : TraduccionesForm_234TL
    {
        public string CrearButton { get; set; }
        public string ModificarButton { get; set; }
        public string EliminarButton { get; set; }
        public string DesbloquearButton { get; set; }
        public string ActivarDesactivarButton { get; set; }
        public string AplicarButton { get; set; }
        public string ConsultaButton { get; set; }

        public string Label_DNI { get; set; }
        public string Label_Nombre { get; set; }
        public string Label_Apellido { get; set; }
        public string Label_Email { get; set; }
        public string Label_Rol { get; set; }
        public string Label_Titulo { get; set; }

        public string Label_Activos { get; set; }
        public string Label_Todos { get; set; }
        public string Label_Atributos { get; set; }

        public string Columna_DNI { get; set; }
        public string Columna_Nombre { get; set; }
        public string Columna_Apellido { get; set; }
        public string Columna_Email { get; set; }
        public string Columna_Rol { get; set; }
        public string Columna_Bloqueado { get; set; }
        public string Columna_Activo { get; set; }
        public string Columna_Login { get; set; }
        public string Columna_Password { get; set; }
        public string Columna_IntentosFallidos { get; set; }
        public string Columna_UltimoIntentoFallido { get; set; }
    }

#endregion

    public class TraduccionesMensajes_234TL
    {
        public Dictionary<string, string> Exito { get; set; }
        public Dictionary<string, string> Error { get; set; }
        public Dictionary<string, string> Confirmacion { get; set; }
        public Dictionary<string, string> Informacion { get; set; }
        public Dictionary<string, string> Peligro { get; set; }
    }


    public class TraduccionesCOMUNES_234TL
    {
        public string Si { get; set; }
        public string No { get; set; }
        public string Aceptar { get; set; }
        public string Cancelar { get; set; }

        public string VolverBotton { get; set; }

        public string Label_Perfil { get; set; }
        public string Label_Nombre { get; set; }
        public string Label_Apellido { get; set; }
        public string Label_Email { get; set; }
        public string Label_DNI { get; set; }
        public string Boton_Volver { get; set; }

        public string Title_Confirmacion { get; set; }
        public string Title_Error { get; set; }
        public string Title_Peligro { get; set; }
        public string Title_Informacion { get; set; }
        public string Title_Exito { get; set; }

    }
}
