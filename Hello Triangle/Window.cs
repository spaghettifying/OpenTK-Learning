using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Hello_Triangle;

public class Window  : GameWindow
{
    private Shader shader;
    
    private int VertexBufferObject;
    private int VertexArrayObject;
    
    
    
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
        VertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
        
        VertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(VertexArrayObject);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        shader = new Shader(@"C:\Users\adam\Documents\GitHub\OpenTK-Learning\Hello Triangle\shader.vert", @"C:\Users\adam\Documents\GitHub\OpenTK-Learning\Hello Triangle\shader.frag");
        shader.Use();
    }

    protected override void OnUnload()
    {
        base.OnUnload();
        shader.Dispose();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        base.OnRenderFrame(args);
        
        //Clears screen using colour set in OnLoad
        //This should always be the first function called when rendering
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        //Code goes here
        shader.Use();
        GL.BindVertexArray(VertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
        
        SwapBuffers();
    }

    //This runs whenever the window gets resized
    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        //Maps the NDC to the window
        GL.Viewport(0, 0, e.Width, e.Height);
        
        //This isn't super important and no further code is going to be added here
    }   
}