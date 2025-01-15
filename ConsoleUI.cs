using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03.GarageLogic;

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
                                addVehicleToGarage();
                            }
                            break;
                        case eMenuOptions.ShowLicenseNumberByFilter:
                            {

                            }
                            break;
                        case eMenuOptions.ChangeStatusOfVehicle:
                            {

                            }
                            break;
                        case eMenuOptions.InfaltingAirInWheels:
                            {

                            }
                            break;
                        case eMenuOptions.FillGasolineTank:
                            {

                            }
                            break;
                        case eMenuOptions.ChargeBattery:
                            {

                            }
                            break;
                        case eMenuOptions.ShowVehicleDetails:
                            {

                            }
                            break;
                        case eMenuOptions.Exit:
                            {

                            }
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


        public void printMenu()//out eMenuOptions io_UserChoice)
        {

            StringBuilder menuString = new StringBuilder();
            menuString.Append($"Welcome to Garage name.{Environment.NewLine}" +
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


            //string userInput = Console.ReadLine().Trim();
            //if (!int.TryParse(userInput, out int userNumber))
            //{
            //    throw new FormatException("Invalid choice, please enter a valid number.");
            //}

            //else if (userNumber > (int)eMenuOptions.Exit || userNumber < (int)eMenuOptions.AddNewVehicle)
            //{
            //    throw new ValueOutOfRangeException(new Exception("Option not in range"),
            //        (int)eMenuOptions.Exit, (int)eMenuOptions.AddNewVehicle);
            //}
            //else
            //{
            //    io_UserChoice = (eMenuOptions)userNumber;
            //}
        }


        private void addVehicleToGarage()
        {

        }
        

        public enum eMenuOptions
        {
            AddNewVehicle = 1,
            ShowLicenseNumberByFilter,
            ChangeStatusOfVehicle,
            InfaltingAirInWheels,
            FillGasolineTank,
            ChargeBattery,
            ShowVehicleDetails,
            Exit
        }
    }
          
}

