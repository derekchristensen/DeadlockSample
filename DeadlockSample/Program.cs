using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlockSample
{
    public static class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var mainRootCommand = new RootCommand();

            var arg = new Argument("lib")
            {
                Arity = ArgumentArity.ZeroOrOne,
                Description = "The DLL to load",
            };
            arg.SetDefaultValue("DeadlockSample.MyApp.dll");
            mainRootCommand.AddArgument(arg);

            try
            {
                mainRootCommand.Handler = CommandHandler.Create<string, CancellationToken>(ProcessCommands);
                return await new CommandLineBuilder(mainRootCommand)
                    .UseDefaults()
                    .Build()
                    .InvokeAsync(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return -1;
        }

        private static async Task<int> ProcessCommands(string lib, CancellationToken cancellationToken)
        {
            var libInfo = new FileInfo(lib);
            if (!libInfo.Exists)
            {
                Console.Error.WriteLine("Unable to locate library: " + libInfo.FullName);
                return -1;
            }

            var assembly = Assembly.LoadFile(libInfo.FullName);
            var type = assembly.GetType("DeadlockSample.MyApp");
            var method = type.GetMethod("Run");

            var app = Activator.CreateInstance(type);

            var ret = (Task<int>) method.Invoke(app, new object[] {cancellationToken});
            await ret;
            return 0;
        }
    }
}
