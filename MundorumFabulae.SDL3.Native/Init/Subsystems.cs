using System;

namespace MundorumFabulae.SDL3.Native.Init;

/// <summary>
/// Subsystem initialization flags.
/// </summary>
[Flags]
public enum Subsystems : uint
{
	/// <summary>
	/// Audio subsystem.
	/// </summary>
	Audio = 0x00000010u,

	/// <summary>
	/// Video subsystem.
	/// </summary>
	Video = 0x00000020u,

	/// <summary>
	/// Joystick subsystem.
	/// </summary>
	Joystick = 0x00000200u,

	/// <summary>
	/// Haptic (force feedback) subsystem.
	/// </summary>
	Haptic = 0x00001000u,

	/// <summary>
	/// Gamepad subsystem.
	/// </summary>
	Gamepad = 0x00002000u,

	/// <summary>
	/// Events subsystem.
	/// </summary>
	Events = 0x00004000u,

	/// <summary>
	/// Sensor subsystem.
	/// </summary>
	Sensor = 0x00008000u,

	/// <summary>
	/// Camera subsystem.
	/// </summary>
	Camera = 0x00010000u,
}
