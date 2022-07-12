using ScriptExecutorMAUI.DTOModel;
using SQLite;

namespace ScriptExecutorMAUI.Model
{
    [Table("process")]
    public class Process : IMapFrom<ProcessDto>
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
