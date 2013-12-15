using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.UnityLogic.Exceptions
{
	public class GraphicsNotFoundException : Exception
	{
        public Type ModelType { get; private set; }
        public GraphicsNotFoundException(Type modeltype)
            : base("The model type: " + modeltype.Name + ", does not currently have a graphics associated with it")
        {
            this.ModelType = modeltype;
        }
	}
}
