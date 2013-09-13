using System;
using System.Collections.Generic;
using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	/// <summary>
	/// Basic conversion tool from objects to XmasObjects and vice versa
	/// </summary>
	public abstract class XmasConversionTool
	{
		internal abstract object ConvertToForeignUnsafe(XmasObject gobj);

		internal abstract XmasObject ConvertToXmasUnsafe(object fobj);
	}

	/// <summary>
	/// Conversion tool for objects of a certain type to XmasObject and vice versa
	/// </summary>
	/// <typeparam name="ForeignType"></typeparam>
	public class XmasConversionTool<ForeignType> : XmasConversionTool
	{
		private Dictionary<Type, XmasConverter> foreignLookup = new Dictionary<Type, XmasConverter>();
		private Dictionary<Type, XmasConverter> gooseLookup = new Dictionary<Type, XmasConverter>();

		/// <summary>
		/// Instantiates a XmasConversionTool, used to convert objects
		/// </summary>
		public XmasConversionTool()
		{
			NoConverter nocon = new NoConverter();
			gooseLookup.Add(typeof (object), nocon);
			foreignLookup.Add(typeof (object), nocon);
		}

		/// <summary>
		/// Adds a converter meant to be used by the converter for converting objects
		/// </summary>
		/// <typeparam name="XmasType">The Xmas type the converter will convert the foreign object to and from</typeparam>
		/// <typeparam name="ForeignTyped">The Foreign type the converter will convert the Xmas object to and from</typeparam>
		/// <param name="converter">The converter that is added to the tool</param>
		public virtual void AddConverter<XmasType, ForeignTyped>(XmasConverter<XmasType, ForeignTyped> converter)
			where ForeignTyped : ForeignType
			where XmasType : XmasObject
		{
			converter.ConversionTool = this;

			if (!(converter is XmasConverterToForeign<XmasType, ForeignTyped>))
				foreignLookup.Add(typeof (ForeignTyped), converter);
			if (!(converter is XmasConverterToXmas<XmasType, ForeignTyped>))
				gooseLookup.Add(typeof (XmasType), converter);
		}

		/// <summary>
		/// Converts the XmasObject into the an object with the tool's Foreign type
		/// </summary>
		/// <param name="gobj">XmasObject to be converted</param>
		/// <exception cref="UnconvertableException">Is thrown if conversion was not possible</exception>
		/// <returns>The object that the XmasObject is converted into</returns>
		
		public ForeignType ConvertToForeign(XmasObject gobj)
		{
			Type original = gobj.GetType();
			Type gt = original;
			XmasConverter converter;
			try
			{
				while (true)
				{
					if (gooseLookup.TryGetValue(gt, out converter))
					{
						if (gt != original)
						{
							SleekConverter sleek = new SleekConverter(converter.BeginUnsafeConversionToForeign,
																	  converter.BeginUnsafeConversionToXmas);
							gooseLookup.Add(original, sleek);
						}
						return (ForeignType)converter.BeginUnsafeConversionToForeign(gobj);
					}
					else
						gt = gt.BaseType;
				}
			}
			catch (Exception inner)
			{
				throw new UnconvertableException(gobj, inner);
			}
			
		}


		internal override object ConvertToForeignUnsafe(XmasObject gobj)
		{
			return ConvertToForeign(gobj);
		}

		internal override XmasObject ConvertToXmasUnsafe(object fobj)
		{
			return ConvertToXmas((ForeignType) fobj);
		}

		/// <summary>
		/// Converts the Foreign object into an XmasObject
		/// </summary>
		/// <param name="foreign">The foreign object to be converted</param>
		/// <exception cref="UnconvertableException">Is thrown if conversion was not possible</exception>
		/// <returns>The XmasObject the foreign object is converted into</returns>
		public XmasObject ConvertToXmas(ForeignType foreign)
		{
			XmasConverter converter;
			try
			{
				Type ft = foreign.GetType();
				if (foreignLookup.TryGetValue(ft, out converter))
				{
					return converter.BeginUnsafeConversionToXmas(foreign);
				}
				throw new KeyNotFoundException("Converter not found for "+foreign.GetType().Name);
			}
			catch (Exception inner)
			{
				throw new UnconvertableException(foreign, inner);
				
			}
			
		}


		private class NoConverter : XmasConverter
		{
			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				throw new UnconvertableException(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToXmas(object obj)
			{
				throw new UnconvertableException(obj);
			}
		}

		private class SleekConverter : XmasConverter
		{
			private Func<XmasObject, object> toForeign;
			private Func<object, XmasObject> toXmas;

			public SleekConverter(Func<XmasObject, object> toForeign, Func<object, XmasObject> toXmas)
			{
				this.toForeign = toForeign;
				this.toXmas = toXmas;
			}

			internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
			{
				return toForeign(gobj);
			}

			internal override XmasObject BeginUnsafeConversionToXmas(object obj)
			{
				return toXmas(obj);
			}
		}
	}
}