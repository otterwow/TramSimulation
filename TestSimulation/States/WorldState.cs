using DiscreteEventSimulation.State;
using System;
using System.Collections.Generic;
using System.Text;
using TestSimulation.Stops;

namespace TestSimulation.States
{
    public class WorldState : State
    {

        public List<Stop> Stops;
        public List<Tram> Trams;
        public List<Tram> Parked;

        public Dictionary<Stop, StopState> StopStates;
        public Dictionary<Tram, TramState> TramStates;
        
        /// <summary>
        /// Define the world state
        /// </summary>
        /// <param name="stops">A list of the stops defined</param>
        /// <param name="trams">A list of the trams we use</param>
        public WorldState(List<Stop> stops, List<Tram> trams)
        {
            this.Stops = stops;
            this.Trams = trams;
            this.Parked = new List<Tram>(trams);
            
            this.StopStates = new Dictionary<Stop, StopState>();
            this.TramStates = new Dictionary<Tram, TramState>();
            
        }

        /// <summary>
        /// Get the <see cref="StopState"/> of the <see cref="Stop"/>
        /// </summary>
        /// <param name="stop">The <see cref="Stop"/> for which we want the state</param>
        /// <returns>Returns <see cref="StopState"/> </returns>
        public StopState GetStopState(Stop stop)
        {
            if (!StopStates.ContainsKey(stop))
                StopStates.Add(stop, new StopState(stop));

            return StopStates[stop];
        }

        public TramState GetTramState(Tram tram)
        {
            return TramStates[tram];
        }

        public string PrintState()
        {
            return "";
        }
    }

    public class StopState : State
    {
        public Stop Stop;
        public Queue<Tram> Queue;
        public int Passengers;
        public long PreviousDeparture;
        public bool Occupied;
        public long[] MeanPassengersIn;
        public long[] MeanPassengersOut;
        public long[] Schedule;

        public StopState(Stop stop)
        {
            this.Stop = stop;
            this.Queue = new Queue<Tram>();
        }
    }

    public class TramState : State
    {
        public long Passengers;
        public long PreviousDepartureTime;
    }
}
