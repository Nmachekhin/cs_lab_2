using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MachekhinZodiak
{
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel();
            DataContext = _viewModel;
            _viewModel.ShowIncorrectDateMessage += IncorrectDateEvent;
            _viewModel.AgeUpdate += UpdateAgeTextBlock;
            _viewModel.DisplayBirthdayMessage += FillBirthdayMessageTextBlock;
            _viewModel.DisplayZodiakSign += UpdateZodiakSignTextBlock;
            _viewModel.DisplayChineeseZodiakSign += UpdateChineeseZodiakSignTextBlock;
            _viewModel.ClearAllCalculatedFields += ClearAllCalculatedFieldsEvent;
        }
        

        private void FillBirthdayMessageTextBlock(object sender, string message)
        {
            CongratTextBlock.Text = message;
        }

        private void UpdateAgeTextBlock(object sender, int age)
        {
            AgeTextBlock.Text = age.ToString();
        }

        private void UpdateZodiakSignTextBlock(object sender, string sign)
        {
            ZodiakSignTextBlock.Text = sign;
        }

        private void UpdateChineeseZodiakSignTextBlock(object sender, string sign)
        {
            ChineeseZodiakSignTextBlock.Text = sign;
        }

        private void IncorrectDateEvent(object sender, EventArgs e)
        {
            IncorrectDateMessagebox();
        }

        private void ClearAllCalculatedFieldsEvent(object sender, EventArgs e)
        {
            CleanAllCalculatedFields();
        }
        private void CleanAllCalculatedFields()
        {
            AgeTextBlock.Text= string.Empty;
            CongratTextBlock.Text= string.Empty;
            ZodiakSignTextBlock.Text = string.Empty;
            ChineeseZodiakSignTextBlock.Text= string.Empty;
        }

        private void IncorrectDateMessagebox()
        {
            MessageBox.Show("Entered birth date is incorrect!", "Birth date error!",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void ConfirmDateButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ConfirmDateButtonClick(sender, e);
        }

    }
}