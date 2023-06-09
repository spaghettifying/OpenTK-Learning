using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using ImGuiNET;
using OpenTK.ImGui;
using System.IO;
using SharpFont;

namespace Debug_Menu;

class Program : GameWindow
{
    private float _objectScale = 1.0f;
    private int _objectColor = 0;

    private ImGuiController _imGuiController;

    public Program(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
        _imGuiController = new ImGuiController(ClientSize.X, ClientSize.Y);
    }

    protected override void OnLoad()
    {
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);

        // Draw your objects here using the _objectScale and _objectColor variables

        _imGuiController.Update(this, (float)args.Time);

        // Create the debug menu
        ImGui.Begin("Debug Menu");

        ImGui.Text("Object Scale");
        

        if (ImGui.InputFloat("##ObjectScaleInput", ref _objectScale))
        {
            
        }

        ImGui.Text("Object Color");

        if (ImGui.RadioButton("Red", ref _objectColor, 0))
        {
            GL.ClearColor(Color.Red);
        }

        if (ImGui.RadioButton("Green", ref _objectColor, 1))
        {
            GL.ClearColor(Color.Green);
        }

        if (ImGui.RadioButton("Blue", ref _objectColor, 2))
        {
            GL.ClearColor(Color.Blue);
        }

        ImGui.End();

        _imGuiController.Render();

        SwapBuffers();
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

        _imGuiController.WindowResized(ClientSize.X, ClientSize.Y);
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        if (KeyboardState.IsKeyDown(Keys.C) && KeyboardState.IsKeyDown(Keys.LeftControl))
        {
            Close();
        }
    }

    protected override void OnUnload()
    {
        _imGuiController.Dispose();
    }

    static void Main(string[] args)
    {
        var gameWindowSettings = new GameWindowSettings();
        var nativeWindowSettings = new NativeWindowSettings();

        using (var window = new Program(gameWindowSettings, nativeWindowSettings))
        {
            window.Run();
        }
    }

    void DisplayText()
    {
        // Load the font file
        Library library = new Library();
        Face face = library.NewFace("font.ttf", 0);

// Set the font size and load the glyph data
        face.SetCharSize(0, 24, 96, 96);

// Specify the text to be rendered
        string text = "Hello, world!";

// Calculate the total width of the text
        int totalWidth = 0;
        foreach (char c in text)
        {
            face.LoadGlyph(face.GetCharIndex(c), LoadFlags.Default, LoadTarget.Normal);
            totalWidth += face.Glyph.Bitmap.Width;
        }

// Set up the OpenGL context and viewport
        GL.MatrixMode(OpenTK.Graphics.OpenGL.MatrixMode.Projection);
        GL.LoadIdentity();
        GL.Ortho(0, ClientSize.X, ClientSize.Y, 0, -1, 1);
        GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);

// Set up the texture and vertex data
        int textureID = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, textureID);
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, totalWidth, face.Glyph.Bitmap.Rows, 0, PixelFormat.Red, PixelType.UnsignedByte, IntPtr.Zero);

        float x = 10;
        float y = 10;

// Render the text
        foreach (char c in text)
        {
            face.LoadGlyph(face.GetCharIndex(c), LoadFlags.Default, LoadTarget.Normal);

            // Upload the glyph bitmap data to the texture
            GL.TexSubImage2D(TextureTarget.Texture2D, 0, (int)x, 0, face.Glyph.Bitmap.Width, face.Glyph.Bitmap.Rows, PixelFormat.Red, PixelType.UnsignedByte, face.Glyph.Bitmap.Buffer);

            // Render the glyph quad
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0);
            GL.Vertex2(x, y);
            GL.TexCoord2(1, 0);
            GL.Vertex2(x + face.Glyph.Bitmap.Width, y);
            GL.TexCoord2(1, 1);
            GL.Vertex2(x + face.Glyph.Bitmap.Width, y + face.Glyph.Bitmap.Rows);
            GL.TexCoord2(0, 1);
            GL.Vertex2(x, y + face.Glyph.Bitmap.Rows);
            GL.End();

            x += face.Glyph.Bitmap.Width;
        }
    }
}