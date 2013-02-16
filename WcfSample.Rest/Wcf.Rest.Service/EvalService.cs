using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using System.ServiceModel.Syndication;

namespace Wcf.Rest.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class EvalService : IEvalService
    {
        private readonly List<Eval> _evals = new List<Eval>();
        private int _evalCount = 0;

        #region Implementation of IEvalService

        public void SubmitEval(Eval eval)
        {
            eval.Id = (++_evalCount).ToString();
            _evals.Add(eval);
        }

        public Eval GetEval(string id)
        {
            return string.IsNullOrEmpty(id) ? null : _evals.First(e => e.Id.Equals(id));
        }

        public List<Eval> GetAllEvals()
        {
            return GetEvalsBySubmitter(null);
        }

        public List<Eval> GetEvalsBySubmitter(string submitter)
        {
            return string.IsNullOrEmpty(submitter)
                ? _evals
                : _evals.Where(e => e.Submitter.Equals(submitter)).ToList();
        }

        public void RemoveEval(string id)
        {
            var evalToRemove = _evals.First(e => e.Id.Equals(id));
            _evals.Remove(evalToRemove);
        }

        public SyndicationFeedFormatter GetFeed(string format)
        {
            var evals = GetAllEvals();

            var feed = new SyndicationFeed {
                                               Title = new TextSyndicationContent("Rest atom evaluation."),
                                               Description = new TextSyndicationContent("Recent student eval summary."),
                                               Items = from eval in evals
                                                       select new SyndicationItem
                                                              {
                                                                  Title = new TextSyndicationContent(eval.Submitter),
                                                                  Content = new TextSyndicationContent(eval.Comments)
                                                              }
                                           };

            return format.Equals("atom") ? (SyndicationFeedFormatter)new Atom10FeedFormatter(feed) : new Rss20FeedFormatter(feed);
        }

        #endregion
    }
}
