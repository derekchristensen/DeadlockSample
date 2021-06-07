using System;
using System.Threading;
using System.Threading.Tasks;

namespace DeadlockSample
{
    public class MyApp
    {
        public async Task<int> Run(CancellationToken cancellationToken)
        {
            try
            {
                Console.WriteLine("Press Ctrl+C to exit...");
                await Task.Delay(10 * 1000, cancellationToken);
                Console.WriteLine("Done");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return -1;
            }
        }
    }
}