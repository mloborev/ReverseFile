using System;
using System.IO;

namespace ReverseFile
{
    public class Program
    {
        public static byte[] _byteArray = new byte[1000];
        static void Main(string[] args)
        {
            //ReverseFile.exe binaryFile.dat reversedBinaryFile.dat
            var inputFilePath = args[0];
            var outputFilePath = args[1];
            CreateBinaryFile(inputFilePath);
            Console.WriteLine("Исходный массив:");
            ReadBinaryFile(inputFilePath);
            Console.WriteLine("Перевёрнутый массив:");
            ReadAndReverseBinaryFile(inputFilePath, outputFilePath);
        }

        public static void CreateBinaryFile(string path)
        {
            using (FileStream file = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                using (var writer = new BinaryWriter(file))
                {
                    var bytes = new byte[] { 0x0A, 0x00, 0x20, 0xFF };
                    writer.Write(bytes);
                }
            }
        }

        public static void ReadBinaryFile(string path)
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                var counter = 0;
                while (reader.PeekChar() > -1)
                {
                    Console.Write(reader.ReadByte() + " ");
                    counter++;
                }
                Console.WriteLine();
            }
        }

        public static void ReadAndReverseBinaryFile(string inputPath, string outputPath)
        {
            using (FileStream inputStream = new FileStream(inputPath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.SequentialScan))
            {
                using (FileStream outputStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.SequentialScan))
                {
                    long pos = inputStream.Length - 1;
                    int ch;
                    while (pos >= 0)
                    {
                        inputStream.Seek(pos, SeekOrigin.Begin);
                        ch = inputStream.ReadByte();
                        Console.Write(ch + " ");
                        outputStream.WriteByte((byte)ch);
                        pos--;
                    }
                }
            }
        }
    }
}
