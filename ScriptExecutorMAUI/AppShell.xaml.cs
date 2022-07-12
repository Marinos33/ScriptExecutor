using ScriptExecutorMAUI.View;

namespace ScriptExecutorMAUI;
public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));
        Routing.RegisterRoute(nameof(AddPage), typeof(AddPage));
        Routing.RegisterRoute(nameof(LogsPage), typeof(LogsPage));
    }
}
