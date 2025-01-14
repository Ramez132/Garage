﻿using Ex03.GarageLogic;
using System.Collections.Generic;
using System.Linq;
using System;

public abstract class Vehicle
{
    private string m_ModelName;
    private string m_LicenseNumber;
    private List<Wheel> m_Wheels = new List<Wheel>();

    // Constructor for base class
    public Vehicle(string i_ModelName, string i_LicenseNumber)
    {
        ModelName = i_ModelName;
        LicenseNumber = i_LicenseNumber;
    }

    public List<Wheel> WheelsList
    {
        get { return m_Wheels; }
    }

    public string ModelName
    {
        get { return m_ModelName; }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                m_ModelName = value;
            }
            else
            {
                throw new FormatException("Invalid model name");
            }
        }
    }

    public string LicenseNumber
    {
        get { return m_LicenseNumber; }
        set
        {
            if (value.All(char.IsDigit))
            {
                m_LicenseNumber = value;
            }
            else
            {
                throw new FormatException("License number must contain only digits.");
            }
        }
    }

    public void AddWheel(Wheel i_Wheel)
    {
        m_Wheels.Add(i_Wheel);
    }
}
