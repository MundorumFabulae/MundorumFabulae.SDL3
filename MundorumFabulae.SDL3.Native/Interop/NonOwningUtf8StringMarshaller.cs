using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace MundorumFabulae.SDL3.Native.Interop;

/// <summary>
/// A marshaller for UTF-8 strings which does not free the memory returned from native code.
/// </summary>
/// <remarks>
/// Many of the functions in SDL return UTF-8 strings whose ownership is not transferred to the caller. 
/// </remarks>
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(NonOwningUtf8StringMarshaller))]
internal static class NonOwningUtf8StringMarshaller
{
	public static string? ConvertToManaged(nint unmanaged)
		=> Marshal.PtrToStringUTF8(unmanaged);
}
