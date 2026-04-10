namespace MundorumFabulae.SDL3.Native.Init;

/// <summary>
/// Predefined metadata properties for an SDL application.
/// </summary>
public static class AppMetadataProperty
{
	/// <summary>
	/// The human-readable name of the application.
	/// </summary>
	public const string Name = "SDL.app.metadata.name";

	/// <summary>
	/// The version of the app that is running.
	/// </summary>
	public const string Version = "SDL.app.metadata.version";

	/// <summary>
	/// A unique string that identifies this app.
	/// </summary>
	public const string Identifier = "SDL.app.metadata.identifier";

	/// <summary>
	/// The human-readable name of the creator/developer/maker of this app.
	/// </summary>
	public const string Creator = "SDL.app.metadata.creator";

	/// <summary>
	/// The human-readable copyright notice.
	/// </summary>
	public const string Copyright = "SDL.app.metadata.copyright";

	/// <summary>
	/// A URL to the app on the web.
	/// </summary>
	public const string Url = "SDL.app.metadata.url";

	/// <summary>
	/// The type of application this is.
	/// </summary>
	public const string Type = "SDL.app.metadata.type";
}
