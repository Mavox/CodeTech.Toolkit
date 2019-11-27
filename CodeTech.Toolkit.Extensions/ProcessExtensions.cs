using System.Diagnostics;
using System.Threading.Tasks;

namespace CodeTech.Toolkit.Extensions
{
    public static class ProcessExtensions
    {
        static Task StartAsync(this Process process)
        {
            var tcs = new TaskCompletionSource<bool>();

            process.EnableRaisingEvents = true;

            process.Exited += (sender, args) =>
            {
                tcs.SetResult(true);
                process.Dispose();
            };

            process.Start();

            return tcs.Task;
        }
    }
}
