using System;
using System.IO;
using System.Text;
using TempElementsLib.Interfaces;

namespace TempElementsLib
{
    public class TempDir : ITempDir
    {
        DirectoryInfo dirInfo;

        public string DirPath;

        public bool IsDestroyed;
        public bool IsEmpty;

        bool ITempElement.IsDestroyed { get; }

        string ITempDir.DirPath { get; }

        bool ITempDir.IsEmpty { get; }

        ~TempDir()
        {
            Dispose(IsDestroyed);
        }
        public TempDir() : this(Path.GetTempFileName()) { }


        public TempDir(string filename)
        {
            //DirPath = filename;
            dirInfo = new DirectoryInfo(filename);
            File.Create(filename);
        }

        public void Dispose()
        {
            Dispose(IsDestroyed);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool v)
        {
            if (v)
            {
            }

            try
            {
                dirInfo?.Delete();
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void Empty()
        {
            IsEmpty = Directory.Exists(DirPath);
            if (!IsEmpty)
            {
                File.Delete(DirPath);
            }
        }
    }
    public class TempTxtFile : TempFile
    {
        TextReader textReader;
        TextWriter textWriter;
        public TempTxtFile() : this(Path.GetTempFileName()) { }

        public TempTxtFile(string filename)
        {
            FilePath = filename + ".txt";
        }
        public void Write(string text)
        {
            using (textWriter = File.CreateText(FilePath))
            {
                textWriter.Write(text);
                textWriter.Flush();
            }
            textWriter.Dispose();
        }
        public void WriteLine(string text)
        {
            using (textWriter = File.CreateText(FilePath))
            {
                textWriter.WriteLine(text);
                textWriter.Flush();
            }
            textWriter.Dispose();
        }
        public void ReadAllText()
        {
            using (textReader = File.OpenText(FilePath))
                Console.WriteLine(textReader.ReadToEnd());

            textReader.Dispose();
        }
        public void ReadLine()
        {
            using (textReader = File.OpenText(FilePath))
                Console.WriteLine(textReader.ReadLine());
            textReader.Dispose();
        }
        ~TempTxtFile()
        {
            Dispose(IsDestroyed);
        }
        private void Dispose(bool v)
        {
            if (v)
            {
                textReader.Close();
                textWriter.Close();
            }

            try
            {
                FilePath = null;
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
            }
        }

    }
    public class TempFile : ITempFile, IDisposable
    {
        public readonly FileStream fileStream;
        public readonly FileInfo fileInfo;

        public string FilePath;

        public bool IsDestroyed;

        string ITempFile.FilePath { get; }

        bool ITempElement.IsDestroyed { get; }

        public TempFile() : this(Path.GetTempFileName()) { }
        ~TempFile()
        {
            Dispose(IsDestroyed);
        }

        public TempFile(string filename)
        {
            fileInfo = new FileInfo(filename);
            fileStream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
        public void AddText(string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fileStream.Write(info, 0, info.Length);
            fileStream.Flush();
        }

        public void Dispose()
        {
            Dispose(IsDestroyed);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool v)
        {
            if (v)
            {
                fileStream?.Close();
            }

            try
            {
                fileInfo?.Delete();
            }
            catch (IOException exception)
            {
                Console.WriteLine(exception);
            }
        }

    }

}
