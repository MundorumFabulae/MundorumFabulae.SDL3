using System;
using System.Diagnostics.CodeAnalysis;

namespace MundorumFabulae.SDL3.Native.MessageBox;

/// <summary>
/// Message box flags.
/// </summary>
[Flags]
[SuppressMessage(
	"Naming",
	"CA1711:Identifiers should not have incorrect suffix",
	Justification = "Its corresponding SDL enum also contains the 'Flags' suffix.")]
public enum MessageBoxFlags : uint
{
	/// <summary>
	/// Show an error message box.
	/// </summary>
	Error = 0x0000_0010u,
	/// <summary>
	/// Show a warning message box.
	/// </summary>
	Warning = 0x0000_0020u,
	/// <summary>
	/// Show an informational message box.
	/// </summary>
	Information = 0x0000_0040u,
	/// <summary>
	/// Place the buttons of the message box from left to right.
	/// </summary>
	ButtonsLeftToRight = 0x000_0080u,
	/// <summary>
	/// Place the buttons of the message box from right to left.
	/// </summary>
	ButtonsRightToLeft = 0x000_0100u,
}
