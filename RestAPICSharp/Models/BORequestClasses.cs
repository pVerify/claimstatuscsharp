using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestAPICSharp.Models
{
    public class PboRequest
    {
        public PboClaimStatusRequest Request { get; set; }
        public string ServiceCodes { get; set; }
        public APIResponse Response { get; set; }
    }
    public class PboClaimStatusRequest
    {
        [Required(ErrorMessage = "PayerCode is missing")]
        public string PayerCode { set; get; }

        public string ProviderFirstName { get; set; }
        public string ProviderLastName { get; set; }


        [RegularExpression(@"\d{10}",
         ErrorMessage = "NPI is 10 digit number.Characters are not allowed.")]
        public string ProviderNPI { set; get; }
        [RegularExpression(@"\d{9}",
         ErrorMessage = "Federal Tax Id is 9 digit number.Characters are not allowed.")]
        public string ProviderFederalTaxId { set; get; }



        public string PayerClaimNumber { get; set; }

        [Required(ErrorMessage = "Charge Amount is required")]


        public decimal ChargeAmount { get; set; }

        [Required(ErrorMessage = "Service start date is missing")]
        public string ServiceStartDate { set; get; }

        [Required(ErrorMessage = "Service end date is missing")]
        public string ServiceEndDate { set; get; }


        [Required(ErrorMessage = "Subscriber is missing")]
        public PboClaimRequestSubscriber Subscriber { set; get; }                //EDI270Subscriber_R

        public bool IsPatientDependent { get; set; }
        public Dependent Dependent { set; get; }
   
        public string RequestSource { get; set; }
    }

    public class Dependent
    {
        public string FirstName { set; get; }
      
        public string LastName { set; get; }
        public string DOB { set; get; }
      
    }

    public class PboClaimRequestSubscriber
    {
        public string FirstName { set; get; }
     
        [Required(ErrorMessage = "Subscriber Last Name is missing")]
        public string LastName { set; get; }
        public string DOB { set; get; }
     
        [Required(ErrorMessage = "Member Id is missing")]
        public string MemberID { get; set; }
    }
}