using System;

namespace URLTester.Business
{
    [Serializable]
    public class FileStatusObject
    {
        public string FileName { get; set; }
        public string StatusCode { get; set; }
        public string Details { get; set; }
    }
}
