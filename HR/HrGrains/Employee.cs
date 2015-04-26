using System;
using System.Diagnostics.Eventing.Reader;
using System.Threading.Tasks;
using HrGrainInterfaces;
using Orleans;
using Orleans.Concurrency;
using Orleans.Providers;

namespace HrGrains
{
    /*To add persistent storage to this grain:
     
     [StorageProvider(ProviderName = "MemoryStore")]
     public class Employee : Orleans.Grain<IEmployeeState>, Interfaces.IEmployee

     Then remove the private variables (transient storage) that you want persisted.
     
     Without persistent storage, and reentrant (to avoid deadlocks):
     
     [Reentrant]
     public class Employee : Grain, IEmployee
     
     */
    [StorageProvider(ProviderName = "MemoryStore")]
    public class Employee : Orleans.Grain<IEmployeeState>, IEmployee
    {
        //private int _level;
        //private IManager _manager;

        public Task<int> GetLevel()
        {
            //return Task.FromResult(_level);
            return Task.FromResult(State.Level);
        }

        public Task Promote(int newLevel)
        {
            //_level = newLevel;
            State.Level = newLevel;
            //Save to storage provider
            State.WriteStateAsync();
            return TaskDone.Done;
        }

        public Task<IManager> GetManager()
        {
            //return Task.FromResult(_manager);
            return Task.FromResult(State.Manager);
        }

        public Task SetManager(IManager manager)
        {
            //_manager = manager;
            State.Manager = manager;
            //Save to storage provider
            State.WriteStateAsync();
            return TaskDone.Done;
        }

        public async Task Greeting(GreetingData greetingData)
        {
            Console.WriteLine("{0} said: {1}", greetingData.From, greetingData.Message);

            // stop this from repeating endlessly
            if (greetingData.Count >= 3) return;

            // send a message back to the sender
            var fromGrain = EmployeeFactory.GetGrain(greetingData.From);
            await fromGrain.Greeting(new GreetingData
            {
                From = this.GetPrimaryKeyLong(),
                Message = "Thanks!",
                Count = greetingData.Count + 1
            });
        }
    }
}
