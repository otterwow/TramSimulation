#define DEBUG

using DiscreteEventSimulation.Event;
using System;
using System.Collections.Generic;
using TestSimulation.Events;
using TestSimulation.States;
using TestSimulation.Stops;

namespace TestSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            DEBUG("Hello World!");

            var program = new Program();

            var timeData = new long[] {110, 78, 82, 60, 100, 59, 243, 135, 134, 243, 59, 101, 60, 86, 78, 113, 90};
            var varianceData = new long[] {290, 141, 17, 58, 61, 328, 440, 392, 26, 1101, 88, 30, 21, 614, 321, 10, 90};
        
            program.Start(timeData, varianceData);
            
            Console.ReadLine();
        }

        private void Start(long[] timeData, long[] varianceData)
        {

            EventScheduler<WorldState> scheduler = new EventScheduler<WorldState>();
            DEBUG("Initializing stops...");
            List<Stop> stops = InitiateStops(timeData, varianceData);
            DEBUG("Finished initializing stops...");
            DEBUG("Initializing trams...");
            List<Tram> trams = InitiateTrams(17);
            DEBUG("Finished initializing trams...");
            DEBUG("Initializing WorldState...");
            WorldState worldState = new WorldState(stops, trams);
            DEBUG("Finished initializing WorldState");

            int seed = 342134532;

            scheduleInitiativeEvents(scheduler, trams, stops);            
            
            worldState = scheduler.StartSimulation(60 * 60 * 24, worldState, (state, ev) => {
                Console.Write($"T: {state.GetTime()}");
                Console.Write($": {ev.PrintEvent()}");
                Console.Write("\n");
            }, seed);
        }

        private void scheduleInitiativeEvents(EventScheduler<WorldState> scheduler, List<Tram> trams, List<Stop> stops)
        {
            for (int i = 0; i < 5; i++)
            {
                scheduler.ScheduleEvent(new DepartStopEvent(trams[i], stops[0], scheduler), i * 60);
            }

            foreach(var stop in stops)
            {
                scheduler.ScheduleEvent(new PassengerArriveEvent(stop, scheduler), 0);
            }
        }

        /// <summary>
        /// Create an initial list of stops
        /// </summary>
        /// <param name="travelTimes">An array of how many seconds it take to go from station to next station, length should be 17</param>
        /// <param name="varianceData">An array of how many seconds variance, length should be 17</param>
        /// <returns></returns>
        private List<Stop> InitiateStops(long[] travelTimes, long[] varianceData)
        {
            // Create a list of stops
            List<Stop> stops = new List<Stop>();
            
            // Create Stop store
            Func<EStop, Stop, long, long, Stop> createStop = 
                (estop, next, travelTime, variance) => new Stop(estop, next, travelTime, variance);

            // Create an array in which the roundtrip is defined.
            EStop[] roundtrip = new EStop[]
            {
                // Stops on the trip to Utrecht CS
                EStop.P_R_DE_UITHOF,
                EStop.WKZ, 
                EStop.UMC, 
                EStop.HEIDELBERGLAAN,
                EStop.PADUALAAN, 
                EStop.KROMME_RIJN, 
                EStop.GALGENWAARD, 
                EStop.VAARTSCHE_RIJN, 
                EStop.STATION_CS_CENTRUMZIJDE, 
                // Stops on the way back to PR
                EStop.VAARTSCHE_RIJN, 
                EStop.GALGENWAARD, 
                EStop.KROMME_RIJN, 
                EStop.PADUALAAN, 
                EStop.HEIDELBERGLAAN, 
                EStop.UMC, 
                EStop.WKZ, 
                EStop.P_R_DE_UITHOF
            };
           
            // Create stops
            #if DEBUG
            DEBUG($"Amount of stops: {roundtrip.Length}");
            DEBUG($"Amount of variance data: {varianceData.Length}");
            DEBUG($"Amount of travel data: {travelTimes.Length}");
            #endif
            
            for (int i = 0; i < roundtrip.Length; i++)
            {
                stops.Add(createStop(roundtrip[i], null, travelTimes[i], varianceData[i]));
            }
            
            // Create next stop connections
            for (int i = 0; i < roundtrip.Length - 1; i++)
            {
                stops[i].NextStop = stops[i + 1];
            }
            // Connect last stop to first
            stops[stops.Count - 1].NextStop = stops[0];

            return stops;
        }

        /// <summary>
        /// Create a list of trams
        /// </summary>
        /// <returns></returns>
        private List<Tram> InitiateTrams(int amountOfTrams)
        {
            List<Tram> trams = new List<Tram>();
            for (int i = 0; i < amountOfTrams; i++)
            {
                trams.Add(new Tram($"Tram: {i}"));
            }
            return trams;
        }

        public static void DEBUG(string message)
        {
            #if DEBUG
            Console.WriteLine(message);           
            #endif
        }
    }
}
