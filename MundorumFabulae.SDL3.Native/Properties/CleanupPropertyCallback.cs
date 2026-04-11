namespace MundorumFabulae.SDL3.Native.Properties;

/// <summary>
/// A function that will be called when a property is deleted.
/// </summary>
/// <remarks>
/// Because this callback will be passed through the managed-unmanaged boundary, any functions implementing this callback <b>MUST NOT</b> throw
/// exceptions.
/// </remarks>
/// <param name="userdata">a pointer that is passed to the cleanup function.</param>
/// <param name="value">the value of the property being deleted.</param>
public delegate void CleanupPropertyCallback(nint userdata, nint value);
