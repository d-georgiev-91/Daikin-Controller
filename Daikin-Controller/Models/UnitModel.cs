namespace DaikinController.Models
{
    public class UnitModel
    {
        public string Name { get; set; }

        public double IndoorTemperature { get; set; }

        public double OutdoorTemperature { get; set; }

        public Power Power { get; set; }

        public Mode Mode { get; set; }
    }
}
