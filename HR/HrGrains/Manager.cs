using System.Collections.Generic;
using System.Threading.Tasks;
using HrGrainInterfaces;
using Orleans;
using Orleans.Providers;

namespace HrGrains
{
    /*To add persistent storage to this grain:
     
     [StorageProvider(ProviderName="MemoryStore")]
     public class Manager : Orleans.Grain<IManagerState>, IManager

     Then remove the private variables (transient storage) that you want persisted.
     
     Without persistent storage:
     
     public class Manager : Grain, IManager
     
     */
    [StorageProvider(ProviderName = "MemoryStore")]
    public class Manager : Orleans.Grain<IManagerState>, IManager
    {
        private IEmployee _me;
        //private readonly List<IEmployee> _reports = new List<IEmployee>();

        public override Task OnActivateAsync()
        {
            _me = EmployeeFactory.GetGrain(this.GetPrimaryKey());
            return base.OnActivateAsync();
        }

        public async Task AddDirectReport(IEmployee employee)
        {
            //_reports.Add(employee);
            State.Reports.Add(employee);
            await employee.SetManager(this);
            await employee.Greeting(new GreetingData
            {
                From = _me.GetPrimaryKeyLong(),
                Message = "Welcome to my team!"
            });
            //Save to storage provider
            await State.WriteStateAsync();
        }

        public Task<List<IEmployee>> GetDirectReports()
        {
            //return Task.FromResult(_reports);
            return Task.FromResult(State.Reports);
        }

        public Task<IEmployee> AsEmployee()
        {
            return Task.FromResult(_me);
        }
    }
}