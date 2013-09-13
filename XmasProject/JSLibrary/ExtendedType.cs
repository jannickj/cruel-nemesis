using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace JSLibrary
{
	public class ExtendedType
	{
		public static List<Type> FindAllDerivedTypes<T>()
		{
			return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof (T)));
		}

		public static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
		{
			Type derivedType = typeof (T);
			return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t)).ToList();
		}
	}
}