using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.DTOModel
{
    public class ProcessDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExecutableFile { get; set; }
        public string Script { get; set; }
        public string ImagePath { get; set; }
        public bool RunOnStart { get; set; }
        public bool RunAfterShutdown { get; set; } = true;
    }
}
