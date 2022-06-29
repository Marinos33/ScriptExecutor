using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Model
{
    public  class Process
    {
        public string Name { get; set; }
        public string ExecutableFile { get; set; }
        public string Script { get; set; }
        public bool RunOnStart { get; set; }
        public bool RunAfterShutdown { get; set; } = true;
    }
}
