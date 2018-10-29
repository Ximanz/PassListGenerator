using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PassListGenerator.Logging
{
    /// <summary>
    /// This class is copied and modified from Sam Saffron on stackoverflow at 
    /// https://stackoverflow.com/questions/1181561/how-to-effectively-log-asynchronously
    /// </summary>
    public abstract class LoggerBase : IDisposable
    {

        List<string> buffer = new List<string>();
        ManualResetEvent hasNewItems = new ManualResetEvent(false);
        ManualResetEvent terminate = new ManualResetEvent(false);
        ManualResetEvent waiting = new ManualResetEvent(false);

        Thread loggingThread;

        const int batchSize = 100;

        public LoggerBase()
        {
            loggingThread = new Thread(new ThreadStart(ProcessQueue));
            loggingThread.IsBackground = true;
            // this is performed from a bg thread, to ensure the queue is serviced from a single thread
            loggingThread.Start();
        }


        void ProcessQueue()
        {
            while (true)
            {
                waiting.Set();

                int i = ManualResetEvent.WaitAny(new WaitHandle[] { hasNewItems, terminate });
                // terminate was signaled 
                if (i == 1) return;

                hasNewItems.Reset();
                if (buffer.Count < batchSize) continue;

                waiting.Reset();

                List<string> bufferCopy;
                lock (buffer)
                {
                    bufferCopy = new List<string>(buffer);
                    buffer.Clear();
                }

                BulkLog(bufferCopy);
            }
        }

        public void Log(string passphrase)
        {
            lock (buffer)
            {
                buffer.Add(passphrase);
            }
            hasNewItems.Set();
        }

        protected abstract void BulkLog(List<string> buffer);


        public void Flush()
        {
            waiting.WaitOne();
        }


        public virtual void Dispose()
        {
            terminate.Set();
            loggingThread.Join();
        }
    }
}
