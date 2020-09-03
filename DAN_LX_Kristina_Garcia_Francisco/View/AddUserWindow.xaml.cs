using DAN_LX_Kristina_Garcia_Francisco.Helper;
using DAN_LX_Kristina_Garcia_Francisco.Model;
using DAN_LX_Kristina_Garcia_Francisco.ViewModel;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace DAN_LX_Kristina_Garcia_Francisco.View
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        /// <summary>
        /// Window constructor for creating users
        /// </summary>
        public AddUserWindow()
        {
            InitializeComponent();
            this.DataContext = new AddUserViewModel(this);
        }

        /// <summary>
        /// Window constructor for editing users
        /// </summary>
        /// <param name="userEdit">user that is bing edited</param>
        public AddUserWindow(tblUser userEdit)
        {
            InitializeComponent();
            this.DataContext = new AddUserViewModel(this, userEdit);
        }

        /// <summary>
        /// User can only imput numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        /// Calcualtes the birth date and places it in the textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            Validations val = new Validations();
            string text = "";
            if (txtJMBG.Text.Length >= 7)
            {
                text = val.CountDateOfBirth(txtJMBG.Text).ToString("dd.MM.yyy.");
            }
            else
            {
                text = "";
            }

            txtDateOfBirth.Text = text;
        }
    }
}
