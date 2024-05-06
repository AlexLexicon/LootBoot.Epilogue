namespace LootBoot.Epilogue.Views;
public partial class Menu : UserControl
{
    public Menu() => InitializeComponent();
    private void Storyboard_Completed(object sender, EventArgs e) => (DataContext as MenuViewModel)?.ActionCompleteCommand?.Execute(null);
}
