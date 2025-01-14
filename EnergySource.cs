using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class EnergySource
    {
        private float m_EnergyPercentageLeft;
        public float EnergyPercentageLeft
        {
            get { return m_EnergyPercentageLeft; }

            set
            {
                if (value < 0 || value > 100)
                {
                    Exception ex = new Exception("Invalid input for percentages of energy remaining .");
                    throw new ValueOutOfRangeException(ex, 100f, 0f);
                }
                else
                {
                    m_EnergyPercentageLeft = value;
                }
            }
        }
    }
}