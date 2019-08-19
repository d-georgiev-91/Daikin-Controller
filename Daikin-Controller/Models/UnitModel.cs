namespace DaikinController.Models
{
    public class UnitModel
    {
        public string Name { get; set; }

        public double IndoorTemperature { get; set; }

        public double TargetTemperature { get; set; }

        public bool Power { get; set; }

        public Mode Mode { get; set; }
    }
}
