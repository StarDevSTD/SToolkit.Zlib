using System.IO;
using SToolkit.Zlib.Managed;

namespace SToolkit.Zlib
{
    public static class Zlib
    {
        /// <summary>
        /// Buffer size
        /// </summary>
        public static int BufferSize { get; set; } = 131072;

        /// <summary>
        /// Compress input array with selected compression level
        /// </summary>
        /// <param name="data">Data for compression</param>
        /// <param name="compressionLevel">Compression level</param>
        /// <returns>Compressed output array</returns>
        public static byte[] Compress(byte[] data, int compressionLevel)
        {
            MemoryStream ms = new MemoryStream();
            ZOutputStream zos = new ZOutputStream(ms, compressionLevel);
            CopyStream(new MemoryStream(data), zos, data.Length);
            zos.finish();
            return ms.ToArray();
        }

        /// <summary>
        /// Compress input stream with selected compression level
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        /// <param name="compressionLevel">Compression level</param>
        public static void Compress(Stream input, Stream output, int compressionLevel)
        {
            ZOutputStream outZStream = new ZOutputStream(output, compressionLevel);
            CopyStream(input, outZStream);
            outZStream.finish();
        }

        /// <summary>
        /// Decompress input array
        /// </summary>
        /// <param name="data">Data for decompression</param>
        /// <param name="originalSize">Original data size</param>
        /// <returns>Decompressed output array</returns>
        public static byte[] Decompress(byte[] data, int originalSize)
        {
            byte[] output = new byte[originalSize];
            ZOutputStream zos = new ZOutputStream(new MemoryStream(output));
            CopyStream(new MemoryStream(data), zos, originalSize);
            return output;
        }

        /// <summary>
        /// Decompress input stream
        /// </summary>
        /// <param name="input">Input stream</param>
        /// <param name="output">Output stream</param>
        public static void Decompress(Stream input, Stream output)
        {
            ZOutputStream outZStream = new ZOutputStream(output);
            CopyStream(input, outZStream);
            outZStream.finish();
        }

        /// <summary>
        /// Check data for compression by zlib
        /// </summary>
        /// <param name="stream">Input stream</param>
        /// <returns>Return true if data compressed by zlib</returns>
        public static bool IsCompressedByZlib(Stream stream)
        {
            byte[] data = new byte[2];
            stream.Read(data, 0, 2);
            stream.Seek(-2, SeekOrigin.Current);
            return IsCompressedByZlib(data);
        }

        /// <summary>
        /// Check data for compression by zlib
        /// </summary>
        /// <param name="data">Input array</param>
        /// <returns>Return true if data compressed by zlib</returns>
        public static bool IsCompressedByZlib(byte[] data)
        {
            if (data.Length < 2)
                return false;
            if (data[0] == 0x78)
            {
                if (data[1] == 0x01 || data[1] == 0x5E || data[1] == 0x9C || data[1] == 0xDA)
                    return true;
            }
            return false;
        }

        private static void CopyStream(Stream input, Stream output, int size = 0)
        {
            if (size == 0)
                size = BufferSize;
            byte[] buffer = new byte[size];
            int len;
            while ((len = input.Read(buffer, 0, size)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
    }
}
