using System;
using System.IO;
using System.IO.Compression;

namespace ConsoleApp4_StreamReader_
{
    class Program
    {
        static void Main(string[] args)
        {
            // здесь записываем в файл  цифры с помощью Stream writer 1млн сделаем через Random.
            Random r = new Random();
            int n = r.Next();
            FileStream fs = new FileStream("numbers.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(fs);
            writer.WriteLine("numbers:");
            for (int i = 0; i < 1000000; i++)
            {
                writer.WriteLine(r.Next(0, 1000));
            }
            writer.Close();
            fs.Close();

            string sourseFile = "numbers.txt";
            string compressedFile = "numbers.gz";
            string targetFile = "numbers_new.txt";
            // здесь делаем сжатия файла
            Compress(sourseFile, compressedFile);
            // восстановления сджатого файла с новым именем numbers_new
            Decompress(compressedFile, targetFile);

            // здесь читаем из файла с помощью Stream reader
            FileStream fr = new FileStream("numbers.txt", FileMode.Open);
            StreamReader reader = new StreamReader(fr);
            string a;
            while ((a = reader.ReadLine()) != null)
            {
            }
            reader.Close();
            fr.Close();
        }
        public static void Compress(string sourceFile, string compressedFile)
        {
            // с using создаем потоки 
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }
        public static void Decompress(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }

        }
    }
}
