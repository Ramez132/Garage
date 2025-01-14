using System;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private eColors eCarColor;
        private eDoors eNumberOfDoors;
        private EnergySource m_EnergySource;

        public Car(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)  // Calls base class constructor
        {
        }

        public EnergySource Energy
        {
            get { return m_EnergySource; }
            set
            {
                m_EnergySource = value;  // Set the energy source

                if (value is FuelEngine fuelEngine)
                {
                    fuelEngine.MaxAmountOfFuelInLiters = 52;
                    fuelEngine.FuelTypes = FuelEngine.eFuelTypes.Octan95;
                }
                else if (value is ElectricBattery battery)
                {
                    battery.MaxBatteryTimeInHours = 5.4f;
                    battery.BatteryTimeLeftInHours = 0;
                }
                else
                {
                    throw new ArgumentException("Invalid energy source for car.");
                }
            }
        }

        internal eDoors Doors
        {
            get { return eNumberOfDoors; }
            set
            {
                if (Enum.IsDefined(typeof(eDoors), value))
                {
                    eNumberOfDoors = value;
                }
                else
                {
                    // Use your original exception format:
                    Exception ex = new Exception("Doors number's input is invalid");
                    throw new ValueOutOfRangeException(ex, (float)eDoors.Five, (float)eDoors.Two);
                }
            }
        }

        internal eColors Color
        {
            get { return eCarColor; }
            set
            {
                if (Enum.IsDefined(typeof(eColors), value))
                {
                    eCarColor = value;
                }
                else
                {
                    // Follow your requested structure:
                    throw new ArgumentException("Color input is invalid");
                }
            }
        }

        public enum eColors
        {
            Blue = 1,
            Black,
            White,
            Gray
        }

        public enum eDoors
        {
            Two = 1,
            Three,
            Four,
            Five
        }
    }
}
