namespace LootBoot.Epilogue.Views;
public partial class Zone : UserControl
{
    public Zone() => InitializeComponent();
    private void Storyboard_Completed(object sender, System.EventArgs e) => (DataContext as ZoneViewModel)?.ActionCompleteCommand?.Execute(null);
}
