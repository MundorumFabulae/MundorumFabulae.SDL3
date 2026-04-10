using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Init;
using MundorumFabulae.SDL3.Native.Interop;

namespace MundorumFabulae.SDL3.Native;

/// <summary>
/// Low-level bindings for SDL3's functions.
/// </summary>
public static partial class NativeMethods
{
	/// <summary>
	/// Get metadata about your app.
	/// </summary>
	/// <param name="name">The name of the metadata property to get.</param>
	/// <returns>
	/// The current value of the metadata property, or the default if it is not set, <see langword="null"/> for properties with no default.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This returns metadata previously set using <see cref="SetAppMetadata"/> or <see cref="SetAppMetadataProperty"/>. See
	/// <see cref="SetAppMetadataProperty"/> for the list of available properties and their meanings.
	/// </para>
	/// <para>
	/// It is safe to call this function from any thread, although the string returned is not protected and could potentially be freed if you call
	/// <see cref="SetAppMetadataProperty"/> to set that property from another thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="SetAppMetadata"/>
	/// <seealso cref="SetAppMetadataProperty"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_GetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalUsing(typeof(NonOwningUtf8StringMarshaller))]
	public static partial string? GetAppMetadataProperty(string name);

	/// <summary>
	/// Initialize the SDL library.
	/// </summary>
	/// <param name="subsystem">Subsystem initialization flags.</param>
	/// <returns>
	/// <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.
	/// </returns>
	/// <remarks>
	/// <para>
	/// <see cref="Init"/> simply forwards to calling <see cref="InitSubSystem"/>. Therefore, the two may be used interchangeably. Though, for
	/// readability of your code, <see cref="InitSubSystem"/> might be preferred.
	/// </para>
	/// <para>
	/// The file I/O (for example: <see cref="IOFromFile"/>) and threading (<see cref="CreateThread"/>) subsystems are initialized by default.
	/// Message boxes (<see cref="ShowSimpleMessageBox"/>) also attempt to work without initializing the video subsystem, in hopes of being useful in
	/// showing an error dialog when <see cref="Init"/> fails. You must specifically initialize other subsystems if you use them in your application.
	/// </para>
	/// <para>
	///	<paramref name="subsystem"/> may be any of the following OR'd together:
	/// <list type="bullet">
	/// <item>
	///		<term><see cref="Subsystems.Audio"/></term>
	///		<description>
	///			audio subsystem; automatically initializes the events subsystem
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Video"/></term>
	///		<description>
	///			video subsystem; automatically initializes the events subsystem, should be initialized on the main thread.
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Joystick"/></term>
	///		<description>
	///			joystick subsystem; automatically initializes the events subsystem
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Haptic"/></term>
	///		<description>
	///			haptic (force feedback) subsystem
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Gamepad"/></term>
	///		<description>
	///			gamepad subsystem; automatically initializes the joystick subsystem
	///		</description>
	///	</item>
	/// <item>
	///		<term><see cref="Subsystems.Events"/></term>
	///		<description>
	///			events subsystem
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Sensor"/></term>
	///		<description>
	///			sensor subsystem; automatically initializes the events subsystem
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="Subsystems.Camera"/></term>
	///		<description>
	///			camera subsystem; automatically initializes the events subsystem
	///		</description>
	/// </item>
	/// </list>
	/// </para>
	/// <para>
	/// Subsystem initialization is ref-counted; you must call <see cref="QuitSubSystem"/> for each <see cref="InitSubSystem"/> to correctly shutdown
	/// a subsystem manually (or call <see cref="Quit"/> to force shutdown). If a subsystem is already loaded, then this call will increase the
	/// ref-count and return.
	/// </para>
	/// <para>
	/// Consider reporting some basic metadata about your application before calling <see cref="Init"/>, using either <see cref="SetAppMetadata"/>
	/// or <see cref="SetAppMetadataProperty"/>.
	/// </para>
	/// <para>
	///	This function should only be called from the main thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="InitSubSystem"/>
	/// <seealso cref="Quit"/>
	/// <seealso cref="QuitSubSystem"/>
	/// <seealso cref="WasInit"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_Init")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool Init(Subsystems subsystem);

	/// <summary>
	/// Compatibility function to initialize the SDL library.
	/// </summary>
	/// <param name="subsystem">Any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
	/// <returns>
	/// <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.
	/// </returns>
	/// <remarks>
	/// <para>
	///	This function and <see cref="Init"/> are interchangeable.
	/// </para>
	/// <para>
	///	This function should only be called from the main thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="Init"/>
	/// <seealso cref="Quit"/>
	/// <seealso cref="QuitSubSystem"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_InitSubSystem")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool InitSubSystem(Subsystems subsystem);

	/// <summary>
	/// Clean up all initialized subsystems.
	/// </summary>
	/// <remarks>
	/// <para>
	/// You should call this function even if you have already shutdown each initialized subsystem with <see cref="QuitSubSystem"/>. It is safe to
	/// call this function even in the case of errors in initialization.
	/// </para>
	/// <para>
	/// You can use this function with <c>atexit()</c> to ensure that it is run when your application is shutdown, but it is not wise to do this from
	/// a library or other dynamically loaded code.
	/// </para>
	/// <para>
	/// This function should only be called from the main thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="Init"/>
	/// <seealso cref="QuitSubSystem"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_Quit")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void Quit();

	/// <summary>
	/// Shut down specific SDL subsystems.
	/// </summary>
	/// <param name="subsystem">Any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
	/// <remarks>
	/// <para>
	/// If you use <see cref="InitSubSystem"/> to initialize a subsystem, you can use this function to shut it down and clean up.
	/// <see cref="InitSubSystem"/> and <see cref="QuitSubSystem"/> are ref-counted.
	/// </para>
	/// <para>
	///	You still need to call <see cref="Quit"/> even if you close all open subsystems with <see cref="QuitSubSystem"/>.
	/// </para>
	/// <para>
	/// This function is not thread-safe.
	/// </para>
	/// </remarks>
	/// <seealso cref="InitSubSystem"/>
	/// <seealso cref="Quit"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_QuitSubSystem")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void QuitSubSystem(Subsystems subsystem);

	/// <summary>
	/// Get a mask of the specified subsystems which are currently initialized.
	/// </summary>
	/// <param name="subsystem">Any of the flags used by <see cref="Init"/>; see <see cref="Init"/> for details.</param>
	/// <returns>
	/// A mask of all initialized subsystems if <paramref name="subsystem"/> is 0, otherwise it returns the initialization status of the specified
	/// subsystems.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This function is not thread-safe.
	/// </para>
	/// </remarks>
	/// <seealso cref="InitSubSystem"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_WasInit")]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial Subsystems WasInit(Subsystems subsystem);

	/// <summary>
	/// Specify basic metadata about your app.
	/// </summary>
	/// <param name="appname">The name of the application ("My Game 2: Bad Guy's Revenge!").</param>
	/// <param name="appversion">The version of the application ("1.0.0beta5" or a git hash, or whatever makes sense).</param>
	/// <param name="appidentifier">A unique string in reverse-domain format that identifies this app ("com.example.mygame2").</param>
	/// <returns>
	/// <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.
	/// </returns>
	/// <remarks>
	/// <para>
	/// You can optionally provide metadata about your app to SDL. This is not required but strongly encouraged.
	/// </para>
	/// <para>
	/// There are several locations where SDL can make use of metadata (an "About" box in the macOS menu bar, the name of the app can be shown on some
	/// audio mixers, etc.). Any piece of metadata can be left as <see langword="null"/> if a specific detail doesn't make sense for the app.
	/// </para>
	/// <para>
	/// This function should be called as early as possible, before <see cref="Init"/>. Multiple calls to this function are allowed, but various
	/// states might not change once it has been set up with a previous call to this function.
	/// </para>
	/// <para>
	/// Passing a <see langword="null"/> removes any previous metadata.
	/// </para>
	/// <para>
	/// This is a simplified interface for the most important information. You can supply significantly more detailed metadata with
	/// <see cref="SetAppMetadataProperty"/>.
	/// </para>
	/// <para>
	///	It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="GetAppMetadataProperty"/>
	/// <seealso cref="SetAppMetadataProperty"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetAppMetadata", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetAppMetadata(string? appname, string? appversion, string? appidentifier);

	/// <summary>
	/// Specify metadata about your app through a set of properties.
	/// </summary>
	/// <param name="key">The name of the metadata property to set.</param>
	/// <param name="value">The value of the property, or <see langword="null"/> to remove that property.</param>
	/// <returns>
	/// <see langword="true"/> on success or <see langword="false"/> on failure; call <see cref="GetError"/> for more information.
	/// </returns>
	/// <remarks>
	/// <para>
	/// You can optionally provide metadata about your app to SDL. This is not required but strongly encouraged.
	/// </para>
	/// <para>
	/// There are several locations where SDL can make use of metadata (an "About" box in the macOS menu bar, the name of the app can be shown on some
	/// audio mixers, etc.). Any piece of metadata can be left out if a specific detail doesn't make sense for the app.
	/// </para>
	/// <para>
	/// This function should be called as early as possible, before <see cref="Init"/>. Multiple calls to this function are allowed, but various
	/// states might not change once it has been set up with a previous call to this function.
	/// </para>
	/// <para>
	/// Once set, this metadata can be read using <see cref="GetAppMetadataProperty"/>.
	/// </para>
	/// <para>
	/// These are the supported properties:
	/// <list type="bullet">
	/// <item>
	///		<term><see cref="AppMetadataProperty.Name"/></term>
	///		<description>
	///			The human-readable name of the application, like "My Game 2: Bad Guy's Revenge!". This will show up anywhere the OS shows the name of
	///			the application separately from window titles, such as volume control applets, etc. This defaults to "SDL Application".
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Version"/></term>
	///		<description>
	///			The version of the app that is running; there are no rules on format, so "1.0.3beta2" and "April 22nd, 2024" and a git hash are all
	///			valid options. This has no default.
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Identifier"/></term>
	///		<description>
	///			A unique string that identifies this app. This must be in reverse-domain format, like "com.example.mygame2". This string is used by
	///			desktop compositors to identify and group windows together, as well as match applications with associated desktop settings and icons.
	///			If you plan to package your application in a container such as Flatpak, the app ID should match the name of your Flatpak container as
	///			well. This has no default.
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Creator"/></term>
	///		<description>
	///			The human-readable name of the creator/developer/maker of this app, like "MojoWorkshop, LLC"
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Copyright"/></term>
	///		<description>
	///			The human-readable copyright notice, like "Copyright (c) 2024 MojoWorkshop, LLC" or whatnot. Keep this to one line; don't paste a copy
	///			of a whole software license in here. This has no default.
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Url"/></term>
	///		<description>
	///			A URL to the app on the web. Maybe a product page, or a storefront, or even a GitHub repository, for user's further information. This
	///			has no default.
	///		</description>
	/// </item>
	/// <item>
	///		<term><see cref="AppMetadataProperty.Type"/></term>
	///		<description>
	///			The type of application this is. Currently, this string can be "game" for a video game, "mediaplayer" for a media player, or
	///			generically "application" if nothing else applies. Future versions of SDL might add new types. This defaults to "application".
	///		</description>
	/// </item>
	/// </list>
	/// </para>
	/// <para>
	///	It is safe to call this function from any thread.
	/// </para>
	/// </remarks>
	/// <seealso cref="GetAppMetadataProperty"/>
	/// <seealso cref="SetAppMetadata"/>
	[LibraryImport("SDL3", EntryPoint = "SDL_SetAppMetadataProperty", StringMarshalling = StringMarshalling.Utf8)]
	[DefaultDllImportSearchPaths(DllImportSearchPath.SafeDirectories)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetAppMetadataProperty(string key, string? value);
}
