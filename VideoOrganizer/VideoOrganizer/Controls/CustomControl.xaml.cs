// CustomControl.xaml.cs
using System.Windows;
using System.Windows.Controls;

namespace YourNamespace
{
    public partial class CustomControl : System.Windows.Controls.UserControl
    {
        public CustomControl()
        {
            InitializeComponent();
        }

        // Dependency property to bind the string data
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(CustomControl), new PropertyMetadata(string.Empty));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
