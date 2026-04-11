using System;
using System.Runtime.InteropServices.Marshalling;
using MundorumFabulae.SDL3.Native.Interop;

namespace MundorumFabulae.SDL3.Native.Properties;

/// <summary>
/// An ID that represents a group of properties.
/// </summary>
/// <remarks>
/// <para>
///	The implementation of this struct allows for direct comparison with its backing type to allow C-like code as shown
/// below.
///	<code>
/// var id = NativeMethods.CreateProperties();
/// if (id == 0) {
///     Console.WriteLine($"Failed to create properties: {NativeMethods.GetError()}");
/// }
/// </code>
/// </para>
/// </remarks>
/// <param name="Id">the property group ID.</param>
[NativeMarshalling(typeof(PropertiesIDMarshaller))]
public readonly record struct PropertiesID(uint Id)
    : IComparable<PropertiesID>,
        IEquatable<uint>,
        IComparable<uint>
{
	/// <summary>
	/// An invalid property group ID.
	/// </summary>
	public static PropertiesID Invalid { get; } = new(0);

	/// <summary>
	/// Checks if the current <see cref="PropertiesID"/> is equal to the specified <see cref="uint"/>.
	/// </summary>
	/// <param name="other">the <see cref="uint"/> to compare.</param>
	/// <returns><see langword="true"/> if equal; otherwise, <see langword="false"/>.</returns>
	public bool Equals(uint other)
		=> Id.Equals(other);
	
	/// <summary>
	/// Compares the current <see cref="PropertiesID"/> to another <see cref="PropertiesID"/>.
	/// </summary>
	/// <param name="other">the <see cref="PropertiesID"/> to compare.</param>
	/// <returns>A value indicating the relative order of the objects being compared.</returns>
	public int CompareTo(PropertiesID other)
		=> Id.CompareTo(other.Id);
	
    /// <summary>
    /// Compares the current <see cref="PropertiesID"/> to a <see cref="uint"/>.
    /// </summary>
    /// <param name="other">the <see cref="uint"/> to compare.</param>
    /// <returns>A value indicating the relative order of the objects being compared.</returns>
    public int CompareTo(uint other)
        => Id.CompareTo(other);

    /// <summary>
    /// Compares two <see cref="PropertiesID"/> instances to determine if the left is less than the right.
    /// </summary>
    /// <param name="left">the left <see cref="PropertiesID"/>.</param>
    /// <param name="right">the right <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<(PropertiesID left, PropertiesID right)
        => left.Id < right.Id;

    /// <summary>
    /// Compares two <see cref="PropertiesID"/> instances to determine if the left is less than or equal to the right.
    /// </summary>
    /// <param name="left">the left <see cref="PropertiesID"/>.</param>
    /// <param name="right">the right <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<=(PropertiesID left, PropertiesID right)
        => left.Id <= right.Id;

    /// <summary>
    /// Compares two <see cref="PropertiesID"/> instances to determine if the left is greater than the right.
    /// </summary>
    /// <param name="left">the left <see cref="PropertiesID"/>.</param>
    /// <param name="right">the right <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>(PropertiesID left, PropertiesID right)
        => left.Id > right.Id;

    /// <summary>
    /// Compares two <see cref="PropertiesID"/> instances to determine if the left is greater than or equal to the right.
    /// </summary>
    /// <param name="left">the left <see cref="PropertiesID"/>.</param>
    /// <param name="right">the right <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>=(PropertiesID left, PropertiesID right)
	    => left.Id >= right.Id;
    
    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if they are equal.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if they are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator==(PropertiesID left, uint right)
        => left.Id == right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if they are equal.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if they are equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator==(uint left, PropertiesID right)
        => left == right.Id;

    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if they are not equal.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if they are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator!=(PropertiesID left, uint right)
        => left.Id != right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if they are not equal.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if they are not equal; otherwise, <see langword="false"/>.</returns>
    public static bool operator!=(uint left, PropertiesID right)
        => left != right.Id;

    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if the left is less than the right.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<(PropertiesID left, uint right)
        => left.Id < right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if the left is less than the right.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<(uint left, PropertiesID right)
        => left < right.Id;

    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if the left is less than or equal to the right.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<=(PropertiesID left, uint right)
        => left.Id <= right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if the left is less than or equal to the right.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator<=(uint left, PropertiesID right)
        => left <= right.Id;

    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if the left is greater than the right.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>(PropertiesID left, uint right)
        => left.Id > right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if the left is greater than the right.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>(uint left, PropertiesID right)
        => left > right.Id;

    /// <summary>
    /// Compares a <see cref="PropertiesID"/> and a <see cref="uint"/> to determine if the left is greater than or equal to the right.
    /// </summary>
    /// <param name="left">the <see cref="PropertiesID"/>.</param>
    /// <param name="right">the <see cref="uint"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>=(PropertiesID left, uint right)
        => left.Id >= right;

    /// <summary>
    /// Compares a <see cref="uint"/> and a <see cref="PropertiesID"/> to determine if the left is greater than or equal to the right.
    /// </summary>
    /// <param name="left">the <see cref="uint"/>.</param>
    /// <param name="right">the <see cref="PropertiesID"/>.</param>
    /// <returns><see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref name="right"/>; otherwise, <see langword="false"/>.</returns>
    public static bool operator>=(uint left, PropertiesID right)
        => left >= right.Id;
}
