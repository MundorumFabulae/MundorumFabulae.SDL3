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

	/// <summary>
	/// Set an error indicating that memory allocation failed.
	/// </summary>
	/// <returns>
	/// Always <see langword="false"/>.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This function does not do any memory allocation.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	public static bool OutOfMemory()
	{
		// We manually implement the marshalling code of this function to ensure that we do not perform any memory
		// allocation on the library's end.

		// Even though SDL_OutOfMemory is documented to always return false, keeping the check here ensures that if,
		// somehow, SDL_OutOfMemory returns true, we match that behaviour.
		return __PInvoke() != 0;

		[DllImport("SDL3", EntryPoint = "SDL_OutOfMemory")]
		[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
		[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
		static extern sbyte __PInvoke();
	}

	/// <summary>
	/// Set the SDL error message for the current thread.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Calling this function will replace any previous error message that was set.
	/// </para>
	/// <para>
	/// This function always returns false, since SDL frequently uses false to signify a failing result, leading to this
	/// idiom:
	///
	/// <code>
	/// if (error_code != 0) {
	///	    return NativeMethods.SetError($"This operation has failed: {error_code}");
	/// }
	/// </code>
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="message">
	/// The error message to set.
	/// </param>
	/// <returns>
	/// Always <see langword="false"/>.
	/// </returns>
	public static bool SetError(string message)
	{
		unsafe {
			var messageMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();

			try {
#pragma warning disable CS9081
				messageMarshaller.FromManaged(
					message,
					stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
				);
#pragma warning restore CS9081

				var messagePtr = messageMarshaller.ToUnmanaged();
				var formatSpan = "%s"u8;

				fixed (byte* fmtPtr = formatSpan) {
					return __PInvoke(fmtPtr, messagePtr) != 0;
				}
			}
			finally {
				messageMarshaller.Free();
			}

			[DllImport("SDL3", EntryPoint = "SDL_SetError")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
			[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
			static extern sbyte __PInvoke(byte* fmt, byte* msg);
		}
	}

	/// <summary>
	/// A macro to standardize error reporting on unsupported operations.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This simply calls <see cref="SetError"/> with a standardized error string for convenience, consistency, and
	/// clarity.
	/// </para>
	/// <para>
	/// A common usage pattern inside SDL is this:
	///
	/// <code>
	/// bool MyFunction(string? str) {
	///     if (str is null) {
	///         return NativeMethods.InvalidParamError(nameof(str));
	///	    }
	///	    DoSomethingWith(str);
	///	    return true;
	/// }
	/// </code>
	/// </para>
	/// </remarks>
	/// <param name="param"></param>
	/// <returns></returns>
	public static bool InvalidParamError(string param)
	{
		unsafe {
			var paramMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();

			try {
#pragma warning disable CS9081
				paramMarshaller.FromManaged(
					param,
					stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
				);
#pragma warning restore CS9081

				var paramPtr = paramMarshaller.ToUnmanaged();
				var formatSpan = "Parameter '%s' is invalid"u8;

				fixed (byte* fmtPtr = formatSpan) {
					return __PInvoke(fmtPtr, paramPtr) != 0;
				}
			}
			finally {
				paramMarshaller.Free();
			}

			[DllImport("SDL3", EntryPoint = "SDL_SetError")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
			[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
			static extern sbyte __PInvoke(byte* fmt, byte* msg);
		}
	}

	/// <summary>
	/// A macro to standardize error reporting on unsupported operations.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This simply calls <see cref="SetError"/> with a standardized error string for convenience, consistency, and
	/// clarity.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <returns>
	/// Always <see langword="false"/>.
	/// </returns>
	public static bool Unsupported()
	{
		unsafe {
			var message = "That operation is not supported"u8;
			fixed (byte* msgPtr = message) {
				return __PInvoke(msgPtr) != 0;
			}

			[DllImport("SDL3", EntryPoint = "SDL_SetError")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
			[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
			static extern sbyte __PInvoke(byte* fmt);
		}
	}
}
