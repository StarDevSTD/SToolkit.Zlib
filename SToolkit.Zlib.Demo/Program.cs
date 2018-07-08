using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SToolkit.Zlib;

namespace SToolkit.Zlib.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = new string[] { "image.jpg.compress", "pdf_book.pdf.compress", "image_decompress.jpg", "pdf_book_decompress.pdf",
                "image.jpg.compress_stream", "image_decompress_stream.jpg", "", "" };
            foreach (string file in files)
            {
                if (File.Exists(file))
                    File.Delete(file);
            }
            byte[] file1 = File.ReadAllBytes("image.jpg");
            if (Zlib.IsCompressedByZlib(file1))
            {
                Console.WriteLine("file1 - compressed by zlib");
            }
            else
            {
                Console.WriteLine("file1 - not compressed by zlib");
            }
            var fs1 = new FileStream("image.jpg", FileMode.Open, FileAccess.Read);
            var fs2 = new FileStream("image.jpg.compress_stream", FileMode.Create, FileAccess.Write);
            Zlib.Compress(fs1, fs2, ZlibCompressionLevel.BEST_COMPRESSION);
            fs2.Close();
            Zlib.Decompress(new FileStream("image.jpg.compress_stream", FileMode.Open, FileAccess.Read),
                new FileStream("image_decompress_stream.jpg", FileMode.Create, FileAccess.Write));
            byte[] file2 = File.ReadAllBytes("pdf_book.pdf");
            byte[] compressed1 = Zlib.Compress(file1, ZlibCompressionLevel.BEST_COMPRESSION);
            File.WriteAllBytes("image.jpg.compress", compressed1);
            if (Zlib.IsCompressedByZlib(compressed1))
            {
                Console.WriteLine("compressed1 - compressed by zlib");
            }
            else
            {
                Console.WriteLine("compressed1 - not compressed by zlib");
            }
            byte[] compressed2 = Zlib.Compress(file2, ZlibCompressionLevel.BEST_COMPRESSION);
            File.WriteAllBytes("pdf_book.pdf.compress", compressed2);
            byte[] decompressed1 = Zlib.Decompress(compressed1, file1.Length);
            File.WriteAllBytes("image_decompress.jpg", decompressed1);
            byte[] decompressed2 = Zlib.Decompress(compressed2, file2.Length);
            File.WriteAllBytes("pdf_book_decompress.pdf", decompressed2);
            Console.ReadKey();
        }
    }
}
