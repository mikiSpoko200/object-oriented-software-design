using System;
using System.Net;
using System.IO;
using System.Linq;

namespace zadanie_1
{
    public interface ICommand
    {
        void Execute();
    }

    #region FTP Command
    public struct FTPDownloadCommandState
    {
        public FTPDownloadCommandState(Uri Uri, string FileName)
        {
            this.FileName = FileName;
            this.Uri = Uri;
        }
        public string FileName { get; set; }
        public Uri Uri { get; set; }
    }

    public class FTPDownloadCommand : ICommand
    {
        public FTPDownloadCommandState State { get; set; }

        public FTPDownloadCommand(FTPDownloadCommandState state)
        {
            this.State = state;
        }

        public void Execute()
        {
            if (this.State.Uri.Scheme != Uri.UriSchemeFtp)
            {
                throw new ArgumentException("Invalid URI scheme.");
            }
            using (WebClient request = new WebClient())
            {
                try
                {
                    request.DownloadFile(this.State.Uri.ToString(), this.State.FileName);
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
    #endregion


    #region HTTP Download Command
    public struct HTTPDownloadCommandState
    {
        public HTTPDownloadCommandState(Uri Uri, string FileName)
        {
            this.Uri = Uri;
            this.FileName = FileName;
        }
        public string FileName { get; set; }
        public Uri Uri { get; set; }
    }

    public class HTTPDownloadCommand : ICommand
    {
        public HTTPDownloadCommandState State { get; set; }
        public HTTPDownloadCommand(HTTPDownloadCommandState State)
        {
            this.State = State;
        }
        public void Execute()
        {
            if (this.State.Uri.Scheme != Uri.UriSchemeHttp)
            {
                throw new ArgumentException("Invalid Protocol.");
            }
            using (var client = new WebClient())
            {
                try
                {
                    client.DownloadFile(this.State.Uri.ToString(), this.State.FileName);
                }
                catch (WebException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
    #endregion


    #region Random File Command
    public struct RandomFileCreateCommandState
    {
        public string FilePath { get; set; }
        public RandomFileCreateCommandState(string FilePath)
        {
            this.FilePath = FilePath;
        }
    }

    public class RandomFileCreateCommand : ICommand
    {
        public RandomFileCreateCommandState State { get; set; }
        public RandomFileCreateCommand(RandomFileCreateCommandState State)
        {
            this.State = State;
        }

        public void Execute()
        {
            string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            Random random = new Random();
            using (var writer = new StreamWriter(this.State.FilePath))
            {
                StringWriter sw = new StringWriter();
                for (int i = 0; i < 100; i++)
                {
                    sw.Write(Chars[random.Next(Chars.Length)]);
                }
                writer.WriteLine(sw.ToString());
            }
        }
    }
    #endregion


    #region Copy File Command
    public struct CopyFileCommandState
    {
        public string OldFileName { get; set; }
        public string NewFileName { get; set; }
        public CopyFileCommandState(string OldFileName, string NewFileName)
        {
            this.OldFileName = OldFileName;
            this.NewFileName = NewFileName;
        }
    }
    
    public class CopyFileCommand : ICommand
    {
        public CopyFileCommandState State { get; set; }
        public CopyFileCommand(CopyFileCommandState State)
        {
            this.State = State;
        }

        public void Execute()
        {
            File.Copy(this.State.OldFileName, this.State.NewFileName);
        }
    }
    #endregion


    internal class Program
    {
        static void Main(string[] args)
        {
            //var uri = new Uri("http://example.org/");
            //var command = new FTPDownloadCommand(new FTPDownloadCommandState(uri, "../../http_file.txt"));

            //var command = new RandomFileCreateCommand(new RandomFileCreateCommandState("../../random_file.txt"));
            var command = new CopyFileCommand(new CopyFileCommandState("../../random_file.txt", "../../random_file_copy.txt"));

            command.Execute();
            Console.ReadLine();
        }
    }
}
