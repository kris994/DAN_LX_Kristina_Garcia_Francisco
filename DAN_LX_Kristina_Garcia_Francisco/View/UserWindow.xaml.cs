using DAN_LX_Kristina_Garcia_Francisco.ViewModel;
using System.Windows;

namespace DAN_LX_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            this.DataContext = new UserViewModel(this);
        }
    }
}
