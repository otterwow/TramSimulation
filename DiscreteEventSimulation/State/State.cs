using System;
using System.Collections.Generic;
using System.Text;

namespace DiscreteEventSimulation.State
{
    public abstract class State
    {
        private long time;

        public void SetTime(long time)
        {
            this.time = time;
        }

        public long GetTime()
        {
            return time;
        }
    }
}
