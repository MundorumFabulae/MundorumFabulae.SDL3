using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Video;

namespace MundorumFabulae.SDL3.Native.Interop;

[CustomMarshaller(typeof(Window?), MarshalMode.Default, typeof(NullableWindowMarshaller))]
public static class NullableWindowMarshaller
{
	public static nint ConvertToUnmanaged(Window? managed)
		=> managed?.Handle ?? nint.Zero;

	public static Window? ConvertToManaged(nint unmanaged)
		=> unmanaged == nint.Zero ? null : new Window(unmanaged);
}
