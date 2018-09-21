using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace RestAPICSharp.Models
{
    public class ResponseBase
    {
        public int APIResponseCode { get; set; }
        public string APIResponseMessage { get; set; }
    }
    public class PboClaimStatusResponse : ResponseBase
    {
        public long RequestId { get; set; }
        public string PayerClaimNo { get; set; }
        public string CategoryCode { get; set; }
        public string Category { get; set; }
        public string ClaimStatusReasonCode { get; set; }
        public string ClaimStatus { get; set; }

        public PboClaimPayer Payer { get; set; }
        public Info ProviderInfo { get; set; }
        public Info Subscriber { get; set; }
        public Info Dependent { get; set; }
        public List<Claim> ClaimStatuses { get; set; }

        public string FileControlNumber { get; set; }
    }

    public class Claim
    {
       
        public List<SecInfo> OtherIndentificationInfo { get; set; }
        public StatusInfo StatusInfo { get; set; }
     
        public List<ServiceLine> ServiceLines { get; set; }
       
        public List<Date> Dates { get; set; }
    }

    public class StatusInfo //STC segment
    {
       
        public List<Status> Statuses { get; set; }
        public string EffectiveDate { get; set; }
        public decimal? ChrageAmount { get; set; }
        public decimal? PaymentAmount { get; set; }
        public string AdjudicationFinalizedDate { get; set; }
        public string RemittanceDate { get; set; }
        public string CheckNumber { get; set; }

    }

    public class ServiceLineStatusInfo //STC segment
    {
      
        public List<Status> Statuses { get; set; }
        public string EffectiveDate { get; set; }

    }

    public class Status
    {
        public string CategoryCode { get; set; }
        public string Category { get; set; }
        public string StatusCode { get; set; }
        public string ClaimStatus { get; set; }
        public string Type { get; set; }

    }


    public class Date
    {

        public string Qualifier { get; set; }

        public string Value { get; set; }
    }

    public class LineItemInfo
    {

        public string ProcedureCode { get; set; }

        public decimal? LineCharge { get; set; }

        public decimal? LinePayment { get; set; }

        public int? Unit { get; set; }
    }
    public class ServiceLine
    {
        public LineItemInfo LineItemInfo { get; set; }

      
        public ServiceLineStatusInfo StatusInfo { get; set; }
       
        public List<SecInfo> OtherIndentificationInfo { get; set; }
       
        public List<Date> Dates { get; set; }
    }


    public class PboClaimPayer
    {
        public Info Info { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
    public class Info
    {
        public string Type { get; set; }
        public string Name { get; set; }

        public string Qualifier { get; set; }
        public string ID { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }

    }
    public class ContactInfo
    {
        public string Name { get; set; }

        public string Qualifier { get; set; }

        public string Number { get; set; }

    }

    public class SecInfo
    {
        public string Qualifier { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
    }
}