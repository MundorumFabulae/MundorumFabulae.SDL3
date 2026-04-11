using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Properties;

namespace MundorumFabulae.SDL3.Native;

public static partial class NativeMethods
{
	/// <summary>
	/// Clear a property from a group of properties.
	/// </summary>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to clear.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	[LibraryImport("SDL3", EntryPoint = "SDL_ClearProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool ClearProperty(PropertiesID props, string name);

	/// <summary>
	/// Copy all the properties from one group of properties to another.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This copies all properties with the exception of properties requiring cleanup (set using
	/// <see cref="SetPointerPropertyWithCleanup(PropertiesID, string, nint, CleanupPropertyCallback, nint)"/>), which will not be copied.
	/// Any property that already exists on <paramref name="dst"/> will be overwritten.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread. This function requires simultaneous mutex locks on both the source and destination property
	/// sets.
	/// </para>
	/// </remarks>
	/// <param name="src">the properties to copy.</param>
	/// <param name="dst">the destination properties.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_CopyProperties", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool CopyProperties(PropertiesID src, PropertiesID dst);

	/// <summary>
	/// Create a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// All properties are automatically destroyed when <see cref="Quit"/> is called.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <returns>an ID for a new group of properties, or 0 on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_CreateProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial PropertiesID CreateProperties();

	/// <summary>
	/// Destroy a group of properties.
	/// </summary>
	/// <param name="props">the properties to destroy.</param>
	/// <remarks>
	/// <para>
	/// All properties are deleted and their cleanup functions will be called, if any.
	/// </para>
	/// <para>
	/// This function should not be called while these properties are locked or other threads might be setting or getting values from these
	/// properties.
	/// </para>
	/// </remarks>
	[LibraryImport("SDL3", EntryPoint = "SDL_DestroyProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void DestroyProperties(PropertiesID props);

	/// <summary>
	/// Enumerate the properties contained in a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The callback function is called for each property in the group of properties. The properties are locked during enumeration.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="callback">the function to call for each property.</param>
	/// <param name="userdata">a pointer that is passed to <paramref name="callback"/>.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_EnumerateProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static unsafe partial bool EnumerateProperties(
		PropertiesID props,
		delegate* unmanaged[Cdecl]<nint, uint, byte*, void> callback,
		nint userdata
	);

	private record struct EnumeratePropertiesWrappedState(EnumeratePropertiesCallback Callback, nint Userdata);

	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static unsafe void EnumeratePropertiesCallbackWrapper(nint userdata, uint props, byte* name)
	{
		Debug.Assert(name != null);

		var state = Unsafe.AsRef<EnumeratePropertiesWrappedState>((void*)userdata);
		(EnumeratePropertiesCallback callback, var passedUserdata) = state;
		var managedName = Marshal.PtrToStringUTF8((nint)name)!;
		var propsId = new PropertiesID(props);

		callback.Invoke(passedUserdata, propsId, managedName);
	}

	/// <summary>
	/// Enumerate the properties contained in a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The callback function is called for each property in the group of properties. The properties are locked during enumeration.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="callback">the function to call for each property.</param>
	/// <param name="userdata">a pointer that is passed to <paramref name="callback"/>.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	public static bool EnumerateProperties(PropertiesID props, EnumeratePropertiesCallback callback, nint userdata)
	{
		ArgumentNullException.ThrowIfNull(callback);
		
		var stateWrapper = new EnumeratePropertiesWrappedState(callback, userdata);
		unsafe {
			var handleAsPtr = Unsafe.AsPointer(ref stateWrapper);
			return EnumerateProperties(props, &EnumeratePropertiesCallbackWrapper, (nint)handleAsPtr);
		}
	}

	/// <summary>
	/// Get a boolean property from a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can use <see cref="GetPropertyType"/> to query whether the property exists and is a boolean property.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <param name="default_value">the default value of the property to return if it is not set.</param>
	/// <returns>the value of the property, or <paramref name="default_value"/> if it is not set.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetBooleanProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool GetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool default_value);

	/// <summary>
	/// Get a floating point property from a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can use <see cref="GetPropertyType"/> to query whether the property exists and is a floating point property.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <param name="default_value">the default value of the property to return if it is not set.</param>
	/// <returns>the value of the property, or <paramref name="default_value"/> if it is not set.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetFloatProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial float GetFloatProperty(PropertiesID props, string name, float default_value);

	/// <summary>
	/// Get the global SDL properties.
	/// </summary>
	/// <returns>a group of properties that are shared by all of SDL.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetGlobalProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial PropertiesID GetGlobalProperties();

	/// <summary>
	/// Get a number property from a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can use <see cref="GetPropertyType"/> to query whether the property exists and is a number property.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <param name="default_value">the default value of the property to return if it is not set.</param>
	/// <returns>the value of the property, or <paramref name="default_value"/> if it is not set.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetNumberProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial long GetNumberProperty(PropertiesID props, string name, long default_value);

	/// <summary>
	/// Get a pointer property from a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// By convention, the names of properties that SDL exposes on objects will start with "SDL.", and properties that SDL uses internally will start
	/// with "SDL.internal.". These should be considered read-only and should not be modified by applications.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread, although the data returned is not protected and could potentially be freed if you call
	/// <see cref="SetStringProperty"/> or <see cref="ClearProperty"/> on these properties from another thread. If you need to avoid this, use
	/// <see cref="LockProperties"/> and <see cref="UnlockProperties"/>.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <param name="default_value">the default value of the property to return if it is not set.</param>
	/// <returns>the value of the property, or <paramref name="default_value"/> if it is not set.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetPointerProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial nint GetPointerProperty(PropertiesID props, string name, nint default_value);

	/// <summary>
	/// Get the type of a property in a group of properties.
	/// </summary>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <returns>the type of the property, or <see cref="PropertyType.Invalid"/> if it doesn't exist.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetPropertyType", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial PropertyType GetPropertyType(PropertiesID props, string name);

	/// <summary>
	/// Get a string property from a group of properties.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You can use <see cref="GetPropertyType"/> to query whether the property exists and is a string property.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread, although the data returned is not protected and could potentially be freed if you call
	/// <see cref="SetStringProperty"/> or <see cref="ClearProperty"/> on these properties from another thread. This is relevant for this binding
	/// as there is a short window between getting the string property and marshalling the result to a managed string. If you need to avoid this, use
	/// <see cref="LockProperties"/> and <see cref="UnlockProperties"/>.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <param name="default_value">the default value of the property to return if it is not set.</param>
	/// <returns>the value of the property, or <paramref name="default_value"/> if it is not set.</returns>
	public static string? GetStringProperty(
		PropertiesID props,
		string name,
		[SuppressMessage(
			"Naming",
			"CA1707:Identifiers should not contain underscores",
			Justification = "This matches the parameter name in SDL's API."
		)]
		string? default_value
	)
	{
		// SDL returns `default_value` if the property does not exist or is not a string property. To match this behaviour, including with
		// `ReferenceEquals`, the marshalling code for `GetStringProperty` has to be written such that `default_value`
		// is returned directly instead of marshalling the output of `GetStringProperty` if it does return `default_value`.
		
		unsafe {
			var nameMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
			var defaultValueMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();

			try {
				// This error is suppressed as this code is simply matching the output of the source generator.
#pragma warning disable CS9081 // A result of a stackalloc expression of this type in this context may be exposed outside of the containing method
				nameMarshaller.FromManaged(name, stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
				defaultValueMarshaller.FromManaged(default_value, stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]);
#pragma warning restore CS9081 // A result of a stackalloc expression of this type in this context may be exposed outside of the containing method

				var namePtr = nameMarshaller.ToUnmanaged();
				var defaultValuePtr = defaultValueMarshaller.ToUnmanaged();

				var result = __PInvoke(props.Id, namePtr, defaultValuePtr);

				return result == defaultValuePtr
					? default_value
					: Marshal.PtrToStringUTF8((nint)result)!;
			}
			finally {
				defaultValueMarshaller.Free();
				nameMarshaller.Free();
			}

			[DllImport("SDL3", EntryPoint = "SDL_GetStringProperty")]
			[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
			[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
			static extern byte* __PInvoke(uint __props, byte* __name, byte* __default_value);
		}
	}

	/// <summary>
	/// Check if a property exists in a group of properties.
	/// </summary>
	/// <remarks>
	///	It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to query.</param>
	/// <param name="name">the name of the property to query.</param>
	/// <returns><see langword="true"/> if the property exists, <see langword="false"/> otherwise.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_HasProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool HasProperty(PropertiesID props, string name);

	/// <summary>
	/// Lock a group of properties for shared, read-only access by multiple threads.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Obtain a multi-threaded lock for these properties. Other threads will wait while trying to lock these properties until they are unlocked.
	/// Properties must be unlocked before they are destroyed.
	/// </para>
	/// <para>
	/// The lock is automatically taken when setting individual properties, this function is only needed when you want to set several properties
	/// atomically or want to guarantee that properties being queried aren't freed in another thread.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to lock.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_LockProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool LockProperties(PropertiesID props);

	/// <summary>
	/// Set a boolean property in a group of properties.
	/// </summary>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetBooleanProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetBooleanProperty(PropertiesID props, string name, [MarshalAs(UnmanagedType.I1)] bool value);

	/// <summary>
	/// Set a floating point property in a group of properties.
	/// </summary>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetFloatProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetFloatProperty(PropertiesID props, string name, float value);

	/// <summary>
	/// Set a number property in a group of properties.
	/// </summary>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetNumberProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetNumberProperty(PropertiesID props, string name, long value);

	/// <summary>
	/// Set a pointer property in a group of properties.
	/// </summary>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property, or <see cref="IntPtr.Zero"/> to delete the property.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetPointerProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetPointerProperty(PropertiesID props, string name, nint value);

	/// <summary>
	/// Set a pointer property in a group of properties with a cleanup function that is called when the property is deleted.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The cleanup function is also called if setting the property fails for any reason.
	/// </para>
	/// <para>
	/// For simply setting basic data types, like numbers, bools, or strings, use <see cref="SetNumberProperty"/>, <see cref="SetBooleanProperty"/>,
	/// or <see cref="SetStringProperty"/> instead, as those functions will handle cleanup on your behalf. This function is only for more complex,
	/// custom data.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property, or <see cref="IntPtr.Zero"/> to delete the property.</param>
	/// <param name="cleanupCallback">the function to call when this property is deleted, or <see langword="null"/> if no cleanup is necessary.</param>
	/// <param name="userdata">a pointer that is passed to <paramref name="cleanupCallback"/>.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetPointerPropertyWithCleanup", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static unsafe partial bool SetPointerPropertyWithCleanup(
		PropertiesID props,
		string name,
		nint value,
		delegate* unmanaged[Cdecl]<nint, nint, void> cleanupCallback,
		nint userdata
	);
	
	private sealed record class CleanupCallbackWrappedState(CleanupPropertyCallback Callback, nint Userdata);

	[UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
	private static void CleanupCallbackWrapper(nint userdata, nint value)
	{
		using var state = GCHandle<CleanupCallbackWrappedState>.FromIntPtr(userdata);
		(CleanupPropertyCallback callback, var passedUserdata) = state.Target;
		
		callback.Invoke(passedUserdata, value);
	}

	/// <summary>
	/// Set a pointer property in a group of properties with a cleanup function that is called when the property is deleted.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The cleanup function is also called if setting the property fails for any reason.
	/// </para>
	/// <para>
	/// For simply setting basic data types, like numbers, bools, or strings, use <see cref="SetNumberProperty"/>, <see cref="SetBooleanProperty"/>,
	/// or <see cref="SetStringProperty"/> instead, as those functions will handle cleanup on your behalf. This function is only for more complex,
	/// custom data.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property, or <see cref="IntPtr.Zero"/> to delete the property.</param>
	/// <param name="callback">the function to call when this property is deleted, or <see langword="null"/> if no cleanup is necessary.</param>
	/// <param name="userdata">a pointer that is passed to <paramref name="callback"/>.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	public static bool SetPointerPropertyWithCleanup(
		PropertiesID props,
		string name,
		nint value,
		CleanupPropertyCallback callback,
		nint userdata
	)
	{
		ArgumentNullException.ThrowIfNull(callback);

		var wrappedState = new CleanupCallbackWrappedState(callback, userdata);

		var wrappedStateHandle = new GCHandle<CleanupCallbackWrappedState>(wrappedState);
		bool handlePassed = false;

		try {
			var statePtr = GCHandle<CleanupCallbackWrappedState>.ToIntPtr(wrappedStateHandle);

			unsafe {
				var result = SetPointerPropertyWithCleanup(props, name, value, &CleanupCallbackWrapper, statePtr);
				handlePassed = true;
				return result;
			}
		}
		finally {
			if (!handlePassed) {
				wrappedStateHandle.Dispose();
			}
		}
	}

	/// <summary>
	/// Set a string property in a group of properties.
	/// </summary>
	/// <param name="props">the properties to modify.</param>
	/// <param name="name">the name of the property to modify.</param>
	/// <param name="value">the new value of the property, or <see langword="null"/> to delete the property.</param>
	/// <returns><see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.</returns>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetStringProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetStringProperty(PropertiesID props, string name, string? value);

	/// <summary>
	/// Unlock a group of properties.
	/// </summary>
	/// <remarks>
	/// It is safe to call this function from any thread.
	/// </remarks>
	/// <param name="props">the properties to unlock.</param>
	[LibraryImport("SDL3", EntryPoint = "SDL_UnlockProperties")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void UnlockProperties(PropertiesID props);
}
