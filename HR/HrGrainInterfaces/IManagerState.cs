using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace HrGrainInterfaces
{
    public interface IManagerState : IGrainState
    {
        List<IEmployee> Reports { get; set; }
    }
}
