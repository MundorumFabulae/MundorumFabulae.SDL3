using System.Diagnostics.CodeAnalysis;

namespace MundorumFabulae.SDL3.Native.MessageBox;

/// <summary>
/// A set of colors to use for message box dialogs.
/// </summary>
[SuppressMessage(
	"Design",
	"CA1051:Do not declare visible instance fields",
	Justification =
		"This struct is designed as a thin wrapper around the array of colors in SDL. This implementation mimics those semantics."
)]
public record struct MessageBoxColorScheme
{
	/// <summary>
	/// The colors used for the message box.
	/// </summary>
	public MessageBoxColorSchemeArray colors;
}
