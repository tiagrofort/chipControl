using System.Windows.Controls;

namespace ChipControl.Presentation.WPF.Views;

public partial class PlaceholderView : UserControl
{
    public string Titulo { get; } = null!;

    public PlaceholderView()
    {
        InitializeComponent();
    }

    public PlaceholderView(string titulo) : this()
    {
        Titulo = titulo;
        DataContext = this;
    }
}
