using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
using NetworkManager.Services;

namespace NetworkManager;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.ConfigureLifecycleEvents(lifecycle => {
#if WINDOWS
            lifecycle.AddWindows(windows => windows.OnWindowCreated((del) => {
                del.ExtendsContentIntoTitleBar = true;
            }));
#endif
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif

#if WINDOWS
        builder.Services.AddSingleton<ITrayService, NetworkManager.Platforms.Windows.TrayService>();
		builder.Services.AddSingleton<INotificationService, NetworkManager.Platforms.Windows.NotificationService>();
#endif

        builder.Services.AddSingleton<MainPage>();

        return builder.Build();
	}
}
