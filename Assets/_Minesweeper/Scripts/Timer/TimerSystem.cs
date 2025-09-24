using VContainer.Unity;

namespace TimerModule
{
    public class TimerSystem : ITickable
    {
        private readonly TimerService timerService;
        private readonly TimerRepository timerRepository;

        public TimerSystem(TimerService timerService, TimerRepository timerRepository)
        {
            this.timerService = timerService;
            this.timerRepository = timerRepository;
        }

        public void Tick()
        {
            var timersCount = timerRepository.Get().Count;
            if (timersCount <= 0)
            {
                return;
            }

            for (var i = timersCount - 1; i >= 0; i--)
            {
                var timer = timerRepository.Get()[i];

                if (timer.State == TimerState.Default)
                {
                    continue;
                }

                if (timer.State == TimerState.Running)
                {
                    if (timerService.IsTimerExpired(timer))
                    {
                        timerService.StopTimer(timer);
                    }
                }

                if (timerService.IsTimerDefrosted(timer))
                {
                    timerService.DefrostTimer(timer);
                }
            }
        }
    }
}
