using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using Windows.ApplicationModel;
using WinRT;

namespace CraftSeal;

public partial class App : Application
{
    private Window? _window;

    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        var services = new ServiceCollection();

        _window = new MainWindow(services.BuildServiceProvider());
        _window.Activate();
    }

    [STAThread]
    public static void Main(string[] args)
    {
        try
        {
            _ = Package.Current;
        }
        catch (InvalidOperationException)
        {
            // Unpackaged
            Bootstrap.Initialize(0x00010007);
        }

        ComWrappersSupport.InitializeComWrappers();
        Start((p) =>
        {
            var context = new DispatcherQueueSynchronizationContext(DispatcherQueue.GetForCurrentThread());
            SynchronizationContext.SetSynchronizationContext(context);
            _ = new App();
        });
    }
}
