using System.Collections.Generic;
using System.Collections.ObjectModel;
using DaikinController.Models;

namespace DaikinController.Services
{
    public class DaikinService
    {
        public IEnumerable<UnitModel> GetUnits()
        {
            return new List<UnitModel>
            {
                new UnitModel
                {
                    Name = "Kids Room",
                    IndoorTemperature = 18.0,
                    OutdoorTemperature = 23.0,
                    Mode = Mode.Cold,
                    Power = Power.Off
                },
                new UnitModel
                {
                    Name = "Living Room",
                    IndoorTemperature = 23.0,
                    OutdoorTemperature = 24.0,
                    Mode = Mode.Auto,
                    Power = Power.On
                }
            };
        }
    }
}