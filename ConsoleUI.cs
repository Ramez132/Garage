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
            Console.WriteLine("Enter vehicle license number:");
            string lisenceNumber = Console.ReadLine();
            checkLisenceInput(lisenceNumber);

            if (m_Garage.Vehicles.ContainsKey(lisenceNumber))
            {
                Console.WriteLine("Vehicle is already in garage ! Status changed to 'Bring Rapaired'");
                m_Garage.Vehicles[lisenceNumber].VehicleCondition = VehicleInGarage.eVehicleCondition.InRepair;
                return;
            }

            //Console.WriteLine("Select the type of vehicle:");
            //Console.WriteLine("1. Motorcycle\n2. Car\n3. Truck");

            string vehicleTypeInput = "";
            string model = "";
            eVehicleType vehicleType = 0;

            Console.WriteLine("Select the type of vehicle:");
            Console.WriteLine("1. Motorcycle\n2. Car\n3. Truck");
            vehicleTypeInput = Console.ReadLine();
            isVehicleTypeInputValid(vehicleTypeInput, vehicleType);

            Console.WriteLine("Please type car modol");
            model = Console.ReadLine();
            checkModelInput(model);

            Vehicle newVehicle = Factory.CreateVehicle(vehicleType, model, lisenceNumber);

            Console.WriteLine("Enter name and phone number of the vehicle owner (Name should onley contain english letters, " +
                "phone number should onley contain numbers):");
            string nameOfOwner = Console.ReadLine();
            isNameOfOwnerInputValid(nameOfOwner);
            string phoneNumberOfOwner = Console.ReadLine();
            isPhoneOfOwnerInputValid(phoneNumberOfOwner);

            Console.WriteLine("Please insert the wheel air preasure:\n1) All together\n2)One by one");
            string wheelAirPreasureInput = Console.ReadLine();
            wheelPreasureOptionCheck(wheelAirPreasureInput);
            switch (int.Parse(wheelAirPreasureInput))
            {
                case 1:
                    {
                        applyWheelPreasureTogether(newVehicle, vehicleType);
                        break;
                    }

                case 2:
                    {
                        applyWheelPreasureOneByOne(newVehicle, vehicleType);
                        break;
                    }
            }

            Console.WriteLine("\nNow the energy source of the vehicle");
            initializeEnertgySource(newVehicle, vehicleType);

            switch (vehicleType)
            {
                case eVehicleType.Car:
                    {
                        selectColor((Car)newVehicle);
                        selectNumberOfDoors((Car)newVehicle);
                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        selectEngineSize((Motorcycle)newVehicle);
                        selectLicenseLevel((Motorcycle)newVehicle);
                        break;
                    }

                case eVehicleType.Truck:
                    {
                        isTransportRefrifiratedItems((Truck)newVehicle);
                        selectCargoVolume((Truck)newVehicle);
                        break;
                    }

            }

            VehicleInGarage addedVehicle = new VehicleInGarage();
            addedVehicle.GarageVehicle = newVehicle;
            addedVehicle.OwnerName = nameOfOwner;
            addedVehicle.OwnerNumber = phoneNumberOfOwner;
            m_Garage.AddNewVehicleToGarage(lisenceNumber, addedVehicle);


        }
        
        private void checkModelInput(string i_Model)
        {
            while (String.IsNullOrEmpty(i_Model))
            {
                Console.WriteLine("Invalid input! you entered an empty input");
                i_Model = Console.ReadLine();
            }
        }
        private void checkLisenceInput(string i_LicenseNumber)
        {
            while (String.IsNullOrEmpty(i_LicenseNumber))
            {
                Console.WriteLine("Invalid input! you entered an empty input");
                i_LicenseNumber = Console.ReadLine();
            }
        }

        private void isVehicleTypeInputValid(string i_VehicleTypeChoice, eVehicleType io_VehicleType)
        {
            int VehicleTypeChoiceParsed;
            string vehicleTypeChoice = Console.ReadLine();
            while (!int.TryParse(vehicleTypeChoice, out VehicleTypeChoiceParsed) || !(int.Parse(vehicleTypeChoice) >= 1 && int.Parse(vehicleTypeChoice) <= 3))
            {
                Console.WriteLine("Invalid vehicle type selected !");
                i_VehicleTypeChoice = Console.ReadLine();   
            }

            io_VehicleType = (eVehicleType)VehicleTypeChoiceParsed;
            
        }

        private void isNameOfOwnerInputValid(string i_Name)
        {
            while (String.IsNullOrEmpty(i_Name) || i_Name.All(Char.IsLetter) == false)
            {
                Console.WriteLine("Invalid input! Name should only contain english letters");
                i_Name = Console.ReadLine();
            }
        }

        private void isPhoneOfOwnerInputValid(string i_Phone)
        {
            if (String.IsNullOrEmpty(i_Phone) || i_Phone.All(Char.IsDigit) == false)
            {
                Console.WriteLine("Invalid input! Phone should only contain numbers");
                i_Phone = Console.ReadLine();
            }
        }

       

        private void wheelPreasureOptionCheck(string i_wheelPreasureInput)
        {
            int wheelPreasueChoiceParsed;
            while (!int.TryParse(i_wheelPreasureInput, out wheelPreasueChoiceParsed) || (int.Parse(i_wheelPreasureInput) != 1 && int.Parse(i_wheelPreasureInput) != 2))
            {
                Console.WriteLine("Inavid Input! Choose 1 for fuel or 2 for battery");
                i_wheelPreasureInput = Console.ReadLine();
            }
        }

        private void applyWheelPreasureOneByOne(Vehicle i_NewVehicle, eVehicleType i_VehicleType)
        {
            for (int i = 0; i < i_NewVehicle.WheelsList.Count; i++)
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter wheel producer:");
                        string producer = Console.ReadLine();

                        //Console.WriteLine("Enter the maximum air preasure");
                        //float maxAirPreasue = float.Parse(Console.ReadLine());
                        float maxAirPreasue = 0;
                        switch (i_VehicleType)
                        {
                            case eVehicleType.Car:
                                {
                                    maxAirPreasue = 34;
                                    break;
                                }

                            case eVehicleType.Motorcycle:
                                {
                                    maxAirPreasue = 32;
                                    break;
                                }

                            case eVehicleType.Truck:
                                {
                                    maxAirPreasue = 29;
                                    break;
                                }
                        }

                        Console.WriteLine("Enter the current air preasure");
                        float currentAirPreasue = float.Parse(Console.ReadLine());
                        i_NewVehicle.AddWheel(new Wheel(producer, maxAirPreasue, currentAirPreasue));
                        break;
                    }

                    catch (FormatException e)
                    {
                        Console.WriteLine($"Invlalid input - {e.Message}");
                    }

                    catch (ValueOutOfRangeException e)
                    {
                        Console.WriteLine($"Invalid input - {e.Message}");

                    }
                }
            }
        }
        private void applyWheelPreasureTogether(Vehicle i_NewVehicle, eVehicleType i_VehicleType)
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter wheel producer:");
                    string producer = Console.ReadLine();

                    //Console.WriteLine("Enter the maximum air preasure");
                    //float maxAirPreasue = float.Parse(Console.ReadLine());
                    float maxAirPreasue = 0;
                    switch (i_VehicleType)
                    {
                        case eVehicleType.Car:
                            {
                                maxAirPreasue = 34;
                                break;
                            }

                        case eVehicleType.Motorcycle:
                            {
                                maxAirPreasue = 32;
                                break;
                            }

                        case eVehicleType.Truck:
                            {
                                maxAirPreasue = 29;
                                break;
                            }
                    }
                    Console.WriteLine("Enter the current air preasure");
                    float currentAirPreasue = float.Parse(Console.ReadLine());

                    for (int i = 0; i < i_NewVehicle.WheelsList.Count; i++) 
                    {
                        i_NewVehicle.AddWheel(new Wheel(producer, maxAirPreasue, currentAirPreasue));
                    }

                    break;
                }

                catch (FormatException e)
                {
                    Console.WriteLine($"Invlalid input - {e.Message}");
                }

                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine($"Invalid input - {e.Message}");
                
                }
            }
        }

        private void initializeEnertgySource(Vehicle i_newVehicle, eVehicleType i_VehicleType)
        {
            if (i_VehicleType == eVehicleType.Car || i_VehicleType == eVehicleType.Motorcycle)
            {
                Console.WriteLine("Choose the type of energy source to create:\n");
                Console.WriteLine("1. Electric Battery\n2. Fuel Engine");
                string energyChoice = "";
                switch (i_VehicleType)
                {

                    case eVehicleType.Car:
                        {

                            //Console.WriteLine("Choose the type of energy source to create:\n");
                            //Console.WriteLine("1. Electric Battery\n2. Fuel Engine");
                            //string energyChoice = "";
                            checkEnergySourceInput(energyChoice);
                            switch (int.Parse(energyChoice))
                            {
                                case 1:
                                    {
                                        EnergySource battery = createElectricBattery(i_VehicleType);
                                        break;
                                    }

                                case 2:
                                    {
                                        EnergySource engine = createFuelEngine(i_VehicleType);
                                        break;
                                    }
                            }

                            break;
                        }

                    case eVehicleType.Motorcycle:
                        {
                            checkEnergySourceInput(energyChoice);
                            switch (int.Parse(energyChoice))
                            {
                                case 1:
                                    {
                                        EnergySource battery = createElectricBattery(i_VehicleType);
                                        break;
                                    }

                                case 2:
                                    {
                                        EnergySource engine = createFuelEngine(i_VehicleType);
                                        break;
                                    }
                            }
                            break;
                        }

                   
                }
            }

            else // Then it's a truck
            {
                EnergySource engine = createFuelEngine(i_VehicleType);
            }
        }

        private EnergySource createFuelEngine(eVehicleType i_VehicleType)
        {
            FuelEngine o_FuelEngine = null;
            float engineCapacity = 0;
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    {
                        engineCapacity = 52;
                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        engineCapacity = 2.9f;
                        break;
                    }

                    case eVehicleType.Truck:
                    {
                        engineCapacity = 125;
                        break;
                    }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter engine percentage left (between 0 to 100):");
                    float energyLeft = float.Parse(Console.ReadLine());

                    //Console.WriteLine("Enter the engine capacity time in hours (2.9 or 5.4):");
                    //float engineLongevity = float.Parse(Console.ReadLine());

                    Console.WriteLine($"Enter the fuel left in engine in hours (from 0 to {engineCapacity}");
                    float timeLeftInFuel = float.Parse(Console.ReadLine());

                    Console.WriteLine("Enter the fuel type:\n1 - for Octan95\n2 - Octan96\n3 - Octan98\n4 - Soler");
                    int fuelTypeChoice = int.Parse(Console.ReadLine());
                    FuelEngine.eFuelTypes fuelType = (FuelEngine.eFuelTypes)fuelTypeChoice;

                    o_FuelEngine = new FuelEngine(energyLeft);
                }

                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine($"Invalid input {e.Message}");
                }

                catch (ArgumentException e)
                {
                    Console.WriteLine($"Invalid input: {e.Message}");
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input - enter only numbers");
                }
            }

            return o_FuelEngine;
        }
        private EnergySource createElectricBattery(eVehicleType i_VehicleType)
        {
            ElectricBattery o_Battery = null;
            float batteryLongevity = 0;
            switch (i_VehicleType)
            {
                case eVehicleType.Car:
                    {
                        batteryLongevity = 5.4f;
                        break;
                    }

                case eVehicleType.Motorcycle:
                    {
                        batteryLongevity = 2.9f;
                        break;
                    }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter battery percentage left (between 0 to 100):");
                    float energyLeft = float.Parse(Console.ReadLine());

                    //Console.WriteLine("Enter the battery longevity time in hours (2.9 or 5.4):");
                    //float batteryLongevity = float.Parse(Console.ReadLine());

                    Console.WriteLine($"Enter the time left in battery in hours (from 0 to {batteryLongevity}");
                    float timeLeftInBattery = float.Parse(Console.ReadLine());

                    o_Battery = new ElectricBattery(energyLeft)
                    {
                        MaxBatteryTimeInHours = timeLeftInBattery,
                        BatteryTimeLeftInHours = energyLeft
                    };
                }

                catch (ValueOutOfRangeException e)
                {
                    Console.WriteLine($"Invalid input {e.Message}");
                }

                catch (FormatException)
                {
                    Console.WriteLine("Invalid input - enter only numbers");
                }
            }

            return o_Battery; 
        }

        private void checkEnergySourceInput(string i_EnergySource)
        {
            int energyChoiceParsed;
            while (!int.TryParse(i_EnergySource, out energyChoiceParsed) || (int.Parse(i_EnergySource) != 1 && int.Parse(i_EnergySource) != 2))
            {
                Console.WriteLine("Inavid Input! Choose 1 for fuel or 2 for battery");
                i_EnergySource = Console.ReadLine();
            }


        }

        private void selectColor(Car i_newVehicle)
        {
            while (true)
            {
                Console.WriteLine("Select car color:\n1) Blue\b2) Black\n3) White\n4) Gray");
                int colorSelection = int.Parse(Console.ReadLine());
                if (colorSelection >= 1 && colorSelection <= 4)
                {
                    i_newVehicle.Color = (Car.eColors)colorSelection;
                    break;
                }

                Console.WriteLine("Invalid input! Select a number from 1 to 4 depending on the color you want");
                colorSelection = int.Parse(Console.ReadLine());
            }
        }

        private void selectNumberOfDoors(Car i_newVehicle)
        {
            while (true)
            {
                Console.WriteLine("Select car color:\n1) 2 Doors\b2) 3 Doors\n3) 4 Doors\n4) 5 Doors");
                int doorsAmountSelection = int.Parse(Console.ReadLine());
                if (doorsAmountSelection >= 1 && doorsAmountSelection <= 4)
                {
                    i_newVehicle.Doors = (Car.eDoors)doorsAmountSelection;
                    return;
                }

                Console.WriteLine("Invalid input! Select a number from 1 to 4 depending on the color you want");
                doorsAmountSelection = int.Parse(Console.ReadLine());
            }
        }

        private void selectLicenseLevel(Motorcycle i_newVehicle)
        {
            while (true)
            {
                int choice;
                Console.WriteLine("Select license level of motorcycle:\n1) A1\n2) A2\n3) A3\n4) A4");
                string levelSelection = Console.ReadLine();
                if (int.TryParse(levelSelection, out choice) && choice >= 1 && choice <= 4)
                {
                    i_newVehicle.LicenseType = (Motorcycle.eLicenseType)choice;
                    return;
                }

                Console.WriteLine("Invalid input! Select a number between 1 and 4 depending on the license level you want");
                levelSelection = Console.ReadLine();
            }
        }

        private void selectEngineSize(Motorcycle i_newVehicle)
        {
            while (true)
            {
                int size;
                Console.WriteLine("Select engine size of motorcycle");
                string engineSizeSelection = Console.ReadLine();
                if (int.TryParse(engineSizeSelection, out size))
                {
                    i_newVehicle.EngineCapacity = size;
                    return;
                }

                Console.WriteLine("Invalid input! Input should be a number a number");
                engineSizeSelection = Console.ReadLine();
            }
        }

        private void isTransportRefrifiratedItems(Truck i_newVehicle)
        {
            while (true)
            {
                int parsedChoice;
                Console.WriteLine("Is truck able to transport refrigirated items?\n1) No\n2) Yes");
                string choice = Console.ReadLine();
                if (int.TryParse(choice, out parsedChoice) && (parsedChoice == 1 || parsedChoice == 2))
                {
                    if (parsedChoice == 1)
                    {
                        i_newVehicle.TransportsRefrigeratedMaterials = false;
                    }

                    else
                    {
                        i_newVehicle.TransportsRefrigeratedMaterials = true;
                    }

                    return;
                }

                Console.WriteLine("Invalid input! Select 1 for no, or 2 for yes");
                choice = Console.ReadLine();
            }
        }

        private void selectCargoVolume(Truck i_newVehicle)
        {
            while (true)
            {
                float cargoVolume;
                Console.WriteLine("Select truck cargo colume");
                string volumeChoice = Console.ReadLine();
                if (float.TryParse(volumeChoice, out cargoVolume))
                {
                    i_newVehicle.CargoVolume = cargoVolume;
                    return;
                }

                Console.WriteLine("Invalid input! Input should be a number a number");
                volumeChoice = Console.ReadLine();
            }
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

