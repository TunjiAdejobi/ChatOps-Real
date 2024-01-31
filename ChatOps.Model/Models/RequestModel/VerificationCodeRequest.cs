using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatOps.Model.Models.RequestModel
{
    public class VerificationCodeRequest
    {
        public string Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}
