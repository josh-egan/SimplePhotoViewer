namespace SimplePhotoViewer.UI.Views
{
    public partial class ShellView
    {
        public ShellView()
        {
            InitializeComponent();
            MouseLeftButtonDown += (s, a) => DragMove();
        }
    }
}