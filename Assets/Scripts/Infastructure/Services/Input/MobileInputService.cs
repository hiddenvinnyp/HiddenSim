using UnityEngine;

public class MobileInputService : InputService
{
    public override Vector2 Axis => SimpleInputAxis();
}
