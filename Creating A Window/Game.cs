using OpenTK;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Creating_A_Window;

public class Game : GameWindow
{
    public Game(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(
        gameWindowSettings, nativeWindowSettings)
    {
    }


    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        //Get the state of the keyboard this frame
        // 'KeyboardState' is a property of GameWindow
        KeyboardState input = KeyboardState;

        if (input.IsKeyDown(Keys.LeftControl) && input.IsKeyDown(Keys.C))
        {
            Close();
        }
    }

    
}