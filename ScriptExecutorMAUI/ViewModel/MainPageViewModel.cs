using ScriptExecutorMAUI.DTOModel;

namespace ScriptExecutorMAUI.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<GameDto> Games { get; } = new();
        private readonly IDataManager _dataManager;

        public MainPageViewModel(IDataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [RelayCommand]
        private async Task GetGames()
        {
            try
            {
                var games = await _dataManager.ReadJson();
                games = games.OrderBy(x => x.Name).ToList();

                if (Games.Count != 0)
                    Games.Clear();

                foreach (var game in games)
                {
                    var g = new GameDto
                    {
                        Name = game.Name,
                        Script = game.Script,
                        ExecutableFile = game.ExecutableFile,
                        RunOnStart = game.RunOnStart,
                        RunAfterShutdown = game.RunAfterShutdown,
                        ImagePath = !string.IsNullOrEmpty(game.ExecutableFile) && !string.IsNullOrEmpty(game.Name) ? "check.png" : "error.png"
                    };

                    Games.Add(g);
                }

            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error! Could not read JSON data", e.Message, "OK");
                //TODO add to logs
            }
        }
    }
}
