using System.Collections.Generic;

namespace Web_API_Spike.Responses
{
    public class ResponsePackage
    {
        public List<string> Errors { get; set; }

        public object Result { get; set; }

        public ResponsePackage(object result, List<string> errors)
        {
            Errors = errors;
            Result = result;
        }
    }
}