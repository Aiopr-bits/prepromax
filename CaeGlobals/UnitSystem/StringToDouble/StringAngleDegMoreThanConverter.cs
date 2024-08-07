﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Globalization;
using UnitsNet;
using UnitsNet.Units;

namespace CaeGlobals
{
    public class StringAngleDegMoreThanConverter : TypeConverter
    {
        // Variables                                                                                                                
        readonly static AngleUnit _angleUnit = AngleUnit.Degree;


        // Properties                                                                                                               
        public static string GetUnitAbbreviation()
        {
            return Angle.GetAbbreviation(_angleUnit);
        }


        // Constructors                                                                                                             
        public StringAngleDegMoreThanConverter()
        {
        }


        // Methods                                                                                                                  
        public override bool CanConvertFrom(ITypeDescriptorContext context, System.Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
            else return base.CanConvertFrom(context, sourceType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            // Convert from string
            if (value is string valueString) return MyNCalc.ConvertFromString(valueString.Replace(">", ""), ConvertToCurrentUnits);
            else return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            // Convert to string
            try
            {
                if (destinationType == typeof(string))
                {
                    if (value is double || value is float || value is int)
                    {
                        double valueDouble = Convert.ToDouble(value);
                        return "> " + valueDouble.ToString() + " " + GetUnitAbbreviation();
                    }
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }
            catch
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
        }
        //
        public static double ConvertToCurrentUnits(string valueWithUnitString)
        {
            try
            {
                Angle angle = Angle.Parse(valueWithUnitString).ToUnit(_angleUnit);
                return angle.Value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + Environment.NewLine + Environment.NewLine + SupportedUnitAbbreviations());
            }
        }
        public static string SupportedUnitAbbreviations()
        {
            return StringAngleConverter.SupportedUnitAbbreviations();
        }
    }
}