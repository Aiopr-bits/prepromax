﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrePoMax;
using CaeModel;
using CaeMesh;
using CaeGlobals;


namespace PrePoMax.Commands
{
    [Serializable]
    class CHideLoads : PreprocessCommand
    {
        // Variables                                                                                                                
        private string _stepName;
        private string[] _loadNames;


        // Constructor                                                                                                              
        public CHideLoads(string stepName, string[] loadNames)
            : base("Hide loads")
        {
            _stepName = stepName;
            _loadNames = loadNames;
        }


        // Methods                                                                                                                  
        public override bool Execute(Controller receiver)
        {
            receiver.HideLoads(_stepName, _loadNames);
            return true;
        }

        public override string GetCommandString()
        {
            return base.GetCommandString() + _stepName + ": " + GetArrayAsString(_loadNames);
        }
    }
}
