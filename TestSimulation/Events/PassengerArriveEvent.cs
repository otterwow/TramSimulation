using DiscreteEventSimulation.Event;
using DiscreteEventSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using TestSimulation.States;
using TestSimulation.Stops;

namespace TestSimulation.Events
{
    class PassengerArriveEvent : Event<WorldState>
    {
        private Stop stop;

        private int _passengersArriving;

        public PassengerArriveEvent(Stop stop, EventScheduler<WorldState> scheduler) : base(scheduler)
        {
            this.stop = stop;
        }

        protected override void ExecuteEvent(WorldState state)
        {
            StopState stopState = state.GetStopState(stop);
            stopState.Passengers += calculatePassengers();

            scheduleEvent(new PassengerArriveEvent(stop, scheduler), calculateNextSchedule());
        }

        private int calculatePassengers()
        {
            _passengersArriving = (int)SimulationRNG.GetRandomPoissonDistribution(100);
            return _passengersArriving;
        }

        private long calculateNextSchedule()
        {
            return 15 * 60;            
        }

        public override string PrintEvent()
        {
            return $"Passengers arrived at stop: {stop.CurrentStop}, a: {_passengersArriving}";
        }
    }
}
