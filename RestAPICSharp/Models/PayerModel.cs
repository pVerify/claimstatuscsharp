using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace RestAPICSharp.Models
{
    public class PayerModel : APIResponse
    {

        public string DOS { get; set; }
        public int TransactionId { get; set; }
    }

   
}