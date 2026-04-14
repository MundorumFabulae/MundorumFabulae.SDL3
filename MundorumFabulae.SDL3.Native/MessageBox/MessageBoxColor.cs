namespace MundorumFabulae.SDL3.Native.MessageBox;

/// <summary>
/// RGB value used in a message box color scheme.
/// </summary>
/// <param name="R">The red component.</param>
/// <param name="G">The green component.</param>
/// <param name="B">The blue component.</param>
public readonly record struct MessageBoxColor(byte R, byte G, byte B)
{
	private readonly byte _r = R;
	private readonly byte _g = G;
	private readonly byte _b = B;

	/// <summary>
	/// The red component.
	/// </summary>
	public byte R {
		get => _r;
		init => _r = value;
	}

	/// <summary>
	/// The green component.
	/// </summary>
	public byte G {
		get => _g;
		init => _g = value;
	}

	/// <summary>
	/// The blue component.
	/// </summary>
	public byte B {
		get => _b;
		init => _b = value;
	}
}
