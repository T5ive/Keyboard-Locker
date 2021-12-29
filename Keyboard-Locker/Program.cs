global using System.Diagnostics;
global using System.Runtime.InteropServices;
global using System.Xml;
global using System.Xml.Serialization;
namespace Keyboard_Locker;
internal static class Program
{
    public static FrmKeyDetection FrmKeyDetection = new();
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();
        Application.Run(new FrmMain());
    }
}
