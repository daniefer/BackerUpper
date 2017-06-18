using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using BackerUpper.Helpers;

namespace BackerUpper.Extensions
{
    public static class Extensions
    {
        public static string Checksum(this FileInfo fileInfo)
        {
            return AWSChecksumHelper.ComputeLocalChecksum(fileInfo);
        }

        public static Task<T> RunAsync<T>(Func<T> function)
        {
            if (function == null)
            {
                throw new ArgumentNullException(nameof(function));
            }
            var tcs = new TaskCompletionSource<T>();
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    T result = function();
                    tcs.SetResult(result);
                }
                catch (Exception e)
                {
                    tcs.SetException(e);
                }
            });
            return tcs.Task;
        }
    }
}
