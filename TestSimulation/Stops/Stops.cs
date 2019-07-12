using DiscreteEventSimulation.Utils;
using System;

namespace TestSimulation.Stops
{
    public enum EStop
    {
        STATION_CS_CENTRUMZIJDE = 0,	
        VAARTSCHE_RIJN,
        GALGENWAARD,
        KROMME_RIJN,
        PADUALAAN,
        HEIDELBERGLAAN,
        UMC,
        WKZ,
        P_R_DE_UITHOF,

    }

    public class Stop
    {
        public long VarianceTravelTime { get; set; }
        public long TravelTime { get; set; }
        
        private EStop _currentStop;
        private Stop _nextStop;

        public Stop NextStop
        {
            get => _nextStop;
            set => _nextStop = value;
        }
        
        

        public EStop CurrentStop
        {
             get => _currentStop;
        }

        public bool IsTerminal
        {
            get
            {
                return
                    _currentStop == EStop.P_R_DE_UITHOF || _currentStop == EStop.STATION_CS_CENTRUMZIJDE;
            }
        }

        public Stop(EStop current, Stop next, long travelTimeToNext, long variance)
        {
            this._currentStop = current;
            this._nextStop = next;
            this.TravelTime = travelTimeToNext;
            this.VarianceTravelTime = variance;
        }

        public long GenerateRandomTravelTime()
        {
            return Math.Max(0, Math.Max(TravelTime - 2 * VarianceTravelTime, SimulationRNG.GetRandomNormal(TravelTime, VarianceTravelTime)));
        }
    }
    
}