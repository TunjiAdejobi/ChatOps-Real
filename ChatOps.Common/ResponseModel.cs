using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Common
{
    public class ResponseModel
    {
        public bool IsSuccessful { get; set; } = true;
        public string? Message { get; set; }
        public bool HasErrors { get; set; }
        public bool Data { get; set; }
    }
    public class ResponseModel<T> : ResponseModel
    {
        public T Data { get; set; }
    }
}
