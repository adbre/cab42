namespace C42A
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class ProgramOptions
    {
        public ProgramOptions()
        {
            this.Variables = new Dictionary<string, string>();
        }

        public bool StartWindowsFormsApplication { get; set; }

        public string FileName { get; set; }

        public Dictionary<string, string> Variables { get; private set; }

        public static ProgramOptions Parse(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");

            var result = new ProgramOptions();

            if (args.Length == 0)
                result.StartWindowsFormsApplication = true;
            else if (args.Length == 1 && args[0] == "open")
                result.StartWindowsFormsApplication = true;
            else if (args.Length == 1 && File.Exists(args[0]))
            {
                result.StartWindowsFormsApplication = true;
                result.FileName = args[0];
            }
            else if (args.Length == 2 && args[0] == "open")
            {
                result.StartWindowsFormsApplication = true;
                result.FileName = args[1];
            }
            else
            {
                for (var i = 0; i < args.Length; i++)
                {
                    var argc = args[i];

                    if (i == 0 && argc != "build")
                        throw new ArgumentException();
                    if (i == 0)
                        continue;

                    if (argc.StartsWith("-"))
                    {
                        switch (argc)
                        {
                            case "--set-variable":
                                if (i + 2 >= args.Length) throw new ArgumentException("--set-variable NAME VALUE");
                                result.Variables.Add(args[++i], args[++i]);
                                break;

                            default:
                                throw new ArgumentException(string.Format("Unrecognized switch: {0}", argc));
                        }
                    }
                    else
                    {
                        if (result.FileName != null) throw new ArgumentException(string.Format("Unexpected argument: {0}", argc));
                        result.FileName = argc;
                    }
                }
            }

            return result;
        }
    }
}