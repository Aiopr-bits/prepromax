﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CaeGlobals
{
    [Serializable]
    public class SelectionNodeMouse : SelectionNode, ISerializable
    {
        // Variables                                                                                                                
        private double[] _pickedPoint;                                  //ISerializable
        private double[] _selectionDirection;                           //ISerializable
        private double[][] _planeParameters; // Ox,Oy,Oz,Nx,Ny,Nz       //ISerializable
        private bool _completelyInside;                                 //ISerializable
        private vtkSelectBy _selectBy;                                  //ISerializable
        private double _angle;                                          //ISerializable
        private int[] _partIds;                                         //ISerializable
        private double[][] _partOffsets;                                //ISerializable
        private double _precision;                                      //ISerializable


        // Properties                                                                                                               
        public double[] PickedPoint { get { return _pickedPoint; } set { _pickedPoint = value; } }
        public double[] SelectionDirection { get { return _selectionDirection; } set { _selectionDirection = value; } }
        public double[][] PlaneParameters { get { return _planeParameters; } set { _planeParameters = value; } }
        public bool CompletelyInside { get { return _completelyInside; } }
        public vtkSelectBy SelectBy { get { return _selectBy; } set { _selectBy = value; } }
        public double Angle { get { return _angle; } set { _angle = value; } }        
        public int[] PartIds { get { return _partIds; } }
        public double[][] PartOffsets { get { return _partOffsets; } }
        public double Precision { get { return _precision; } set { _precision = value; } }
        public bool IsGeometryBased { get { return _selectBy.IsGeometryBased(); } }


        // Constructors                                                                                                             
        public SelectionNodeMouse(double[] pickedPoint, double[] selectionDirection,
                                  double[][] planeParameters, bool completelyInside,
                                  vtkSelectOperation selectOperation, int[] partIds, double[][] partOffsets, 
                                  vtkSelectBy selectBy, double angle)
            : base(selectOperation)
        {
            _pickedPoint = pickedPoint;
            _selectionDirection = selectionDirection;
            _planeParameters = planeParameters;
            _completelyInside = completelyInside;
            _selectBy = selectBy;
            _angle = angle;
            _partIds = partIds;
            _partOffsets = partOffsets;
            _precision = -1;
        }
        public SelectionNodeMouse(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _partIds = null;            // Compatibility for version v0.9.0
            _precision = -1;            // Compatibility for version v0.5.2
            _selectionDirection = null; // Compatibility for version v0.9.0
            _partOffsets = null;        // Compatibility for version v0.9.0
            _completelyInside = false;  // Compatibility for version v0.9.0
            //
            foreach (SerializationEntry entry in info)
            {
                switch (entry.Name)
                {
                    case "_pickedPoint":
                        _pickedPoint = (double[])entry.Value; break;
                    case "_selectionDirection":
                        _selectionDirection = (double[])entry.Value; break;
                    case "_planeParameters":
                        _planeParameters = (double[][])entry.Value; break;
                    case "_completelyInside":
                        _completelyInside = (bool)entry.Value; break;
                    case "_selectBy":
                        _selectBy = (vtkSelectBy)entry.Value; break;
                    case "_angle":
                        _angle = (double)entry.Value; break;
                    case "_partIds":
                        _partIds = (int[])entry.Value; break;
                    case "_partOffsets":
                        _partOffsets = (double[][])entry.Value; break;
                    case "_precision":
                        _precision = (double)entry.Value; break;
                    default:
                        break;
                }
            }
        }


        // Methods                                                                                                                  
        public void SetSinglePartSelection(int partId, double[] partOffset)
        {
            _partIds = new int[] { partId };
            _partOffsets = partOffset == null ? null : new double[][] { partOffset };
        }
        public void AddOffset(double[] offset)
        {
            if (offset == null) return;
            //
            if (_pickedPoint != null)
            {
                _pickedPoint[0] += offset[0];
                _pickedPoint[1] += offset[1];
                _pickedPoint[2] += offset[2];
            }
            if (_planeParameters != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    _planeParameters[i][0] += offset[0];
                    _planeParameters[i][1] += offset[1];
                    _planeParameters[i][2] += offset[2];
                }
            }
        }
        public void RemoveOffset(double[] offset)
        {
            if (offset == null) return;
            //
            if (_pickedPoint != null)
            {
                _pickedPoint[0] -= offset[0];
                _pickedPoint[1] -= offset[1];
                _pickedPoint[2] -= offset[2];
            }
            if (_planeParameters != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    _planeParameters[i][0] -= offset[0];
                    _planeParameters[i][1] -= offset[1];
                    _planeParameters[i][2] -= offset[2];
                }
            }
        }
        // ISerialization
        public new void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            // Using typeof() works also for null fields
            info.AddValue("_pickedPoint", _pickedPoint, typeof(double[]));
            info.AddValue("_selectionDirection", _selectionDirection, typeof(double[]));
            info.AddValue("_planeParameters", _planeParameters, typeof(double[][]));
            info.AddValue("_completelyInside", _completelyInside, typeof(bool));
            info.AddValue("_selectBy", _selectBy, typeof(vtkSelectBy));
            info.AddValue("_angle", _angle, typeof(double));
            info.AddValue("_partIds", _partIds, typeof(int[]));
            info.AddValue("_partOffsets", _partOffsets, typeof(double[][]));
            info.AddValue("_precision", _precision, typeof(double));
        }


    }
}
