namespace LootBoot.Epilogue.Views;
public partial class Startup : UserControl
{
    public Startup() => InitializeComponent();
    private void Storyboard_Completed(object sender, EventArgs e) => (DataContext as StartupViewModel)?.LoadingCompleteCommand?.Execute(null);
}
