using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ex03.GarageLogic.FuelEngine;

namespace Ex03.GarageLogic
{
    internal class ElectricBattery: EnergySource
    {
        //from father- m_EnergyPercentageLeft;
        private float m_BatteryTimeLeftInHours;
        private float m_MaxBatteryTimeInHours;
        
        public ElectricBattery(float i_value)
        {
            base.EnergyPercentageLeft = i_value;
        }

        public float BatteryTimeLeftInHours
        {
            get { return m_BatteryTimeLeftInHours; }
            set
            {
                if (value < 0 || value > m_MaxBatteryTimeInHours)
                {
                    Exception ex = new Exception("Invalid input for Battery time left");
                    throw new ValueOutOfRangeException(ex, m_MaxBatteryTimeInHours, 0f);
                }
                else
                {
                    m_BatteryTimeLeftInHours = value;
                }
            }
        }
        public float MaxBatteryTimeInHours
        {
            get { return m_MaxBatteryTimeInHours; }
            set
            {
                if(value == 5.4 || value == 2.9)
                {
                    m_MaxBatteryTimeInHours = value;
                }
              
                else
                {
                    throw new Exception("Invalid input for maximum battery time");
                }
            }

        }
        public void chargeAction(float i_ChargeToAdd)
        {
            if (m_BatteryTimeLeftInHours + i_ChargeToAdd > m_MaxBatteryTimeInHours || i_ChargeToAdd < 0)
            {
                Exception ex = new Exception("Invalid input of amount of charge in charge action");
                throw new ValueOutOfRangeException(ex, m_MaxBatteryTimeInHours - m_BatteryTimeLeftInHours, 0f);
            }
        
            else
            {
                m_BatteryTimeLeftInHours += i_ChargeToAdd;
            }
        }

    }

}
