using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Interop;

namespace MundorumFabulae.SDL3.Native;

public static partial class NativeMethods
{
	/// <summary>
	/// Get the version of SDL that is linked against your program.
	/// </summary>
	/// <returns>
	/// The version of the linked library.
	/// </returns>
	/// <remarks>
	/// <para>
	/// If you are linking to SDL dynamically, then it is possible that the current version will be different than the version you compiled against.
	/// This function returns the current version, while <c>SDL_VERSION</c> is the version you compiled with.
	/// </para>
	/// <para>
	/// This function may be called safely at any time, even before <see cref="Init"/>.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="GetRevision"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetVersion")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetVersion();

	/// <summary>
	/// Get the code revision of SDL that is linked against your program.
	/// </summary>
	/// <returns>
	/// An arbitrary string, uniquely identifying the exact revision of the SDL library in use.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This value is the revision of the code you are linking against and may be different from the code you are compiling with, which is found in
	/// the constant <c>SDL_REVISION</c>.
	/// </para>
	/// <para>
	/// The revision is an arbitrary string (a hash value) uniquely identifying the exact revision of the SDL library in use, and is only useful in
	/// comparing against other revisions. It is NOT an incrementing number.
	/// </para>
	/// <para>
	/// If SDL wasn't built from a git repository with the appropriate tools, this will return an empty string.
	/// </para>
	/// <para>
	/// You shouldn't use this function for anything but logging it for debugging purposes. The string is not intended to be reliable in any way.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="GetVersion"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetRevision")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(NonOwningUtf8StringMarshaller))]
	public static partial string GetRevision();
}
