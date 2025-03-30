using System.ComponentModel;
using System.Diagnostics;
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
            SetDatePicker(_viewModel.Date);
            _viewModel.ShowIncorrectDateMessage += IncorrectDateEvent;
            _viewModel.AgeUpdate += UpdateAgeTextBlock;
            _viewModel.DisplayBirthdayMessage += FillBirthdayMessageTextBlock;
            _viewModel.DisplayZodiakSign += UpdateZodiakSignTextBlock;
            _viewModel.DisplayChineeseZodiakSign += UpdateChineeseZodiakSignTextBlock;
            _viewModel.ClearAllCalculatedFields += ClearAllCalculatedFieldsEvent;
            _viewModel.RevealProperties += RevealPropertiesEvent;
            _viewModel.DatePickerUpdate += DatePickerUpdateEventHandler;
            _viewModel.DisplayAdultText += FillAdultTextBlock;
            _viewModel.UpdateProceedButtonStatus += ButtonStatusEventHandler;
            _viewModel.DisplayNameText += FillNameTextBlock;
            _viewModel.DisplaySurnameText += FillSurnameTextBlock;
            _viewModel.DisplayEmailText += FillEmailTextBlock;
            _viewModel.UpdateInputsPanelStatus += InputPanelStatusEventHandler;
        }


        private void InputPanelStatusEventHandler(object sender, bool active)
        {
            AllInputsPanel.IsEnabled = active;
        }

        private void FillNameTextBlock(object sender, string message)
        {
            NameTextBlock.Text = message;
        }

        private void FillSurnameTextBlock(object sender, string message)
        {
            SurnameTextBlock.Text = message;
        }

        private void FillEmailTextBlock(object sender, string message)
        {
            EmailTextBlock.Text = message;
        }


        private void ButtonStatusEventHandler(object sender, bool active)
        {
            ProceedButton.IsEnabled = active;
        }


        private void DatePickerUpdateEventHandler(object sender, DateTime date)
        {
            BirthdayDatePicker.SelectedDate = date;
        }

        private void SetDatePicker(DateTime date)
        {
            BirthdayDatePicker.SelectedDate = date;
        }

        private void RevealPropertiesEvent(object sender, EventArgs e)
        {
            RevealPropertiesPanel();
        }

        private void FillBirthdayMessageTextBlock(object sender, string message)
        {
            CongratTextBlock.Text = message;
        }

        private void FillAdultTextBlock(object sender, string message)
        {
            AdultTextBlock.Text = message;
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
            HidePropertiesPanel();
            CleanAllCalculatedFields();
        }
        private void CleanAllCalculatedFields()
        {
            AgeTextBlock.Text= string.Empty;
            CongratTextBlock.Text= string.Empty;
            ZodiakSignTextBlock.Text = string.Empty;
            ChineeseZodiakSignTextBlock.Text= string.Empty;
            EmailTextBlock.Text=string.Empty;
            NameTextBlock.Text= string.Empty;
            SurnameTextBlock.Text= string.Empty;
            AgeTextBlock.Text = string.Empty;
        }

        private void HidePropertiesPanel()
        {
            PropertiesStackPanel.Visibility = Visibility.Collapsed;
        }

        private void RevealPropertiesPanel()
        {
            PropertiesStackPanel.Visibility = Visibility.Visible;
        }

        private void IncorrectDateMessagebox()
        {
            MessageBox.Show("Entered birth date is incorrect!", "Birth date error!",
            MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void ConfirmDateButtonClick(object sender, RoutedEventArgs e)
        {
            _viewModel.ProseedButtonClick(NameInputBox.Text, SurnameInputBox.Text, EmailInputBox.Text, Convert.ToDateTime(BirthdayDatePicker.SelectedDate));
        }

        private void InputsTextChanged(object sender, EventArgs e)
        {
            _viewModel.CheckInputsStatus(NameInputBox.Text, SurnameInputBox.Text, EmailInputBox.Text, BirthdayDatePicker.Text, ProceedButton.IsEnabled);
        }

    }
}