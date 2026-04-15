using System;
using System.Runtime.CompilerServices;

namespace MundorumFabulae.SDL3.Native.MessageBox;

/// <summary>
/// A fixed-size array of 5 <see cref="MessageBoxColor" /> objects.
/// </summary>
[InlineArray(5)]
public struct MessageBoxColorSchemeArray
	: IEquatable<MessageBoxColorSchemeArray>
{
	private MessageBoxColor _element;

	/// <summary>
	/// Gets or sets the color for the specified <see cref="MessageBoxColorType" />.
	/// </summary>
	/// <param name="type">The color type to get or set.</param>
	/// <returns>The color for the specified color type.</returns>
	public MessageBoxColor this[MessageBoxColorType type] {
		readonly get => this[(int)type];
		set => this[(int)type] = value;
	}
	
	/// <inheritdoc />
	public bool Equals(MessageBoxColorSchemeArray other)
	{
		for (int i = 0; i < 5; i++) {
			if (this[i] != other[i]) {
				return false;
			}
		}

		return true;
	}

	/// <inheritdoc />
	public override bool Equals(object? obj)
		=> obj is MessageBoxColorSchemeArray other && Equals(other);

	/// <inheritdoc />
	public override int GetHashCode()
		=> HashCode.Combine(this[0], this[1], this[2], this[3], this[4]);

	/// <summary>
	/// Compares two <see cref="MessageBoxColorSchemeArray" /> objects for equality.
	/// </summary>
	/// <param name="left">The left side of the comparison.</param>
	/// <param name="right">The right side of the comparison.</param>
	/// <returns><see langword="true" /> if the objects are equal; otherwise, <see langword="false" />.</returns>
	public static bool operator==(MessageBoxColorSchemeArray left, MessageBoxColorSchemeArray right)
		=> left.Equals(right);

	/// <summary>
	/// Compares two <see cref="MessageBoxColorSchemeArray" /> objects for inequality.
	/// </summary>
	/// <param name="left">The left side of the comparison.</param>
	/// <param name="right">The right side of the comparison.</param>
	/// <returns><see langword="true" /> if the objects are NOT equal; otherwise, <see langword="false" />.</returns>
	public static bool operator!=(MessageBoxColorSchemeArray left, MessageBoxColorSchemeArray right)
		=> !left.Equals(right);
}
