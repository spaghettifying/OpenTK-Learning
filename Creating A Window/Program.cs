using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Creating_A_Window;

internal class Program
{
    static void Main()
    {
        var nativeWindowSettings = new NativeWindowSettings()
        {
            Size = new Vector2i(800, 600),
            Title = "LearnOpenTK",
            //This is needed to run on macos, just included it if I need to look back at this in the future
            Flags = ContextFlags.ForwardCompatible
        };
        
        //This line creates a new instance, and wraps the instance in a using statement so it's automatically disposed once we've exited the block
        using (Game game = new Game(GameWindowSettings.Default, nativeWindowSettings))
        {
            game.Run();
        }
    }
}