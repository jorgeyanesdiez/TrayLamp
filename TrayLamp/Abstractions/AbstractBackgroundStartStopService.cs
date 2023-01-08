using System.Threading;
using System.Threading.Tasks;

namespace TrayLamp.Abstractions
{
    public abstract class AbstractBackgroundStartStopService : IStartStopService
    {
        private CancellationTokenSource CancellationTokenSource { get; }
        protected CancellationToken CancellationToken { get; }

        protected AbstractBackgroundStartStopService()
        {
            CancellationTokenSource = new CancellationTokenSource();
            CancellationToken = CancellationTokenSource.Token;
        }

        public void Start() => Task.Run(() => DoWork(), CancellationToken);

        public void Stop() => CancellationTokenSource.Cancel();

        protected abstract void DoWork();
    }
}
