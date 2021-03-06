using Chords.Predictors;
using System;
using System.Windows.Forms;

namespace ChordsDesktop
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(
                new AutoMlPredictor(),
                new Chords.Repositories.FileSystemChordRepository("./storedChords/")
            ));
        }
    }
}
