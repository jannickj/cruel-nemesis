using XmasEngineModel.Exceptions;

namespace XmasEngineModel.Conversion
{
	/// <summary>
	/// Converter designed only for converting from a Xmas type to a foreign type
	/// </summary>
	/// <typeparam name="XmasType">The Xmas type that is to be converted into a foreign type</typeparam>
	/// <typeparam name="ForeignType">The foreign type that the Xmas type is to be converted into</typeparam>
	public abstract class XmasConverterToForeign<XmasType, ForeignType> : XmasConverter<XmasType, ForeignType>
		where XmasType : XmasObject
	{
		public override XmasType BeginConversionToXmas(ForeignType fobj)
		{
			throw new UnableToConvertException(this);
		}
	}
}