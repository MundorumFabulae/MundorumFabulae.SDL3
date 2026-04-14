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

#pragma warning disable CA1707
	/// <summary>
	/// The current major version of SDL headers.
	/// </summary>
	/// <remarks>
	/// <para>
	/// If this were SDL version 3.2.1, this value would be 3.
	/// </para>
	/// <para>
	/// For this library, this refers to the major version of SDL for which the bindings were written.
	/// </para>
	/// </remarks>
	public const int MAJOR_VERSION = 3;

	/// <summary>
	/// The current minor version of SDL headers.
	/// </summary>
	/// <remarks>
	/// <para>
	/// If this were SDL version 3.2.1, this value would be 2.
	/// </para>
	/// <para>
	/// For this library, this refers to the minor version of SDL for which the bindings were written.
	/// </para>
	/// </remarks>
	public const int MINOR_VERSION = 4;

	/// <summary>
	/// The current micro version of SDL headers.
	/// </summary>
	/// <remarks>
	/// <para>
	/// If this were SDL version 3.2.1, this value would be 1.
	/// </para>
	/// <para>
	/// For this library, this refers to the micro version of SDL for which the bindings were written.
	/// </para>
	/// </remarks>
	public const int MICRO_VERSION = 0;

	/// <summary>
	/// This macro is a string describing the source at a particular point in development.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This string is often generated from revision control's state at build time.
	/// </para>
	/// <para>
	/// This string can be quite complex and does not follow any standard. For example, it might be something like
	/// "SDL-prerelease-3.1.1-47-gf687e0732". It might also be user-defined at build time, so it's best to treat it as a
	/// clue in debugging forensics and not something the app will parse in any way.
	/// </para>
	/// <para>
	/// This library will always return an empty string for this value instead of using the value present when using
	/// SDL 3 from C.
	/// </para>
	/// </remarks>
	public const string REVISION = "";

	/// <summary>
	/// This is the version number macro for the current SDL version.
	/// </summary>
	/// <remarks>
	/// <para>
	/// For this library, this is the version of SDL for which the bindings were written.
	/// </para>
	/// <para>
	/// It is safe to call this macro from any thread.
	/// </para>
	/// </remarks>
	public const int VERSION = MAJOR_VERSION * 1_000_000 + MINOR_VERSION * 1_000 + MICRO_VERSION;
	
	/// <summary>
	/// This macro will evaluate to true if compiled with SDL at least X.Y.Z.
	/// </summary>
	/// <remarks>
	/// It is safe to call this macro from any thread.
	/// </remarks>
	/// <param name="major">The major version of the version to check</param>
	/// <param name="minor">The minor version of the version to check</param>
	/// <param name="micro">The micro version of the version to check</param>
	/// <returns>
	/// <see langword="true"/> if compiled with SDL at least X.Y.Z, <see langword="false"/> otherwise.
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)] // Marked with AggressiveInlining to mimic the semantics of a macro
	public static bool VERSION_ATLEAST(int major, int minor, int micro)
		=> VERSION >= VERSIONNUM(major, minor, micro);
	
	/// <summary>
	/// This macro turns the version numbers into a numeric value.
	/// </summary>
	/// <remarks>
	/// (1, 2, 3) becomes 1002003.
	/// </remarks>
	/// <param name="major">The major version</param>
	/// <param name="minor">The minor version</param>
	/// <param name="micro">The micro version</param>
	/// <returns>The numeric value of the version</returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)] // Marked with AggressiveInlining to mimic the semantics of a macro
	public static int VERSIONNUM(int major, int minor, int micro)
		=> major * 1_000_000 + minor * 1_000 + micro;
	
	/// <summary>
	/// This macro extracts the major version from a version number
	/// </summary>
	/// <remarks>
	/// <para>
	/// 1,002,003 becomes 1.
	/// </para>
	/// <para>
	/// It is safe to call this macro from any thread.
	/// </para>
	/// </remarks>
	/// <param name="version">The version number</param>
	/// <returns>
	/// The major version
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)] // Marked with AggressiveInlining to mimic the semantics of a macro
	public static int VERSIONNUM_MAJOR(int version)
		=> version / 1_000_000;
	
	/// <summary>
	/// This macro extracts the minor version from a version number
	/// </summary>
	/// <remarks>
	/// <para>
	/// 1,002,003 becomes 2.
	/// </para>
	/// <para>
	/// It is safe to call this macro from any thread.
	/// </para>
	/// </remarks>
	/// <param name="version">The version number</param>
	/// <returns>
	/// The minor version
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)] // Marked with AggressiveInlining to mimic the semantics of a macro
	public static int VERSIONNUM_MINOR(int version)
		=> (version / 1_000) % 1_000;
	
	/// <summary>
	/// This macro extracts the micro version from a version number
	/// </summary>
	/// <remarks>
	/// <para>
	/// 1,002,003 becomes 3.
	/// </para>
	/// <para>
	/// It is safe to call this macro from any thread.
	/// </para>
	/// </remarks>
	/// <param name="version">The version number</param>
	/// <returns>
	/// The micro version
	/// </returns>
	[MethodImpl(MethodImplOptions.AggressiveInlining)] // Marked with AggressiveInlining to mimic the semantics of a macro
	public static int VERSIONNUM_MICRO(int version)
		=> version % 1_000;
#pragma warning restore CA1707
}
