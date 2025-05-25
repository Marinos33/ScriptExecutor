using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace ScriptExecutor.Desktop;

//TODO :
// 1. repair edit and remove button to be clickable
// 2. implmement the update
// 3. implement the remove
// 4. Implement the try script with a cmd opening when using this
// 5. Add a run script buttont o grid view to run the script
// 6. Add styling
internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}