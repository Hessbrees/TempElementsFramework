using System;
using System.IO;
using System.Text;

namespace TempElementsLib.Interfaces
{
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
            Dispose(false);
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
            Dispose(true);
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
