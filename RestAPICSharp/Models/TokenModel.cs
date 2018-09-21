using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;

namespace RestAPICSharp.Models
{
    public class TokenModel:APIResponse
    {

        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class ClaimResponse :APIResponse 
    {
        public int RequestId { get; set; }
    }

    public class APIResponse
    {

        public string ApiResponse { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Token { get; set; }
        public string ClientUserName { get; set; }
        public string ClientPassword { get; set; }
        public string ApiRequest { get; set; }
    }

    public class  PayerHelper
    {

        public static SelectList GetSupportedPayerList()
        {

            List<PayerUI> list = new List<PayerUI>(){ new PayerUI(){ Code="00007",Name="Medicare Part A and B" },
                                              new PayerUI(){ Code="00001",Name="Aetna" }
                                  };
          
            return new SelectList(list,"Code","Name");

            }
        }
    

    public class PayerUI
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
