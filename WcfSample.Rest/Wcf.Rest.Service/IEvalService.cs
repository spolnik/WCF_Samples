using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

namespace Wcf.Rest.Service
{
    [DataContract(Namespace = "http://nprogramming.worpress.com/evals")]
    public class Eval
    {
        [DataMember]
        public string Id;

        [DataMember]
        public string Submitter;

        [DataMember]
        public DateTime Timesent;

        [DataMember]
        public string Comments;
    }

    [ServiceKnownType(typeof(Atom10FeedFormatter))]
    [ServiceKnownType(typeof(Rss20FeedFormatter))]
    [ServiceContract]
    public interface IEvalService
    {
        [WebInvoke(Method = "POST", UriTemplate = "evals")]
        [OperationContract]
        void SubmitEval(Eval eval);

        [WebGet(UriTemplate="eval/{id}")]
        [OperationContract]
        Eval GetEval(string id);

        [WebGet(UriTemplate = "evals", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Eval> GetAllEvals();

        [WebGet(UriTemplate = "evals/{submitter}")]
        [OperationContract]
        List<Eval> GetEvalsBySubmitter(string submitter);

        [WebInvoke(Method = "DELETE", UriTemplate = "eval/{id}")]
        [OperationContract]
        void RemoveEval(string id);

        [WebGet(UriTemplate = "evals/feed/{format}")]
        [OperationContract]
        SyndicationFeedFormatter GetFeed(string format);
    }
}
