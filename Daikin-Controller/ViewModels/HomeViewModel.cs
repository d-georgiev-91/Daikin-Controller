using System.Collections.ObjectModel;
using DaikinController.Models;
using DaikinController.Services;

namespace DaikinController.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<DiscoveryInfoModel> units;

        public HomeViewModel() : base(new DaikinService())
        {
        }

        private async void LoadData()
        {
            var units = await DaikinService.GetUnits();

            foreach (var unit in units)
            {
                Units.Add(unit);
            }
        }

        public ObservableCollection<DiscoveryInfoModel> Units
        {
            get
            {
                if (units == null)
                {
                    units = new ObservableCollection<DiscoveryInfoModel>();
                    this.LoadData();
                }

                return units;
            }
            set
            {
                units = value;
                OnPropertyChanged();
            }
        }
    }
}