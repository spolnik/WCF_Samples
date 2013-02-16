using System;
using System.ServiceModel.Web;
using Wcf.Rest.Service;

namespace WcfSample.Rest.Client
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("*** Evaluation Client Application ***\n");

            WebChannelFactory<IEvalService> channelFactory =
                new WebChannelFactory<IEvalService>(new Uri("http://localhost:8080/evalservice"));
            var client = channelFactory.CreateChannel();

            Console.WriteLine("Please enter a command: ");
            var command = Console.ReadLine();

            while (string.IsNullOrEmpty(command) || !command.Equals("exit"))
            {
                switch (command)
                {
                    case "submit":
                        Console.WriteLine("Please enter your name:");
                        var name = Console.ReadLine();
                        Console.WriteLine("Please enter your comments:");
                        var comments = Console.ReadLine();

                        var eval = new Eval {
                                                Timesent = DateTime.Now,
                                                Submitter = name,
                                                Comments = comments
                                            };

                        client.SubmitEval(eval);

                        Console.WriteLine("Evaluation submitted!\n");
                        break;
                    case "get":
                        Console.WriteLine("Please enter the eval id:");
                        var id = Console.ReadLine();

                        var fe = client.GetEval(id);

                        if (fe == null)
                            Console.WriteLine("Not exists.");
                        else
                            Console.WriteLine("{0} -- {1} said: {2} (id {3})\n", fe.Timesent, fe.Submitter, fe.Comments, fe.Id);

                        break;
                    case "list":
                        Console.WriteLine("Please enter the submitter name:");
                        name = Console.ReadLine();

                        var evals = client.GetEvalsBySubmitter(name);
                        evals.ForEach(e => Console.WriteLine("{0} -- {1} said: {2} (id {3})\n", 
                            e.Timesent, e.Submitter, e.Comments, e.Id));
                        Console.WriteLine();
                        break;
                    case "remove":
                        Console.WriteLine("Please enter the eval id:");
                        id = Console.ReadLine();

                        client.RemoveEval(id);

                        Console.WriteLine("Evaluation {0} removed!\n");
                        break;
                    default:
                        Console.WriteLine("Unsupported command.");
                        break;
                }

                Console.WriteLine("Please enter a command: ");
                command = Console.ReadLine();
            }
        }
    }
}
