﻿public class WindowService : IWindowService
{
    private IUIFactory _uiFactory;

    public WindowService(IUIFactory uiFactory)
    {
        _uiFactory = uiFactory;
    }

    public void Open(WindowId windowId)
    {
        _uiFactory.CreateWindow(windowId);
        /*switch (windowId)
        {
            case WindowId.Unknown:
                break;
            case WindowId.Shop:
                _uiFactory.CreateWindow(windowId);
                break;
            case WindowId.Pause:
                _uiFactory.CreatePause();
                break;
            case WindowId.Options:
                _uiFactory.CreateOptions();
                break;
            default:
                break;
        }*/
    }
}