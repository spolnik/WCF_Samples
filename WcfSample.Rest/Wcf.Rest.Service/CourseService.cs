using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace Wcf.Rest.Service
{
    [ServiceContract]
    public interface ICourseService
    {
        [OperationContract]
        List<string> GetCourseList();
    }

    public class CourseService : ICourseService
    {
        #region Implementation of ICourseService

        public List<string> GetCourseList()
        {
            var courses = new List<string> {
                                               "WCF Fundamentals",
                                               "WF Fundamentals",
                                               "WPF Fundamentals",
                                               "Silverlight Fundamentals"
                                           };

            return courses;
        }

        #endregion
    }
}