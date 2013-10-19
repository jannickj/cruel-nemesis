using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmasEngineModel.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class VirtualNotImplementedAttribute : Attribute
    {
        public string Comment { get; private set; }

        public VirtualNotImplementedAttribute(string comment)
        {
            this.Comment = comment;
            
        }

    }
}
