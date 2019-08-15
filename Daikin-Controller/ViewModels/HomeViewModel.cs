using System.Collections.ObjectModel;
using DaikinController.Models;
using DaikinController.Services;

namespace DaikinController.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<UnitModel> units;

        public HomeViewModel() : base(new DaikinService())
        {
            
        }

        public ObservableCollection<UnitModel> Units    
        {
            get => units ?? (units = new ObservableCollection<UnitModel>(DaikinService.GetUnits()));
            set
            {
                units = value;
                OnPropertyChanged();
            }
        }
    }
}
