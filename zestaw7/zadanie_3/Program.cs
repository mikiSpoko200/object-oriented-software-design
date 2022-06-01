using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadanie_3
{
    public enum EmailType
    {
        Approval,
        Complaint,
        Order,
        Other,
    }

    public static class EmailTypeMethods
    {
        public const string ApprovalEmailRepresentaion = "approval";
        public const string ComplaintEmailRepresentaion = "complaint";
        public const string OrderEmailRepresentaion = "order";
        public const string OtherEmailRepresentaion = "other";

        public const string TypeSeparator = ":";

        public static EmailType IntoEmailType(this string email)
        {
            switch (email.Trim().ToLower())
            {
                case EmailTypeMethods.ApprovalEmailRepresentaion: return EmailType.Approval;
                case EmailTypeMethods.ComplaintEmailRepresentaion: return EmailType.Complaint;
                case EmailTypeMethods.OrderEmailRepresentaion: return EmailType.Order;
                case EmailTypeMethods.OtherEmailRepresentaion: return EmailType.Other;
                default: throw new ArgumentException("unsupported emial type");
            }
        }

        public static string IntoString(this EmailType type)
        {
            switch (type)
            {
                case EmailType.Approval: return EmailTypeMethods.ApprovalEmailRepresentaion;
                case EmailType.Complaint: return EmailTypeMethods.ComplaintEmailRepresentaion;
                case EmailType.Order: return EmailTypeMethods.OrderEmailRepresentaion;
                case EmailType.Other: return EmailTypeMethods.OtherEmailRepresentaion;
                default: throw new ArgumentException("unsupported emial type");
            }
        }
    }

    public class Email
    {
        public EmailType EmailType { get; set; }
        public string Message { get; set; }
        public Email(string contents)
        {
            string[] data = contents.Split(EmailTypeMethods.TypeSeparator.ToCharArray());
            this.EmailType = data[0].IntoEmailType();
            this.Message = data[1].Trim();
        }

        public static string FromString(EmailType Type, string Contents)
        {
            return Type.IntoString() + EmailTypeMethods.TypeSeparator + Contents;
        }
    }

    public abstract class AbstractHandler<T>
    {
        public AbstractHandler<T> Next { get; set; }

        public abstract bool ProcessRequest(T request);

        public void AppendNextHanlder(AbstractHandler<T> handler)
        {
            if (this.Next != null)
            {
                this.Next.AppendNextHanlder(handler);
            }
            else
            {
                this.Next = handler;
            }
        }

        public void DispatchRequest(T request)
        {
            bool should_dispatch = this.ProcessRequest(request);

            if (should_dispatch && this.Next != null)
            {
                this.Next.DispatchRequest(request);
            }
        }
    }

    #region Concrete Handlers
    public class ApprovalEmailHandler : AbstractHandler<Email>
    {
        public override bool ProcessRequest(Email request)
        {
            if (request.EmailType == EmailType.Approval)
            {
                Console.WriteLine("Przeforwardowano maila pochwalnego do prezesa. Treść:");
                Console.WriteLine($"> {request.Message}\n");
            }
            return true;
        }
    }

    public class ComplaintEmailHandler : AbstractHandler<Email>
    {
        public override bool ProcessRequest(Email request)
        {
            if (request.EmailType == EmailType.Complaint)
            {
                Console.WriteLine("Przeforwardowano maila ze skargą do działu prawnego. Treść:");
                Console.WriteLine($"> {request.Message}\n");
            }
            return true;
        }
    }

    public class OrderEmailHandler : AbstractHandler<Email>
    {
        public override bool ProcessRequest(Email request)
        {
            if (request.EmailType == EmailType.Order)
            {
                Console.WriteLine("Przeforwardowano maila z zamówieniem do działu handlowego. Treść:");
                Console.WriteLine($"> {request.Message}\n");
                
            }
            return true;
        }
    }

    public class MiscellaneousEmialHanlder : AbstractHandler<Email>
    {
        public override bool ProcessRequest(Email request)
        {
            if (request.EmailType == EmailType.Other)
            {
                Console.WriteLine("Przeforwardowano maila do działu marketingu. Treść:");
                Console.WriteLine($"> {request.Message}\n");
            }
            return true;
        }
    }

    public class EmialArchivingHandler : AbstractHandler<Email>
    {
        public override bool ProcessRequest(Email request)
        {
            Console.WriteLine("Zarchiwizowano maila. Treść:");
            Console.WriteLine($"> {request.Message}\n");
            return true;
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            var EmialProcessingPipeline = new ApprovalEmailHandler();
            EmialProcessingPipeline.AppendNextHanlder(new ComplaintEmailHandler());
            EmialProcessingPipeline.AppendNextHanlder(new OrderEmailHandler());
            EmialProcessingPipeline.AppendNextHanlder(new MiscellaneousEmialHanlder());
            EmialProcessingPipeline.AppendNextHanlder(new EmialArchivingHandler());

            string example_1 = "approval: Witam bardzo podoba mi się visual studio 2022.";
            string example_2 = "complaint: Denerwuje brak funkcji auto zapisu.";
            string example_3 = "order: Chciałbym zakupić dobrą implementację Chain Of Responsibility.";
            string example_4 = "other: ZDAŁ ALGORYTMY I STRUKTURY DANYCH W PIERWSZYM TEMRINIE!!1 KLIKNIJ TUTAJ ŻEBY ZOBACZYĆ JAK TO ZROBIŁ!!!";

            var messages = new string[] { example_1, example_2, example_3, example_4 };
            foreach (var message in messages)
            {
                var email = new Email(message);
                EmialProcessingPipeline.DispatchRequest(email);
            }
            Console.ReadLine();
        }
    }
}
