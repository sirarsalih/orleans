using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans;

namespace HrGrainInterfaces
{
    public interface IEmployeeState : IGrainState
    {
        int Level { get; set; }
        IManager Manager { get; set; }
    }
}
