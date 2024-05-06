namespace LootBoot.Epilogue.Views;
public partial class ZoneCollectable : UserControl
{
    public ZoneCollectable() => InitializeComponent();
    private void Storyboard_Completed(object sender, EventArgs e) => (DataContext as CollectableViewModel)?.ActionCompleteCommand?.Execute(null);
}
