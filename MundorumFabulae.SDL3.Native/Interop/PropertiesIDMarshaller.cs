using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Properties;

namespace MundorumFabulae.SDL3.Native.Interop;

[CustomMarshaller(typeof(PropertiesID), MarshalMode.Default, typeof(PropertiesIDMarshaller))]
internal static class PropertiesIDMarshaller
{
	public static uint ConvertToUnmanaged(PropertiesID managed)
		=> managed.Id;

	public static PropertiesID ConvertToManaged(uint unmanaged)
		=> new PropertiesID(unmanaged);
}
