using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ClearBufferMask = OpenTK.Graphics.OpenGLES1.ClearBufferMask;

namespace Hello_Triangle;

/*  Vertex Input:
 *  OpenGL is a 3D graphics library, and as such uses 3D coordinates
 *  It doesn't simply transform 3D coordinates to 2D pixels on the screen; OpenGL only processes 3D coordinates when they're in a specific range between -1.0 and 1.0
 *  All coordinates within this so called normalized device coordinates range will end up visible on the screen
 *
 *  For this program, we are trying to render a single triangle
 *  We want to specify a total of three vertices with each vertex having a 3D position
 *  We define them in normalized device coordinates in a float array
 *  Because OpenGL works in 3D space we render a 2D triangle with each vertex having a z position of 0.0, this way the depth remains the same, emulating the 2D effect
 */

/*  Normalized Device Coordinates (NDC):
 *  Once your coordinates have been processed in the vertex shader, they should be in NDC which is a small space where the x, y and z values vary from -1.0 to 1.0
 *  Any coordinates that fall outside this range will be discarded and won't be visible on your screen
 *  Unlike normal screen coordinate systems, the positive y-axis points in the up-direction and (0,0) is located in the middle of the screen, rather than top left
 *  Eventually you will want all coordinates to end up in this coordinate space, otherwise they won't be visible
 *  Your NDC coordinates will then be transformed to screen-space coordinates via the viewpoint transform using the data you provided with GL.Viewport
 *  The resulting screen-space coordinates are then transformed to fragments as inputs to your fragment shader
 * 
 */

public class Window  : GameWindow
{
    private float[] vertices =
    {
        -0.5f, -0.5f, 0.0f, //Bottom-left vertex
        0.5f, -0.5f, 0.0f, //Bottom-right vertex
        0.0f, 0.5f, 0.0f //Top vertex
    };
    
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