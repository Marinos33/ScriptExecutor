using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorMAUI.Model
{
    [Table("process")]
    public  class Process
    {
        [PrimaryKey, AutoIncrement, Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("executableFile")]
        public string ExecutableFile { get; set; }

        [Column("script")]
        public string Script { get; set; }

        [Column("runOnStart"), DefaultValue("False")]
        public bool RunOnStart { get; set; }

        [Column("runAfterShutdown"), DefaultValue("True")]
        public bool RunAfterShutdown { get; set; } = true;
    }
}
