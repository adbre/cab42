namespace C42A
{
    using System;
    using System.Collections.Generic;
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
            if (args.Length > 0 && args[0] == "build")
            {
                AllocConsole();

                if (args.Length != 2) Console.WriteLine("cab42.exe build PATH");

                try
                {
                    Build(args[1]);
                }
                catch (Exception error)
                {
                    Console.Error.WriteLine(error.ToString());
                    return 1;
                }

                return 0;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            using (var f = new global::C42A.CAB42.Windows.Forms.CAB42())
            {
                if (args != null && args.Length > 0)
                {
                    if (args.Length == 1)
                    {
                        f.OpenFileOnShow = args[0];
                    }
                }

                Application.Run(f);
            }

            return 0;
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
    }
}
