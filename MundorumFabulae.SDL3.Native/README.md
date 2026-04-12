# MundorumFabulae.SDL3.Native

[![License](https://img.shields.io/github/license/MundorumFabulae/MundorumFabulae.SDL3)](https://github.com/MundorumFabulae/MundorumFabulae.SDL3/blob/main/MundorumFabulae.SDL3.Native/LICENSE)

Low-level C# bindings for SDL3. This library provides direct access to SDL's C API, with an API designed to allow
writing code that would look similar to C code.

## Mapping Conventions

This library follows the following convention to map SDL's C API to C#:

- **Functions and Function-like Macros**: Located in the `NativeMethods` static class. The `SDL_` prefix is removed
  (e.g., `SDL_CreateWindow` becomes `NativeMethods.CreateWindow` and `SDL_VERSIONNUM` becomes
  `NativeMethods.VERSIONNUM`). Almost all functions available in the SDL wiki are (planned to be) included,
  even those that would technically be useless under C# like threading, file I/O, etc.
  - For functions which take a function pointer as an argument, two overloads are provided: one that takes an unmanaged
    function pointer directly, and one that takes a delegate with a signature matching the function pointer. The
    delegate is wrapped along with any userdata pointers and an `[UnmanagedCallersOnly]` wrapper function is passed to
    the first overload to ensure that the delegate is called correctly.
    - Delegates passed to the second overload should **NOT** throw exceptions. The wrapper function will **NOT** catch
      exceptions thrown by the delegate as there would be no way to bubble the exception up to the caller if one gets
      thrown.
    - See [NativeMethods.SetPointerPropertyWithCleanup](NativeMethods.Properties.cs) for an example of this.
  - Variadic functions such as `SDL_Log` have their signature modified so that they only accept the formatted string.
    This is passed to the variadic function with `"%s"` as the format string.
  - Functions which take a `va_list` are not included as P/Invoke does not support them.
- **Enums and Flags**: Mapped to C# enums. Their backing type is set to whatever type is used in the C API.
- **Pointers/Handles**: Mapped to C# record structs for type-safety. Comparison operators between the struct and the
  underlying type are provided to allow code similar to how they would be done in C.
  - Implicit conversion to `bool` is **NOT** provided, even though it is a common pattern in C to check for a null
    pointer.
  - Wrapped pointers/handles do **NOT** implement IDisposable. They must be treated as an unmanaged resource and cleaned
    manually much like how it would be done in C.
- **Documentation**: Documentation for functions, types, etc. is recreated from the official SDL3 documentation on a
  best-effort basis. Names are matched to refer to functions and types in the package.

## Installation

This package is not yet available on NuGet.

This package does not include binaries for SDL 3. If you want to bundle SDL 3 with your application, the
MundorumFabulae.SDL3.Runtime package redistributes the official SDL 3 releases for Windows and macOS.

## Usage Example

The following example shows how to initialize SDL 3 and set application metadata. This example mirrors how an SDL3
application would be written in C. Note that due to other C# language features like exceptions, you **SHOULD NOT**
actually write your application like this if you want it to be safe.

```csharp
using MundorumFabulae.SDL3.Native;
using MundorumFabulae.SDL3.Native.Init;

// Set application metadata before initialization
NativeMethods.SetAppMetadata("My SDL Game", "1.0.0", "com.example.mygame");

// Initialize SDL with Video and Events subsystems
if (!NativeMethods.Init(Subsystems.Video | Subsystems.Events))
{
    string error = NativeMethods.GetError();
    NativeMethods.LogError($"Failed to initialize SDL: {error}");
    goto cleanup;
}

NativeMethods.LogDebug("SDL initialized successfully!");

int version = NativeMethods.GetVersion();
int major = NativeMethods.VERSIONNUM_MAJOR(version);
int minor = NativeMethods.VERSIONNUM_MINOR(version);
int patch = NativeMethods.VERSIONNUM_PATCH(version);
NativeMethods.LogInfo($"Using SDL3 {major}.{minor}.{patch}");

// Your application logic goes here...

// Clean up and exit
cleanup:
NativeMethods.Quit();
```

## License

This project is licensed under the [Zlib License](LICENSE). SDL3 itself is also licensed under the Zlib License.
