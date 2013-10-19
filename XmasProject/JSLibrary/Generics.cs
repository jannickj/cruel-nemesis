using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JSLibrary
{
    public class Generics
    {
        /// <summary>
        /// Instantiates a generic class, such as GenericClass&lt;A,B,C&gt;
        /// </summary>
        /// <param name="generic">type of the generic class</param>
        /// <param name="subtypes">the subtypes such as the A, B and C</param>
        /// <param name="constructorInput">Input given to the constructor of the class</param>
        public static object InstantiateGenericClass(Type generic, Type[] subtypes,params Object[] constructorInput)
        {
            var gentype = generic.MakeGenericType(subtypes);
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            return Activator.CreateInstance(gentype, flags, null, constructorInput, null);
        }
    }
}
