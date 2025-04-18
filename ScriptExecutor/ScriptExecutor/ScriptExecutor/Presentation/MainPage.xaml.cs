
namespace ScriptExecutor.Presentation;

public sealed partial class MainPage : Page
{
    private MainModel? ViewModel => DataContext as MainModel;

    public MainPage()
    {
        this.InitializeComponent();
        this.Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object sender, RoutedEventArgs e)
    {
        // Add event handlers for buttons after data is loaded
        var listView = this.FindName("ProcessesListView") as ListView;
        if (listView != null)
        {
            listView.ContainerContentChanging += ListView_ContainerContentChanging;
        }
    }

    private void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args)
    {
        if (args.ItemContainer.ContentTemplateRoot is FrameworkElement root)
        {
            var editButton = root.FindName("EditButton") as Button;
            var deleteButton = root.FindName("DeleteButton") as Button;
            var process = args.Item as Process;

            if (editButton != null)
            {
                editButton.Click += async (s, e) =>
                {
                    if (process != null && ViewModel != null)
                    {
                        var processesMessage = await ViewModel.Processes.GetSource(default).FirstOrDefaultAsync();
                        var processes = processesMessage.Current.Data.IsSome(out var data) ? data as List<Process> : null;
                        if (processes != null)
                        {
                            int index = processes.IndexOf(process);
                            if (index >= 0)
                            {
                                await ViewModel.EditProcessHandler(process); // Updated method call
                            }
                        }
                    }
                };
            }

            if (deleteButton != null)
            {
                deleteButton.Click += async (s, e) =>
                {
                    if (process != null && ViewModel != null)
                    {
                        var processesMessage = await ViewModel.Processes.GetSource(default).FirstOrDefaultAsync();
                        var processes = processesMessage.Current.Data.IsSome(out var data) ? data as List<Process> : null;
                        if (processes != null)
                        {
                            int index = processes.IndexOf(process);
                            if (index >= 0)
                            {
                                await ViewModel.DeleteProcessHandler(process); // Updated method call
                            }
                        }
                    }
                };
            }
        }
    }
}
