using System;
using System.Collections.Generic;
using DiscreteEventSimulation.Event;
using TestSimulation.States;
using TestSimulation.Stops;

namespace TestSimulation.Events
{
    public class ArriveStopEvent : Event<WorldState>
    {
        private Stop stop;
        private Tram tram;

        /// <summary>
        /// A <param name="tram">tram</param> has arrived at <param name="stop">stop</param>
        /// </summary>
        /// <param name="tram"></param>
        /// <param name="stop"></param>
        /// <param name="scheduler"></param>
        public ArriveStopEvent(Tram tram, Stop stop, EventScheduler<WorldState> scheduler) : base(scheduler)
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
            // Tram arrives at queue
            StopState stopState = state.GetStopState(stop);            

            this.scheduleEvent(new DepartStopEvent(tram, stop.NextStop, scheduler), 80);
        }

        public override string PrintEvent()
        {
            var term = stop.IsTerminal ? "Terminal Station" : "";
            return $"Arrived: {tram.Name}: {stop.CurrentStop}, {tram}, {term}";
        }

    }
}