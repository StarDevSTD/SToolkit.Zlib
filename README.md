# SToolkit.Zlib
Zlib manage library, based on componentace.zlib with easy usage

[![NuGet](https://img.shields.io/nuget/v/SToolkit.Zlib.svg)](https://www.nuget.org/packages/SToolkit.Zlib/)
# Install
[Nuget Package](https://www.nuget.org/packages/SToolkit.Zlib/)

Or Nuget console
```
Install-Package SToolkit.Zlib
```
# Examples
[#1](https://github.com/StarDevSTD/SToolkit.Zlib/blob/master/SToolkit.Zlib.Demo/Program.cs)
# Usage
Including
```C#
using SToolkit.Zlib;
```
Compress data
```C#
byte[] file1 = File.ReadAllBytes("image.jpg");
byte[] compressed1 = Zlib.Compress(file1, ZlibCompressionLevel.BEST_COMPRESSION);
File.WriteAllBytes("image.jpg.compress", compressed1);
```
Decompress data
```C#
byte[] decompressed1 = Zlib.Decompress(compressed1, file1.Length);
File.WriteAllBytes("image_decompress.jpg", decompressed1);
```
Check if data compression
```C#
if (Zlib.IsCompressedByZlib(file1))
{
    Console.WriteLine("file1 - compressed by zlib");
}
else
{
    Console.WriteLine("file1 - not compressed by zlib");
}
```
