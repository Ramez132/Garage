using System;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_TransportsRefrigeratedMaterials;
        private float m_CargoVolume;
        private FuelEngine m_EnergySource;

        public Truck(string i_ModelName, string i_LicenseNumber)
            : base(i_ModelName, i_LicenseNumber)  // Calls the base constructor
        {
        }

        public FuelEngine Energy
        {
            get { return m_EnergySource; }
            set
            {
                m_EnergySource.MaxAmountOfFuelInLiters = 125f;
                m_EnergySource.FuelTypes = FuelEngine.eFuelTypes.Soler;
                m_EnergySource = value;  // Set the energy source after validation
            }
        }

        public float CargoVolume
        {
            get { return m_CargoVolume; }
            set
            {
                if (value < 0)
                {
                    Exception ex = new Exception("Invalid cargo volume input.");
                    throw new ValueOutOfRangeException(ex, float.MaxValue, 0f);  // Your custom format
                }
                else
                {
                    m_CargoVolume = value;
                }
 
            }
        }

        public bool TransportsRefrigeratedMaterials
        {
            get { return m_TransportsRefrigeratedMaterials; }
            set { m_TransportsRefrigeratedMaterials = value; }
        }
    }
}
