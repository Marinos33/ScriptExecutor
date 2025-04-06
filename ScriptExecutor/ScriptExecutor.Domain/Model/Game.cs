namespace ScriptExecutor.Domain.Model
{
    public class Game
    {
        public string Name { get; set; }
        public string ExecutableFile { get; set; }
        public string Script { get; set; }
        public bool RunOnStart { get; set; }
        public bool RunAfterShutdown { get; set; } = true;

        public Game DeepCopy()
        {
            return new()
            {
                Name = Name,
                ExecutableFile = ExecutableFile,
                Script = Script,
                RunOnStart = RunOnStart,
                RunAfterShutdown = RunAfterShutdown
            };
        }
    }
}