namespace KnapsackProblem
{
    using CommandLine;
    using KnapsackProblem.Arguments;
    using System;

    public class Program
    {

        public static void Main(string[] args)
        {
            var programArgs = new ProgramArgs();
            var parser = new Parser(settings =>
                                    {
                                        settings.HelpWriter = null;
                                        settings.IgnoreUnknownArguments = false;
                                        settings.MutuallyExclusive = true;
                                    });

            parser.ParseArgumentsStrict(args, programArgs, () =>
                                                           {
                                                               WriteLine("Program ran with invalid arguments.");
                                                               Environment.Exit(0);
                                                           });

            if (programArgs.WaitForKey)
            {
                Console.ReadKey();
            }
        }

        public static void WriteLine(string format, params object[] args)
        {
            Console.Out.WriteLine(format, args);
        }
    }
}
