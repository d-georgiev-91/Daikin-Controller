using System.Collections.Generic;
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
                    TargetTemperature = 18.0,
                    IndoorTemperature = 23.0,
                    Mode = Mode.Cold,
                    Power = true
                },
                new UnitModel
                {
                    Name = "Living Room",
                    TargetTemperature = 23.0,
                    IndoorTemperature= 24.0,
                    Mode = Mode.Auto,
                    Power = false
                }
            };
        }
    }
}