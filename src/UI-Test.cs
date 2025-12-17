using OpenTK.Graphics.OpenGL;
using OpenTK.Windowing.Common;
using RetroForge.NET;
using RetroForge.NET.Assets.Asset;
using RetroForge.NET.Geometry;
using RetroForge.NET.Graphics;
using System.Runtime.InteropServices;

public class UITest : IRetroForgePlugin
{
    private int i = 0;
    private ShaderProgram BaseShader;
    private VAO vao;
    private Texture tex;

    public override void Update(FrameEventArgs frame_args)
    {
        Logger.Log($"{this} {i++} : {frame_args.Time}", '\r');
    }
    public bool x = true;
    public override void Render(FrameEventArgs frame_args) {
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        BaseShader.Use();
        tex.Bind();
        vao.Draw();
        tex.Unbind();
        BaseShader.End();
        if (Window is null)
            Logger.Error("Window is null");
        else Window.SwapBuffers();
    }
    public override string View() => typeof(UITest).FullName!;
    public override string ToString() => View();

    public override UITest SetFields(Window window, string[] args)
    {
        Window = window;
        this.args = args;
        return this;
    }

    public override void OnLoad()
    {
        //Engine.RegisterPluginData()
        Logger.Log($"{this} loading");
        Shader BaseVert = Shader.CompileShader(Engine.BaseVertSource, Engine.AppData("shaders", "Base.vert.spv"), shaderc.SourceLanguage.Glsl, shaderc.ShaderKind.VertexShader);
        Shader BaseFrag = Shader.CompileShader(Engine.BaseFragSource, Engine.AppData("shaders", "Base.frag.spv"), shaderc.SourceLanguage.Glsl, shaderc.ShaderKind.FragmentShader);
        BaseShader = new ShaderProgram([BaseVert, BaseFrag]);
        vao = new VAO();
        vao.AddMesh(GLMesh.Quad);
        GLMesh.Quad.DebugTris();
        vao.UseVB(0, (vertexBuf) =>
        {
            var mesh = vao.GetMesh(0);
            vertexBuf.AddVerts(mesh.GetBlock(0).vertices);
            vertexBuf.AddFaces(mesh.GetBlock(0).faces);
            vertexBuf.AddTexCoords(mesh.GetBlock(0).texCoords);
            vertexBuf.Upload();
        });
        tex = new(Engine.AppData("images", "BaseTex.png"), true);
        vao.SetAttribPointer<float>(0, 3, 2);
        vao.SetAttribPointer<float>(1, 2, 3, 3);

        Logger.Log($"{this} loaded");
    }
}
