using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{

	/// <summary>
	/// Converter designed only for converting from a foreign type to a Xmas type
	/// </summary>
	/// <typeparam name="XmasType">The Xmas type that the foreign type is to be converted into</typeparam>
	/// <typeparam name="ForeignType">The foreign type that is to be converted into a Xmas type</typeparam>
	public abstract class XmasConverterToXmas<XmasType, ForeignType> : XmasConverter<XmasType, ForeignType>
		where XmasType : XmasObject
	{
		public override ForeignType BeginConversionToForeign(XmasType gobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}