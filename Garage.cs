using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleInGarage> m_Vehicles = new Dictionary<string, VehicleInGarage>();//string for LicenseNumber

        public Dictionary<string, VehicleInGarage> Vehicles
        {
            get { return m_Vehicles; }
        }
        public void AddNewVehicleToGarage(string i_LicenseNumber, VehicleInGarage i_VehicleToAdd)
        {
            if (m_Vehicles.ContainsKey(i_LicenseNumber))
            {
                m_Vehicles[i_LicenseNumber].VehicleCondition = VehicleInGarage.eVehicleCondition.InRepair;
                throw new Exception("Vehicle Already in garage");
            }
            else
            {
                m_Vehicles.Add(i_LicenseNumber, i_VehicleToAdd);
            }

        }
        public List<Vehicle> printVehiclesInGarage(bool i_ByFilterOfCondition, VehicleInGarage.eVehicleCondition i_VehicleCondition)
        {
            List<Vehicle> vehiclesToPrintByFilter = new List<Vehicle>();
            if (i_ByFilterOfCondition)
            {
                foreach (var vehicle in m_Vehicles)
                {
                    if (i_VehicleCondition.Equals(vehicle.Value.VehicleCondition))
                    {
                        vehiclesToPrintByFilter.Add(vehicle.Value.GarageVehicle);
                    }
                }
            }
            else
            {
                foreach (var vehicle in m_Vehicles)
                {
                    vehiclesToPrintByFilter.Add(vehicle.Value.GarageVehicle);
                }
            }

            return vehiclesToPrintByFilter;//in the ui prints the list
        }

        public void inflateTiresToMaximumInGarageVehicle(string i_LicneseNumber)
        {
            try
            {
                m_Vehicles[i_LicneseNumber].inflateWheelsToMaximum();
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }
        }
        public void FuelVehicleInGarage(string i_LicenseNumber, FuelEngine.eFuelTypes i_FuelType, float i_AmountOfFuelToAdd)
        {
            try
            {
                if (m_Vehicles[i_LicenseNumber].GarageVehicle is Car &&
                   ((Car)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy is FuelEngine)
                {
                    (((Car)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy as FuelEngine).fuelAction(i_AmountOfFuelToAdd, i_FuelType);
                }

                else if (m_Vehicles[i_LicenseNumber].GarageVehicle is Motorcycle &&
                   ((Motorcycle)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy is FuelEngine)
                {
                    (((Motorcycle)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy as FuelEngine).fuelAction(i_AmountOfFuelToAdd, i_FuelType);
                }
                else if (m_Vehicles[i_LicenseNumber].GarageVehicle is Truck)
                {
                    (((Truck)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy).fuelAction(i_AmountOfFuelToAdd, i_FuelType);
                }

            }

            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ChargeVehicleInGarage(string i_LicenseNumber, float i_AmountOfChargeToAdd)
        {
            try
            {
                if (m_Vehicles[i_LicenseNumber].GarageVehicle is Car &&
                   ((Car)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy is ElectricBattery)
                {
                    (((Car)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy as ElectricBattery).chargeAction(i_AmountOfChargeToAdd);
                }
                else if (m_Vehicles[i_LicenseNumber].GarageVehicle is Motorcycle &&
                   ((Motorcycle)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy is ElectricBattery)
                {
                    (((Motorcycle)m_Vehicles[i_LicenseNumber].GarageVehicle).Energy as ElectricBattery).chargeAction(i_AmountOfChargeToAdd);
                }
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

