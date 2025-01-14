//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Ex03.GarageLogic
//{
//    internal class Factory
//    {

//    }
//}
using System;

namespace Ex03.GarageLogic
{
    internal class Factory
    {
        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LicenseNumber)
        {
            Vehicle vehicle = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    vehicle = new Car(i_ModelName, i_LicenseNumber);  
                    break;

                case eVehicleType.Motorcycle:
                    vehicle = new Motorcycle(i_ModelName, i_LicenseNumber);  
                    break;

                case eVehicleType.Truck:
                    vehicle = new Truck(i_ModelName, i_LicenseNumber);  
                    break;

                default:
                    throw new ArgumentException("Invalid vehicle type.");
            }

            return vehicle;  // The vehicle is returned for data input later.
        }
    }
    public enum eVehicleType
    {
        Car,
        Truck,
        Motorcycle
    }

    public enum eEnergySourceType
    {
        FuelEngine,
        Battery
    }
}
  
  

