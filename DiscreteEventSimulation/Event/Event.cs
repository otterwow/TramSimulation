using DiscreteEventSimulation.State;
using DiscreteEventSimulation.Utils;
using System.Collections.Generic;

namespace DiscreteEventSimulation.Event
{
    public abstract class Event<T> where T: State.State
    {   
        protected EventScheduler<T> scheduler;
        
        public Event(EventScheduler<T> scheduler)
        {            
            this.scheduler = scheduler;                        
        }

        protected abstract void ExecuteEvent(T state);

        public void Execute(T state)
        {
            this.ExecuteEvent(state);
        }

        protected void scheduleEvent(Event<T> e, long time)
        {
            scheduler.ScheduleEvent(e, scheduler.GetTime() + time);
        }

        public virtual string PrintEvent()
        {
            return this.GetType().Name;
        }
    }
}
