using DiscreteEventSimulation.Event;
using TestSimulation.States;
using TestSimulation.Stops;

namespace TestSimulation.Events
{
    public class DepartStopEvent : Event<WorldState>
    {
        private Stop stop;
        private Tram tram;

        /// <summary>
        /// A <param name="tram">tram</param> has departed from the <param name="stop">stop</param>
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="stop"></param>
        /// <param name="scheduler"></param>
        public DepartStopEvent(Tram tram, Stop stop, EventScheduler<WorldState> scheduler) : base(scheduler)
        {
            this.tram = tram;
            this.stop = stop;
        }

        /// <summary>
        /// The tram has departed from this stop, to the next stop.
        /// </summary>
        /// <param name="state"></param>
        protected override void ExecuteEvent(WorldState state)
        {
            scheduleEvent(new ArriveStopEvent(tram, stop.NextStop, scheduler), stop.GenerateRandomTravelTime());   
        }
        
        
        public override string PrintEvent()
        {
            return $"Departed: {tram.Name}: {stop.CurrentStop}, {tram}";
        }

    }
}