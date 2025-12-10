using OpenTK.Windowing.Common;
using RetroForge.NET;

public class UITest : IRetroForgePlugin
{
    private int i = 0;
    public override UITest SetFields(Window window, string[] args)
    {
        Window = window;
        this.args = args;
        return this;
    }

    public override void Update(FrameEventArgs frame_args)
    {
        Logger.Log($"{i++} : {frame_args.Time}", '\r');
    }
    public override void Render(FrameEventArgs frame_args) { }
    public override string View() => typeof(UITest).FullName!;
    public override string ToString() => View();
}
