# MundorumFabulae.SDL3.Runtime

[![License](https://img.shields.io/github/license/libsdl-org/SDL)](https://github.com/MundorumFabulae/MundorumFabulae.SDL3/blob/main/MundorumFabulae.SDL3.Runtime/LICENSE.txt)

This package contains prebuilt **SDL3** native binaries for use in .NET applications.

It is designed to be used alongside MundorumFabulae.SDL3.Native, but can also serve as a runtime provider for other SDL3
bindings.

## Included Platforms

The binaries included in this package are taken directly from the official SDL3 releases in their
[GitHub repository](https://github.com/libsdl-org/SDL/releases).

| Platform    | Architecture               | Binary Name     |
|:------------|:---------------------------|:----------------|
| **Windows** | x86, x64, ARM64            | `SDL3.dll`      |
| **macOS**   | Universal (x86_64 + ARM64) | `libSDL3.dylib` |

*Note: The macOS binary is extracted from `SDL3-3.4.4.dmg` with the path at*
*`/SDL3/SDL3.xcframework/macos-arm64_x86_64/SDL3.framework/Versions/A/SDL3`.*

### Why no Linux?

This package only redistributes the binaries from the official SDL3 releases, which does not provide pre-built binaries
for Linux. To avoid issues caused by the maintainer's machine's configuration during compilation, this package does not
compile SDL3 from source for Linux.

Developers wanting to vendor SDL3 with their Linux applications should compile their own SDL3 binaries and distribute
them alongside their application.

## Installation

This package is not yet available on NuGet.

## License

SDL3 is licensed under the [Zlib License](https://www.libsdl.org/license.php). This package does not have its own
license, but the other packages in this repository are also licensed under the
[Zlib License](https://github.com/MundorumFabulae/MundorumFabulae.SDL3/blob/main/LICENSE).
