using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.FuelEngine;
using static Ex03.GarageLogic.VehicleInGarage;

namespace Ex03.ConsoleUI
{
    internal class ConsoleUI
    {
        private readonly int r_NumberOfOptionsInMenu = 8;
        private Garage m_Garage = new Garage();
        public ConsoleUI()
        {
            eMenuOptions userChoice = 0;
            printMenu();
            userChoice = checkMenuOptionInput();
            while (userChoice != eMenuOptions.Exit)
            {
                try
                {
                    switch (userChoice)
                    {
                        case eMenuOptions.AddNewVehicle:
                            {

                            }
                            break;
                        case eMenuOptions.ShowLicenseNumbersByFilter:
                            {
                                showLicenseNumbersToUserByFilter();
                            }
                            break;
                        case eMenuOptions.ChangeStatusOfVehicle:
                            {
                                changeVehicleStatus();
                            }
                            break;
                        case eMenuOptions.InfaltingAirInWheels:
                            {
                                infalteWheelsInVehicleToMaximum();
                            }
                            break;
                        case eMenuOptions.FillGasolineTank:
                            {
                                fillGasolineInTank();
                            }
                            break;
                        case eMenuOptions.ChargeBattery:
                            {
                                chargeVehicleBattery();
                            }
                            break;
                        case eMenuOptions.ShowVehicleDetails:
                            {
                                showVehicleFullDetails();
                            }
                            break;
                        case eMenuOptions.Exit:
                            break;
                    }
                }
                catch (ValueOutOfRangeException vOutRangeEx)
                {
                    Console.WriteLine($"{vOutRangeEx.Message}");
                }
                catch (ArgumentException argEx)
                {
                    Console.WriteLine($"{argEx.Message}");
                }
                catch (FormatException formEx)
                {
                    Console.WriteLine($"{formEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }

            }
        }

        private void showLicenseNumbersToUserByFilter()
        {   
            bool userResponse = askForFilterByLicense();

            eVehicleCondition vehicleConditionFromUser = default;
            if (userResponse)
            {
                getVehicleConditionFromUser(out vehicleConditionFromUser);
            }

            List<Vehicle> vehiclesToPrint = m_Garage.GetVehiclesInGarageByFilter(userResponse, vehicleConditionFromUser);


            if (vehiclesToPrint.Count == 0)
            {
                throw new Exception("No vehicles match the selected filter.");
            }
            else
            {
                Console.WriteLine("Vehicles in the garage:");
                foreach (Vehicle vehicle in vehiclesToPrint)
                {
                    Console.WriteLine(vehicle.ToString()); // Display vehicle details
                }
            }
        }

        private bool askForFilterByLicense()
        {
            while (true)
            {
                Console.WriteLine("Do you want to filter the vehicles by license? (yes/no):");
                string input = Console.ReadLine()?.Trim().ToLower(); 

                if (input == "yes")
                {
                    return true; 
                }
                else if (input == "no")
                {
                    return false; 
                }
                else
                {
                    throw new Exception("Invalid input. Please enter 'yes' or 'no'.");
                }
            }
        }

        private void changeVehicleStatus()
        {

            try
            {
                string licenseNumber;
                eVehicleCondition eVehicleConditionFromUser;

                getLicenseNumberFromUser(out licenseNumber);

                getVehicleConditionFromUser(out eVehicleConditionFromUser);

                m_Garage.Vehicles[licenseNumber].ChangeVehicleCondition(eVehicleConditionFromUser);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
            catch (ValueOutOfRangeException ex)
            {
                throw ex;
            }


        }

        private void getVehicleConditionFromUser(out eVehicleCondition io_eVehicleConditionFromUser)
        {
            Console.WriteLine("Please select the vehicle condition:");
            Console.WriteLine("Options: InRepair, Repaired, Paid");

            string input = Console.ReadLine();

            if (!Enum.TryParse(input, true, out io_eVehicleConditionFromUser) ||
                !Enum.IsDefined(typeof(eVehicleCondition), io_eVehicleConditionFromUser))
            {
                throw new ArgumentException($"'{input}' is not a valid vehicle condition. Please enter: InRepair, Repaired, or Paid.");
            }
        }

        private void infalteWheelsInVehicleToMaximum()
        {
            try
            {
                string licenseNumber;

                getLicenseNumberFromUser(out licenseNumber);
                m_Garage.Vehicles[licenseNumber].InflateWheelsToMaximum();
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (ValueOutOfRangeException ex)
            {

                throw ex;
            }

        }

        private void showVehicleFullDetails()
        {
            try
            {
                string licenseNumber;
                getLicenseNumberFromUser(out licenseNumber);
                Console.Write(m_Garage.Vehicles[licenseNumber].ToString());
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (ValueOutOfRangeException ex)
            {

                throw ex;
            }


        }

        private eMenuOptions checkMenuOptionInput()
        {
            eMenuOptions o_parsedInput;

            while (true)
            {
                string userInput = Console.ReadLine().Trim();

                if (!int.TryParse(userInput, out int userNumber))
                {
                    throw new FormatException("Invalid choice, please enter a valid number.");
                }

                else if (userNumber > (int)eMenuOptions.Exit || userNumber < (int)eMenuOptions.AddNewVehicle)
                {
                    throw new ValueOutOfRangeException(new Exception("Option not in range"),
                        (int)eMenuOptions.Exit, (int)eMenuOptions.AddNewVehicle);
                }
                else
                {
                    o_parsedInput = (eMenuOptions)userNumber;
                    break;
                }
            }

            return o_parsedInput;
        }


        public void printMenu()
        {

            StringBuilder menuString = new StringBuilder();
            menuString.Append($"Welcome to Ohad's and Ramez's Garage.{Environment.NewLine}" +
                $"Please select the option you want:{Environment.NewLine}" +
                $"1. Enter new vehicle to the garage.{Environment.NewLine}" +
                $"2. Show license number of vehicles by filter.{Environment.NewLine}" +
                $"3. Change status of vehicle.{Environment.NewLine}" +
                $"4. Inflating air in wheels.{Environment.NewLine}" +
                $"5. Fill gas tank.{Environment.NewLine}" +
                $"6. Charge battery.{Environment.NewLine}" +
                $"7. Show vehicle details.{Environment.NewLine}" +
                $"8. Exit");

            Console.WriteLine(menuString);

        }

        private void fillGasolineInTank()
        {
            try
            {
                getLicenseNumberFromUser(out string licenseNumber);

                getFuelTypeFromUser(out eFuelTypes fuelType);

                getAmountOfFuel(out float amountOfFuel);

                m_Garage.FuelVehicleInGarage(licenseNumber, fuelType, amountOfFuel);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (ValueOutOfRangeException ex)
            {

                throw ex;
            }


        }
        private void chargeVehicleBattery()
        {
            try
            {
                getLicenseNumberFromUser(out string licenseNumber);

                getNumberOfMinutesToChargeTheBattery(out float amountOfMinutes);

                m_Garage.ChargeVehicleInGarage(licenseNumber, amountOfMinutes);
            }
            catch (FormatException ex)
            {
                throw ex;
            }
            catch (ArgumentException ex)
            {

                throw ex;
            }
            catch (ValueOutOfRangeException ex)
            {

                throw ex;
            }


        }

        private void getNumberOfMinutesToChargeTheBattery(out float io_amount)
        {
            bool isValidInput = false;
            io_amount = 0f;

            Console.WriteLine("Please enter the number of minutes to charge the battery:");

            do
            {
                try
                {
                    string input = Console.ReadLine();
                    io_amount = float.Parse(input);

                    if (io_amount <= 0)
                    {
                        Console.WriteLine("The number of minutes must be a positive number. Please try again.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The number entered is too large or too small. Please try again.");
                }
            } while (!isValidInput);
        }

        private void getAmountOfFuel(out float io_amountOfFuel)
        {
            bool isValidInput = false;
            io_amountOfFuel = 0f;

            Console.WriteLine("Please enter amount of fuel:");

            do
            {
                try
                {
                    string input = Console.ReadLine();
                    io_amountOfFuel = float.Parse(input);
                    if (io_amountOfFuel <= 0)
                    {
                        Console.WriteLine("Fuel amount must be a positive number. Please try again.");
                    }
                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid numeric value.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("The number entered is too large or too small. Please try again.");
                }
            } while (!isValidInput);
        }


        private void getFuelTypeFromUser(out FuelEngine.eFuelTypes io_fuelType)
        {
            bool isValidInput = false;
            io_fuelType = FuelEngine.eFuelTypes.Octan95;

            Console.WriteLine("Please select a fuel type from the following options:");


            foreach (string fuelName in Enum.GetNames(typeof(FuelEngine.eFuelTypes)))
            {
                Console.WriteLine($"- {fuelName}");
            }

            do
            {
                try
                {
                    Console.Write("Enter fuel type: ");
                    string input = Console.ReadLine();

                    // Try to parse the input as an enum value
                    if (Enum.TryParse(input, true, out FuelEngine.eFuelTypes parsedFuelType) &&
                        Enum.IsDefined(typeof(FuelEngine.eFuelTypes), parsedFuelType))
                    {
                        io_fuelType = parsedFuelType;
                        isValidInput = true;
                    }
                    else
                    {
                        Console.WriteLine("Invalid fuel type. Please try again.");
                    }
                }
                catch (FormatException forEx)
                {
                    Console.WriteLine($"Error: {forEx.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            } while (!isValidInput);
        }


        private void getLicenseNumberFromUser(out string licenseNumber)
        {
            bool isValidInput = false;
            licenseNumber = string.Empty;

            Console.WriteLine("Please enter the license number:");

            do
            {
                try
                {
                    licenseNumber = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(licenseNumber))
                    {
                        Console.WriteLine("License number cannot be empty. Please try again.");
                    }

                    else
                    {
                        isValidInput = true;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } while (!isValidInput);
        }

        public enum eMenuOptions
        {
            AddNewVehicle = 1,
            ShowLicenseNumbersByFilter,
            ChangeStatusOfVehicle,
            InfaltingAirInWheels,
            FillGasolineTank,
            ChargeBattery,
            ShowVehicleDetails,
            Exit
        }
    }

}

