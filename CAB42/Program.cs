namespace C42A
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    using C42A.CAB42;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static int Main(string[] args)
        {
            var options = ProgramOptions.Parse(args);
            if (options.StartWindowsFormsApplication)
                StartWinFormsApplication(options.FileName);
            else
            {
                return RunConsole(options);
            }

            return 0;
        }

        private static int RunConsole(ProgramOptions options)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(options.FileName))
                {
                    Build(ProjectInfo.Open(options));
                }

                PrintHelp();
                return 2;
            }
            catch (Exception error)
            {
                Console.Error.WriteLine(error.ToString());
                return 1;
            }
        }

        private static void PrintHelp()
        {
            Console.WriteLine("cab42.exe [build] [PATH]");
        }

        private static void StartWinFormsApplication(string file)
        {
            FreeConsole();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var f = new CAB42.Windows.Forms.CAB42())
            {
                if (file != null) f.OpenFileOnShow = file;

                Application.Run(f);
            }
        }

        private static void Build(ProjectInfo projectInfo)
        {
            using (var buildContext = new CabwizBuildContext())
            {
                var tasks = projectInfo.CreateBuildTasks();

                var feedback = new BuildFeedbackBase(Console.Out);
                buildContext.Build(tasks, feedback);
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
    }
}
