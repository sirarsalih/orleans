using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Concurrency;

namespace HrGrainInterfaces
{
    [Immutable]
    public class GreetingData
    {
        public long From { get; set; }
        public string Message { get; set; }
        public int Count { get; set; }
    }
}
