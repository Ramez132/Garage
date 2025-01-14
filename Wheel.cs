using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.GarageLogic
{
    public class Wheel
    {   
        private float m_MaxAirPressure;
        private float m_CurrentAirPressure;
        private string m_ProducerName;
        public float MaxAirPressure
        {
            get { return m_MaxAirPressure; }

            set { if(value > 0)
                {
                    m_MaxAirPressure = value;
                }
                else
                {
                    Exception ex = new Exception("The max air pressure must be a positive number");
                    throw new ValueOutOfRangeException(ex,m_MaxAirPressure,0f);
                }
            }
        }

        private string ProducerName
        {
            get { return m_ProducerName; }

            set
            {
                if (value != string.Empty)
                {
                    m_ProducerName = value;
                }
                else
                {
                    throw new FormatException("Producer's name invalid.");
                };
            }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }

            set
            {
                if (value >= 0 && value <= m_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    Exception ex = new Exception("Wheel's air pressure is invalid.");
                    throw new ValueOutOfRangeException(ex, m_MaxAirPressure, 0f);
                }
            }
        }

        public void InflateAction(float i_AirToAddToWheel)
        {

            if (i_AirToAddToWheel + m_CurrentAirPressure <= m_MaxAirPressure)
            {
                m_CurrentAirPressure += i_AirToAddToWheel;
            }
            else
            {
                Exception ex = new Exception("Air pressure to add is invalid.");
                throw new ValueOutOfRangeException(ex, m_MaxAirPressure - m_CurrentAirPressure, 0f);
            }


        }
        public override string ToString()
        {
            string wheel = string.Format(
                "Producer is {0}, the current air pressure is {1} out of {2}.{3}",m_ProducerName, m_CurrentAirPressure,
                m_MaxAirPressure);

            return wheel;
        }
    }
}

