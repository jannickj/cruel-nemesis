namespace XmasEngineModel.Conversion
{
	public abstract class XmasConverter
	{
		internal abstract object BeginUnsafeConversionToForeign(XmasObject gobj);
		internal abstract XmasObject BeginUnsafeConversionToXmas(object obj);
	}

	/// <summary>
	/// An abstract converter meant to be implemented into actual converters
	/// </summary>
	/// <typeparam name="XmasType">The XmasObject tyoe that the converter will convert foreign objects to and from</typeparam>
	/// <typeparam name="ForeignType">The foreign object type that the converter will convert XmasObjects to and from</typeparam>
	public abstract class XmasConverter<XmasType, ForeignType> : XmasConverter where XmasType : XmasObject
	{
		private XmasConversionTool conversionTool;

		internal XmasConversionTool ConversionTool
		{
			private get { return conversionTool; }
			set { conversionTool = value; }
		}


		/// <summary>
		/// This method is called when the converter is asked by the converter tool to convert a foreign object into a XmasObject 
		/// </summary>
		/// <param name="fobj">The foreign object to be converted</param>
		/// <returns>The Xmas object that the foreign object was converted into</returns>
		public abstract XmasType BeginConversionToXmas(ForeignType fobj);

		/// <summary>
		/// This method is called when the converter is asked by the converter tool to convert a Xmas object into a foreign object  
		/// </summary>
		/// <param name="gobj">The Xmas object to be converted</param>
		/// <returns>The foreign object that the Xmas object was converted into</returns>
		public abstract ForeignType BeginConversionToForeign(XmasType gobj);


		/// <summary>
		/// Requests the conversion of an XmasObject into an object, only if the ConversionTool the converter is added to is this possible.
		/// </summary>
		/// <exception cref="UnconvertableException">Is thrown if conversion was not possible</exception>
		/// <param name="gobj">The XmasObject to be converted</param>
		/// <returns>The object the XmasObject was converted into</returns>
		protected object ConvertToForeign(XmasObject gobj)
		{
			return conversionTool.ConvertToForeignUnsafe(gobj);
		}

		/// <summary>
		/// Requests the conversion of an object into an XmasObject, only if the ConversionTool the converter is added to is this possible.
		/// </summary>
		/// <exception cref="UnconvertableException">Is thrown if conversion was not possible</exception>
		/// <param name="fobj">The object to be converted</param>
		/// <returns>The XmasObject the object was converted into</returns>
		protected XmasObject ConvertToXmas(ForeignType fobj)
		{
			return conversionTool.ConvertToXmasUnsafe(fobj);
		}

		internal override object BeginUnsafeConversionToForeign(XmasObject gobj)
		{
			return BeginConversionToForeign((XmasType) gobj);
		}

		internal override XmasObject BeginUnsafeConversionToXmas(object obj)
		{
			return BeginConversionToXmas((ForeignType) obj);
		}
	}
}