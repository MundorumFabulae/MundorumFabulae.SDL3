using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Interop;
using MundorumFabulae.SDL3.Native.MessageBox;
using MundorumFabulae.SDL3.Native.Video;

namespace MundorumFabulae.SDL3.Native;

public static partial class NativeMethods
{
	[LibraryImport("SDL3", EntryPoint = "SDL_ShowSimpleMessageBox", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool ShowSimpleMessageBox(
		MessageBoxFlags flags,
		string title,
		string message,
		[MarshalUsing(typeof(NullableWindowMarshaller))] Window? parent
	);
}
