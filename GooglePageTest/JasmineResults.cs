using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GooglePageTest
{
    public class FailedExpectation
    {
        public string matcherName { get; set; }
        public string message { get; set; }
        public string stack { get; set; }
        public bool passed { get; set; }
        public int? expected { get; set; }
        public int? actual { get; set; }
    }

    public class PassedExpectation
    {
        public string matcherName { get; set; }
        public string message { get; set; }
        public string stack { get; set; }
        public bool passed { get; set; }
    }

    public class JasmineResults
    {
        public string id { get; set; }
        public string description { get; set; }
        public string fullName { get; set; }
        public List<FailedExpectation> failedExpectations { get; set; }
        public List<PassedExpectation> passedExpectations { get; set; }
        public string pendingReason { get; set; }
        public string status { get; set; }
    }
}
