using UnityEngine;

public class Game
{
    public static IInputService InputService;

    public Game()
    {
        RegisterInputService();
    }

    private static void RegisterInputService()
    {
        if (Application.isMobilePlatform)
            InputService = new MobileInputService();
        else
            InputService = new StandaloneInputService();
    }
}