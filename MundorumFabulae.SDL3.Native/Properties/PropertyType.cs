namespace MundorumFabulae.SDL3.Native.Properties;

#pragma warning disable CA1720 // Identifier contains type name
/// <summary>
/// The type of a property in a group of properties.
/// </summary>
public enum PropertyType
{
	/// <summary>
	/// The property does not exist.
	/// </summary>
	Invalid,

	/// <summary>
	/// The property is a pointer.
	/// </summary>
	Pointer,

	/// <summary>
	/// The property is a string.
	/// </summary>
	String,

	/// <summary>
	/// The property is a signed 64-bit integer.
	/// </summary>
	Number,

	/// <summary>
	/// The property is a floating point number.
	/// </summary>
	Float,

	/// <summary>
	/// The property is a boolean.
	/// </summary>
	Boolean,
}
#pragma warning restore CA1720
