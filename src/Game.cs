namespace Game;

public partial class Game : Node
{
    public override void _EnterTree()
    {
        GetNode("/root/DebugMenu").Set("style", 2);
    }
}
