using ScriptExecutorMAUI.ViewModel;

namespace ScriptExecutorMAUI.View;

public partial class AddPage : ContentPage
{
    public AddPage(AddPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        PickExe.Clicked += async (sender, args) =>
        {
            try
            {
                var customFileType = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                    { DevicePlatform.WinUI, new[] { ".exe"} },
                    });

                PickOptions options = new()
                {
                    PickerTitle = "Please select an executable file",
                    FileTypes = customFileType,
                };

                var result = await FilePicker.Default.PickAsync(options);
                if (result != null)
                {
                    ExecutableEntry.Text = result.FileName;
                    SemanticScreenReader.Announce(ExecutableEntry.Text);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        };
    }
}