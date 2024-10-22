using Messaging.Abstraction.IConsumerService;

namespace API.Jobs
{
    public class BackgroundWorker : IHostedService
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ISysGeneralCodeConsumerService _sysGeneralCodeConsumerService;

        public BackgroundWorker(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                _sysGeneralCodeConsumerService = scope.ServiceProvider.GetRequiredService<ISysGeneralCodeConsumerService>();
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            //_sysGeneralCodeConsumerService.Consume("sys_general_code");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }


}
