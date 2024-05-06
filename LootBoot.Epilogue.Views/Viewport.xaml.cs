namespace LootBoot.Epilogue.Views;
public partial class Viewport : UserControl
{
    public Viewport()
    {
        InitializeComponent();
        PORT.Children.Add(Engine.Viewport.Draw.Element);
    }
}
