using AutoMapper;

namespace ScriptExecutorMAUI.DTOModel
{
    public class ProcessDto : IMapFrom<Process>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExecutableFile { get; set; }
        public string Script { get; set; }
        public string ImagePath { get; set; } //determine the image to use
        public bool RunOnStart { get; set; }
        public bool RunAfterShutdown { get; set; } = true;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Process, ProcessDto>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(p => !string.IsNullOrEmpty(p.ExecutableFile) && !string.IsNullOrEmpty(p.Name) ? "check.png" : "error.png"));
        }
    }
}
