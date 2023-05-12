namespace NetworkManager.Services;

public interface ITrayService
{
    void Initialize();

    Action LeftClickHandler { get; set; }
    Action RightClickHandler { get; set; }
    Action DoubleClickHandler { get; set; }
}
