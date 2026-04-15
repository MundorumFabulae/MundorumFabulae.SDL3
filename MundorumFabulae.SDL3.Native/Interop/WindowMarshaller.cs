using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Video;

namespace MundorumFabulae.SDL3.Native.Interop;

[CustomMarshaller(typeof(Window), MarshalMode.Default, typeof(WindowMarshaller))]
public static class WindowMarshaller
{
	public static nint ConvertToUnmanaged(Window managed)
		=> managed.Handle;

	public static Window ConvertToManaged(nint unmanaged)
		=> new Window(unmanaged);
}
