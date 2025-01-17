using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ex03.GarageLogic
{
    public class VehicleInGarage
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerNumber;
        private eVehicleCondition m_VehicleCondition = eVehicleCondition.InRepair;


        public string OwnerNumber
        {
            get { return m_OwnerNumber; }

            set
            {
                if (value.All(char.IsDigit))
                {
                    m_OwnerName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid phone number input");
                }
            }

        }
        public Vehicle GarageVehicle
        {
            get { return m_Vehicle; }

            set { m_Vehicle = value; }

        }
        public string OwnerName
        {
            get { return m_OwnerName; }

            set
            {
                if (value.All(char.IsLetter))
                {
                    m_OwnerName = value;
                }
                else
                {
                    throw new ArgumentException("Invalid owner name input");
                }
            }

        }
        public eVehicleCondition VehicleCondition
        {
            get { return m_VehicleCondition; }
            set
            {

                if (!Enum.IsDefined(typeof(eVehicleCondition), value))
                {
                    Exception ex = new Exception("Invalid vehicle condition input.");
                    throw new ValueOutOfRangeException(ex, (float)eVehicleCondition.Paid, (float)eVehicleCondition.InRepair);
                }
                else
                {
                    m_VehicleCondition = value;
                }
            }
        }
        public void ChangeVehicleCondition(VehicleInGarage.eVehicleCondition i_VehicleConditionToAssign)
        {
            try
            {
                VehicleCondition = i_VehicleConditionToAssign;
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }

        }
        public void InflateWheelsToMaximum()
        {
            try
            {
                foreach (var wheel in m_Vehicle.WheelsList)
                {
                    wheel.InflateAction(wheel.MaxAirPressure - wheel.CurrentAirPressure);
                }
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }

        }
        public enum eVehicleCondition
        {
            InRepair = 1,
            Repaired,
            Paid
        }


        public override string ToString()
        {
            StringBuilder vehicleDetails = new StringBuilder();
            vehicleDetails.AppendLine($"Owner Name: {m_OwnerName}");
            vehicleDetails.AppendLine($"Owner Phone: {m_OwnerNumber}");
            vehicleDetails.AppendLine($"Vehicle Condition: {m_VehicleCondition}");
            vehicleDetails.AppendLine(m_Vehicle.ToString());  // Calls the vehicle's overridden ToString()
            return vehicleDetails.ToString();
        }
    }
}
