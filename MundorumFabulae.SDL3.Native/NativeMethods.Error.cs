using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Interop;

namespace MundorumFabulae.SDL3.Native;

public static partial class NativeMethods
{
	/// <summary>
	/// Retrieve a message about the last error that occurred on the current thread.
	/// </summary>
	/// <returns>
	/// A message with information about the specific error that occurred or an empty string if there hasn't been an error message set since the
	/// last call to <see cref="ClearError"/>.
	/// </returns>
	/// <remarks>
	/// <para>
	/// It is possible for multiple errors to occur before calling <see cref="GetError"/>. Only the last error is returned.
	/// </para>
	/// <para>
	/// The message is only applicable when an SDL function has signaled an error. You must check the return values of SDL function calls to determine
	/// when to appropriately call <see cref="GetError"/>. You should <i>not</i> use the results of <see cref="GetError"/> to decide if an error has
	/// occurred! Sometimes, SDL will set an error string even when reporting success.
	/// </para>
	/// <para>
	/// SDL will <i>not</i> clear the error string for successful API calls. You <i>must</i> check return values for failure cases before you can
	/// assume the error string applies.
	/// </para>
	/// <para>
	/// Error strings are set per-thread, so an error set in a different thread will not interfere with the current thread's operation.
	/// </para>
	/// <para>
	/// The returned string is a thread-local string which will remain valid until the current thread's error string is changed.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="ClearError"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetError")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[SuppressGCTransition]
	[return: MarshalUsing(typeof(NonOwningUtf8StringMarshaller))]
	public static partial string GetError();

	/// <summary>
	/// Clear any previous error message for this thread.
	/// </summary>
	/// <returns>
	/// Always <see langword="true"/>.
	/// </returns>
	/// <remarks>
	///	It is safe to call this function from any thread.
	/// </remarks>
	[LibraryImport("SDL3", EntryPoint = "SDL_ClearError")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[SuppressGCTransition]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool ClearError();
}
