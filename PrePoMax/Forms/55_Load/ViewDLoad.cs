﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CaeMesh;
using DynamicTypeDescriptor;
using System.ComponentModel;
using CaeGlobals;
using CaeModel;

namespace PrePoMax
{
    [Serializable]
    public class ViewDLoad : ViewLoad
    {
        // Variables                                                                                                                
        private DLoad _dLoad;


        // Properties                                                                                                               
        public override string Name { get { return _dLoad.Name; } set { _dLoad.Name = value; } }
        //
        [CategoryAttribute("Region")]
        [OrderedDisplayName(2, 10, "Surface")]
        [DescriptionAttribute("Select the surface for the creation of the load.")]
        [Id(3, 2)]
        public string SurfaceName { get { return _dLoad.SurfaceName; } set {_dLoad.SurfaceName = value;} }
        //
        [CategoryAttribute("Pressure magnitude")]
        [OrderedDisplayName(0, 10, "Magnitude")]
        [DescriptionAttribute("Value of the pressure load magnitude.")]
        [TypeConverter(typeof(EquationPressureConverter))]
        [Id(1, 3)]
        public EquationString Magnitude { get { return _dLoad.Magnitude.Equation; } set { _dLoad.Magnitude.Equation = value; } }
        //
        [CategoryAttribute("Pressure phase")]
        [OrderedDisplayName(0, 10, "Phase")]
        [DescriptionAttribute("Value of the pressure phase.")]
        [TypeConverter(typeof(EquationAngleDegConverter))]
        [Id(1, 4)]
        public EquationString Phase { get { return _dLoad.PhaseDeg.Equation; } set { _dLoad.PhaseDeg.Equation = value; } }
        //
        public override string AmplitudeName { get { return _dLoad.AmplitudeName; } set { _dLoad.AmplitudeName = value; } }
        [Browsable(false)]
        public override string CoordinateSystemName
        {
            get { return _dLoad.CoordinateSystemName; }
            set { _dLoad.CoordinateSystemName = value; }
        }
        public override System.Drawing.Color Color { get { return _dLoad.Color; } set { _dLoad.Color = value; } }


        // Constructors                                                                                                             
        public ViewDLoad(DLoad dLoad)
        {
            _dLoad = dLoad;
            //
            Dictionary<RegionTypeEnum, string> regionTypePropertyNamePairs = new Dictionary<RegionTypeEnum, string>();
            regionTypePropertyNamePairs.Add(RegionTypeEnum.Selection, nameof(SelectionHidden));
            regionTypePropertyNamePairs.Add(RegionTypeEnum.SurfaceName, nameof(SurfaceName));
            //
            SetBase(_dLoad, regionTypePropertyNamePairs);
            DynamicCustomTypeDescriptor = ProviderInstaller.Install(this);
            // Phase
            DynamicCustomTypeDescriptor.GetProperty(nameof(Phase)).SetIsBrowsable(_dLoad.Complex);
        }


        // Methods                                                                                                                  
        public override Load GetBase()
        {
            return _dLoad;
        }
        public void PopulateDropDownLists(string[] surfaceNames, string[] amplitudeNames)
        {
            Dictionary<RegionTypeEnum, string[]> regionTypeListItemsPairs = new Dictionary<RegionTypeEnum, string[]>();
            regionTypeListItemsPairs.Add(RegionTypeEnum.Selection, new string[] { "Hidden" });
            regionTypeListItemsPairs.Add(RegionTypeEnum.SurfaceName, surfaceNames);
            PopulateDropDownLists(regionTypeListItemsPairs);
            //
            PopulateAmplitudeNames(amplitudeNames);
        }
    }
}
