using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Infraestructure.Custom
{
    public class ResultEntity
    {
        public bool ResultOk { get; set; }
        public object BusinessEntity { get; set; }
        public string Message { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
