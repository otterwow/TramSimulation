using DiscreteEventSimulation.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using DiscreteEventSimulation.State;

namespace DiscreteEventSimulation.Event
{
    public class EventScheduler<T> where T : State.State
    {
        private OrderedQueue<Event<T>> queue;

        private long currentTime = 0;

        public EventScheduler()
        {
            this.queue = new OrderedQueue<Event<T>>();
        }

        public Tuple<long, Event<T>> PeekEvent()
        {
            return queue.Peek();
        }

        public void ScheduleEvent(Event<T> e, long time)
        {
            this.queue.Enqueue(time, e);
        }

        public long GetTime()
        {
            return currentTime;
        }

        public Event<T> ExecuteNextEvent(T state)
        {
            
            Tuple<long, Event<T>> item = this.queue.Dequeue();
            if (item != null)
            {
                currentTime = item.Item1;

                state.SetTime(GetTime());
                item.Item2.Execute(state);
                return item.Item2;
            } else
            {
                throw new Exception();
            }            
        }

        public T StartSimulation(int v, T state, Action<T, Event<T>> func, int seed = 0)
        {
            SimulationRNG.CreateInstance(seed);
            while (GetTime() <= v && PeekEvent() != null)
            {
                try
                {
                    Event<T> ev = ExecuteNextEvent(state);
                    func(state, ev);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                    break;
                }                
            }
            return state;
        }
    }
}
