using System;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacityInCC;
        private EnergySource m_EnergySource;

        public Motorcycle(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)  // Calls the base constructor
        {
        }

        public EnergySource Energy
        {
            get { return m_EnergySource; }
            set
            {
                if (value is FuelEngine fuelEngine)
                {
                    fuelEngine.MaxAmountOfFuelInLiters = 6.2f;
                    fuelEngine.FuelTypes = FuelEngine.eFuelTypes.Octan98;
                }
                else if (value is ElectricBattery battery)
                {
                    battery.MaxBatteryTimeInHours = 2.9f;
                    battery.BatteryTimeLeftInHours = 0f;
                }
                else
                {
                    throw new ArgumentException("Invalid energy source for motorcycle.");
                }
                m_EnergySource = value;  // Set the energy source after validation
            }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacityInCC; }
            set
            {
                if (value < 0)
                {
                    Exception ex = new Exception("Invalid engine capacity input.");
                    throw new ValueOutOfRangeException(ex, int.MaxValue, 0f);  // Your custom format
                }
                m_EngineCapacityInCC = value;
            }
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (!Enum.IsDefined(typeof(eLicenseType), value))
                {
                    Exception ex = new Exception("Invalid license type input.");
                    throw new ValueOutOfRangeException(ex, (float)eLicenseType.A4, (float)eLicenseType.A1);  // Your custom format
                }
                m_LicenseType = value;
            }
        }

        public enum eLicenseType
        {
            A1 = 1,
            A2,
            A3,
            A4
        }
    }
}
