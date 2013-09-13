using System;
using System.Collections.Generic;

namespace XmasEngineModel.Management.Interfaces
{
	public interface ITrigger
	{
		ICollection<Type> Events { get; }
	}
}