using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HYMiniProgram
{
    public class ReturnValue<T>
    {
        public ReturnValue() { }
        public ReturnValue(T data)
        {
            Passed = true;
            Message = "";
        }
        public ReturnValue(bool passed, T data, string failedMessage)
        {
            Passed = passed;
            Data = data;
            Message = failedMessage;
        }
        public string Message { get; set; }
        public bool Passed { get; set; }
        public T Data { get; set; }
    }
}