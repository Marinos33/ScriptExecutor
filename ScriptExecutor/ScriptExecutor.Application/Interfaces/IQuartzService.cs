using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutor.Application.Interfaces
{
    public interface IQuartzService
    {
        Task StartAsync();
        Task StopAsync();
    }
}
