namespace C42A
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static int Main(string[] args)
        {
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
    }
}
