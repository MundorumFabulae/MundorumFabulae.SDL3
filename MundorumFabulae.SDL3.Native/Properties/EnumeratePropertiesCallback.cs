namespace MundorumFabulae.SDL3.Native.Properties;

/// <summary>
/// A function that will be called for each property in a group of properties.
/// </summary>
/// <remarks>
/// Because this callback will be passed through the managed-unmanaged boundary, any functions implementing this callback <b>MUST NOT</b> throw
/// exceptions.
/// </remarks>
/// <param name="userdata">a pointer that is passed to the callback function.</param>
/// <param name="props">the group of properties being enumerated.</param>
/// <param name="name">the name of the property being enumerated.</param>
public delegate void EnumeratePropertiesCallback(nint userdata, PropertiesID props, string name);
