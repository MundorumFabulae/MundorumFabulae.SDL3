# MundorumFabulae.SDL3

[![License](https://img.shields.io/github/license/MundorumFabulae/MundorumFabulae.SDL3)](https://github.com/MundorumFabulae/MundorumFabulae.SDL3/blob/main/LICENSE)

`MundorumFabulae.SDL3` is a collection of .NET libraries for working with [SDL 3](https://libsdl.org/). It provides
low-level, high-performance bindings to the C API, allowing you to build cross-platform multimedia applications using C#.

## Projects

- **[MundorumFabulae.SDL3.Native](MundorumFabulae.SDL3.Native/)**: Low-level C# bindings for the SDL 3 C API.
- **[MundorumFabulae.SDL3.Runtime](MundorumFabulae.SDL3.Runtime/)**: Native binaries for SDL 3 (Windows and macOS),
  packaged for easy distribution in .NET.

## Getting Started

To use SDL 3 in your .NET project, you should reference both the bindings and the runtime provider (unless you want to
provide your own binaries).

See the README in the sub-projects for usage instructions.

## Installation

These packages are not yet available on NuGet.

## License

This project is licensed under the [Zlib License](LICENSE). SDL 3 itself is also licensed under the Zlib License.
