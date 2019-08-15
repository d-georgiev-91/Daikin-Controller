using System.Windows.Controls;
using DaikinController.Services;
using DaikinController.Views;

namespace DaikinController.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel() : 
            base(null)
        {
            this.CurrentView = new Home();
        }

        public UserControl CurrentView { get; set; }
    }
}
