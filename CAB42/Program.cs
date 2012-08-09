namespace C42A
{
    using System;
    using System.Collections.Generic;
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
            if (args.Length == 1 && File.Exists(args[0]))
            {
                StartWinFormsApplication(args[0]);
            }
            else if (args.Length == 0)
            {
                StartWinFormsApplication(null);
            }
            else
            {
                return RunConsole(args);
            }

            return 0;
        }

        private static int RunConsole(string[] args)
        {
            try
            {
                if (args.Length == 2 && args[0] == "build")
                {
                    Build(args[1]);
                    return 0;
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

        private static void Build(string file)
        {
            var buildProject = ProjectInfo.Open(file);
            using (var buildContext = new CabwizBuildContext())
            {
                var tasks = buildProject.CreateBuildTasks();

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
