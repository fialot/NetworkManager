using System.Collections.Specialized;

namespace NetworkManager.Contracts.Services;

public interface IAppNotificationService
{
    void Initialize();

    bool Show(string payload);
    bool ShowMessage(string caption, string text);
    NameValueCollection ParseArguments(string arguments);

    void Unregister();
}
