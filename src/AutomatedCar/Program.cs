namespace AutomatedCar
{
    using Avalonia;
    using Avalonia.ReactiveUI;
    using System.Runtime.InteropServices;

    class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        public static void Main(string[] args)
        {
            // Show console window
            AllocConsole();

            // Start Avalonia app with classic desktop lifetime
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

            // Optional: Hide console window after Avalonia app closes
            FreeConsole();
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace()
            .UseReactiveUI();
    }
}