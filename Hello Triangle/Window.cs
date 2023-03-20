using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ClearBufferMask = OpenTK.Graphics.OpenGLES1.ClearBufferMask;

namespace Hello_Triangle;

public class Window  : GameWindow
{
    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        base.OnUpdateFrame(args);

        KeyboardState input = KeyboardState;
        if (input.IsKeyDown(Keys.LeftControl) && input.IsKeyDown(Keys.C))
        {
            Close();
        }
    }

    protected override void OnLoad()
    {
        base.OnLoad();
        
        //Decides colour of window after it gets cleared between frames
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        //Code goes here
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        //Clears screen using colour set in OnLoad
        //This should always be the first function called when rendering
        OpenTK.Graphics.OpenGLES1.GL.Clear(ClearBufferMask.ColorBufferBit);
        
        //Code goes here
        
        //Context.SwapBuffers, almost any modern OpenGL context is what's known as "double-buffered"
        //Double-buffering means that there are two areas that OpenGL draws to.
        //In essence: One area is displayed, while the other is being rendered to
        //SwapBuffers reverses these 
        //A single buffered context could have issues such as screen tearing
        SwapBuffers();
    }

    //This runs whenever the window gets resized
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        //Maps the NDC to the window
        OpenTK.Graphics.OpenGLES1.GL.Viewport(0, 0, e.Width, e.Height);
        
        //This isn't super important and no further code is going to be added here
    }   
}