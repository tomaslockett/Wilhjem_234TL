namespace GUI_234TL
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Application.Run(new FormPrincipal_234TL());
        }
    }
}