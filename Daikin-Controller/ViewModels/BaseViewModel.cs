using System.ComponentModel;
using System.Runtime.CompilerServices;
using DaikinController.Annotations;
using DaikinController.Services;

namespace DaikinController.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public BaseViewModel(DaikinService daikinService)
        {
            DaikinService = daikinService;
        }

        protected DaikinService DaikinService { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}