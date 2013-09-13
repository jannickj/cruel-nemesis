using System;
using System.Collections.Generic;
using JSLibrary.IiLang.DataContainers;
using XmasEngineExtensions.EisExtension.Model.ActionTypes;

namespace XmasEngineExtensions.EisExtension.Model
{
	public class IILActionParser
	{
		private Dictionary<string, Type> convert = new Dictionary<string, Type>();

		public IILActionParser()
		{
			Add<EISGetAllPercepts>("getAllPercepts");
		}

		public void Add<EISActionType>(string actionName) where EISActionType : EISAction
		{
			convert.Add(actionName, typeof (EISActionType));
		}

		public EISAction parseIILAction(IilAction action)
		{
			EISAction retval;
			Type actiontype;

			if (convert.TryGetValue(action.Name, out actiontype))
			{
				retval = (EISAction) Activator.CreateInstance(actiontype);
				retval.TransferFrom(action);
				return retval;
			}
			else
				throw new Exception("Unable to parse IIL action named: " + action.Name);
		}
	}
}