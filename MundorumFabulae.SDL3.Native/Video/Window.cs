using System;
using System.Runtime.InteropServices.Marshalling;

using MundorumFabulae.SDL3.Native.Interop;

namespace MundorumFabulae.SDL3.Native.Video;

/// <summary>
/// The struct used as an opaque handle to a window.
/// </summary>
/// <param name="Handle">The pointer handle of the window.</param>
[NativeMarshalling(typeof(WindowMarshaller))]
public readonly record struct Window(nint Handle)
	: IEquatable<nint>
	, IComparable<Window>
	, IComparable<nint>
{
	/// <summary>
	/// Checks if the <see cref="Handle"/> of the current <see cref="Window"/> instance is equal to the specified <see cref="IntPtr"/>.
	/// </summary>
	/// <param name="other">The <see cref="IntPtr"/> to compare with the current <see cref="Handle"/>.</param>
	/// <returns><see langword="true"/> if the <see cref="Handle"/> is equal to <paramref name="other"/>; otherwise, <see langword="false"/>.</returns>
	public bool Equals(IntPtr other)
		=> Handle.Equals(other);

	/// <summary>
	/// Compares the <see cref="Handle"/> of the current <see cref="Window"/> instance with the <see cref="Handle"/> of another <see cref="Window"/>.
	/// </summary>
	/// <param name="other">The <see cref="Window"/> to compare with.</param>
	/// <returns>A value that indicates the relative order of the <see cref="Handle"/> values being compared.</returns>
	public int CompareTo(Window other)
		=> Handle.CompareTo(other.Handle);

	/// <summary>
	/// Compares the <see cref="Handle"/> of the current <see cref="Window"/> instance with the specified <see cref="IntPtr"/>.
	/// </summary>
	/// <param name="other">The <see cref="IntPtr"/> to compare with the current <see cref="Handle"/>.</param>
	/// <returns>A value that indicates the relative order of the <see cref="Handle"/> values being compared.</returns>
	public int CompareTo(IntPtr other)
		=> Handle.CompareTo(other);

	/// <summary>
	/// Compares two <see cref="Window"/> instances.
	/// </summary>
	/// <param name="left">The left operand.</param>
	/// <param name="right">The right operand.</param>
	/// <returns><see langword="true"/> if the left handle is less than the right handle, otherwise <see langword="false"/>.</returns>
	public static bool operator <(Window left, Window right) => left.CompareTo(right) < 0;

	/// <summary>
	/// Compares two <see cref="Window"/> instances.
	/// </summary>
	/// <param name="left">The left operand.</param>
	/// <param name="right">The right operand.</param>
	/// <returns><see langword="true"/> if the left handle is less than or equal to the right handle, otherwise <see langword="false"/>.</returns>
	public static bool operator <=(Window left, Window right) => left.CompareTo(right) <= 0;

	/// <summary>
	/// Compares two <see cref="Window"/> instances.
	/// </summary>
	/// <param name="left">The left operand.</param>
	/// <param name="right">The right operand.</param>
	/// <returns><see langword="true"/> if the left handle is greater than the right handle, otherwise <see langword="false"/>.</returns>
	public static bool operator >(Window left, Window right) => left.CompareTo(right) > 0;

	/// <summary>
	/// Compares two <see cref="Window"/> instances.
	/// </summary>
	/// <param name="left">The left operand.</param>
	/// <param name="right">The right operand.</param>
	/// <returns><see langword="true"/> if the left handle is greater than or equal to the right handle, otherwise <see langword="false"/>.</returns>
	public static bool operator >=(Window left, Window right) => left.CompareTo(right) >= 0;

	/// <summary>
	/// Compares a <see cref="Window"/> instance's <see cref="Handle"/> with a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if the window's <see cref="Handle"/> is less than the specified handle, otherwise <see langword="false"/>.</returns>
	public static bool operator <(Window left, nint right) => left.CompareTo(right) < 0;

	/// <summary>
	/// Compares a <see cref="Window"/> instance's <see cref="Handle"/> with a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if the window's <see cref="Handle"/> is less than or equal to the specified handle, otherwise <see langword="false"/>.</returns>
	public static bool operator <=(Window left, nint right) => left.CompareTo(right) <= 0;

	/// <summary>
	/// Compares a <see cref="Window"/> instance's <see cref="Handle"/> with a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if the window's <see cref="Handle"/> is greater than the specified handle, otherwise <see langword="false"/>.</returns>
	public static bool operator >(Window left, nint right) => left.CompareTo(right) > 0;

	/// <summary>
	/// Compares a <see cref="Window"/> instance's <see cref="Handle"/> with a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if the window's <see cref="Handle"/> is greater than or equal to the specified handle, otherwise <see langword="false"/>.</returns>
	public static bool operator >=(Window left, nint right) => left.CompareTo(right) >= 0;

	/// <summary>
	/// Compares a <see cref="nint"/> with a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if the handle is less than the window's <see cref="Handle"/>, otherwise <see langword="false"/>.</returns>
	public static bool operator <(nint left, Window right) => right.CompareTo(left) > 0;

	/// <summary>
	/// Compares a <see cref="nint"/> with a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if the handle is less than or equal to the window's <see cref="Handle"/>, otherwise <see langword="false"/>.</returns>
	public static bool operator <=(nint left, Window right) => right.CompareTo(left) >= 0;

	/// <summary>
	/// Compares a <see cref="nint"/> with a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if the handle is greater than the window's <see cref="Handle"/>, otherwise <see langword="false"/>.</returns>
	public static bool operator >(nint left, Window right) => right.CompareTo(left) < 0;

	/// <summary>
	/// Compares a <see cref="nint"/> with a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if the handle is greater than or equal to the window's <see cref="Handle"/>, otherwise <see langword="false"/>.</returns>
	public static bool operator >=(nint left, Window right) => right.CompareTo(left) <= 0;

	/// <summary>
	/// Checks if a <see cref="Window"/> instance's <see cref="Handle"/> is equal to a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if they are equal, otherwise <see langword="false"/>.</returns>
	public static bool operator ==(Window left, nint right) => left.Equals(right);

	/// <summary>
	/// Checks if a <see cref="Window"/> instance's <see cref="Handle"/> is not equal to a <see cref="nint"/>.
	/// </summary>
	/// <param name="left">The window.</param>
	/// <param name="right">The handle.</param>
	/// <returns><see langword="true"/> if they are not equal, otherwise <see langword="false"/>.</returns>
	public static bool operator !=(Window left, nint right) => !left.Equals(right);

	/// <summary>
	/// Checks if a <see cref="nint"/> is equal to a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if they are equal, otherwise <see langword="false"/>.</returns>
	public static bool operator ==(nint left, Window right) => right.Equals(left);

	/// <summary>
	/// Checks if a <see cref="nint"/> is not equal to a <see cref="Window"/> instance's <see cref="Handle"/>.
	/// </summary>
	/// <param name="left">The handle.</param>
	/// <param name="right">The window.</param>
	/// <returns><see langword="true"/> if they are not equal, otherwise <see langword="false"/>.</returns>
	public static bool operator !=(nint left, Window right) => !right.Equals(left);
}
