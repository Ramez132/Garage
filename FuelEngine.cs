using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    public class FuelEngine : EnergySource
    {
        private eFuelTypes m_FuelType;
        private float m_CurrentAmountOfFuelInLiters;
        private float m_MaxAmountOfFuelInLiters;
        //from father - m_EnergyPercentageLeft;

        public FuelEngine(float i_value)
        {
            base.EnergyPercentageLeft = i_value;
        }
        public float CurrentAmountOfFuel
        {
            get { return m_CurrentAmountOfFuelInLiters; }

            set
            {
                if (value > 0 && value < m_MaxAmountOfFuelInLiters)
                {
                    m_CurrentAmountOfFuelInLiters = value;
                }
                else
                {
                    Exception ex = new Exception("Invalid input of amount of fuel");
                    throw new ValueOutOfRangeException(ex, m_MaxAmountOfFuelInLiters, 0f);
                }
            }
        }
        public float MaxAmountOfFuelInLiters
        {
            get { return m_MaxAmountOfFuelInLiters; }
            set
            {
                if (value != 52 || value != 125 || value != 6.2)//enum
                {
                    throw new Exception("Invalid input of maximum amount of fuel");
                }
                else
                {
                    m_MaxAmountOfFuelInLiters = value;
                }

            }
        }
        public void fuelAction(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            if (m_CurrentAmountOfFuelInLiters + i_FuelToAdd > m_MaxAmountOfFuelInLiters || i_FuelToAdd < 0)
            {
                Exception ex = new Exception("Invalid input of amount of fuel in fuel action");
                throw new ValueOutOfRangeException(ex, m_MaxAmountOfFuelInLiters - m_CurrentAmountOfFuelInLiters, 0f);
            }
            else if (!Enum.IsDefined(typeof(eFuelTypes), i_FuelType) || i_FuelType != m_FuelType)
            {
                throw new ArgumentException("Invalid input of type of fuel in fuel action");
            }

            else
            {
                m_CurrentAmountOfFuelInLiters += i_FuelToAdd;
            }
        }
        public eFuelTypes FuelTypes
        {
            get { return m_FuelType; }


            set
            {
                if (Enum.IsDefined(typeof(eFuelTypes), value))
                {
                    m_FuelType = value;
                }
                else
                {
                    throw new ArgumentException("Invalid fuel type");
                }
            }
        }

        public enum eFuelTypes
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

    }
}
