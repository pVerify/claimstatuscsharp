using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestAPICSharp.Models;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;
using System.Net;

namespace RestAPICSharp.Controllers
{
    public class RestAPIController : Controller
    {
        // GET: RestAPI
        public ActionResult Index()
        {
            return View();
        }

        #region Token
        public ActionResult Token()
        {
            TokenModel token = new TokenModel();
            token.Token = "";
            token.ApiResponse = "";
            token.UserName = "pverify_demo";
            token.Password = "pverify@949";
         
            return View(token);
        }

        [HttpPost]
         public ActionResult Token(TokenModel model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/Token", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/x-www-form-urlencoded");

            request.AddParameter("application/x-www-form-urlencoded", "username=" + model.UserName + "&password=" + model.Password + "&grant_type=password", ParameterType.RequestBody);
           
            request.RequestFormat = DataFormat.Json;

            RestClient client = new RestClient(apiBaseURL);
            // client.Timeout = 3 * 60 * 1000;//3 minutes

            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;
            if(response.StatusCode==HttpStatusCode.OK)
            {
                dynamic r = JsonConvert.DeserializeObject(response.Content);
                model.Token = r.access_token;
                Session["accessToken"] = model.Token;
                Session["userName"] = model.UserName;
                Session["password"] = model.Password;
            }
           
         
            return View(model);
        }

        #endregion

      


        #region Inquiry
        public ActionResult Self()
        {
            PboRequest model = new PboRequest();
            PboClaimStatusRequest request = new PboClaimStatusRequest();
            request.Subscriber = new PboClaimRequestSubscriber();
         
            request.ServiceStartDate = DateTime.Now.ToString("MM/dd/yyyy");
            request.ServiceEndDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.Request = request;
            model.Response = new APIResponse();
            model.Response.Token = GetToken();
            model.Response. ClientUserName = GetUserName();
           
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Self(PboRequest model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/ClaimStatusInquiry", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + model.Response.Token);
            request.AddHeader("Client-User-Name", model.Response.ClientUserName);
          
            request.RequestFormat = DataFormat.Json;

         
            model.Request.IsPatientDependent=false;
            model.Request.RequestSource = "API";
            request.AddBody(model.Request);

            model.Response.ApiRequest = JsonConvert.SerializeObject(model.Request);
            model.Response.ApiRequest = model.Response.ApiRequest.Trim();
       
            RestClient client = new RestClient(apiBaseURL);
          //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.Response.StatusCode = response.StatusCode;
            model.Response.ApiResponse = response.Content;

            return View(model);
        }

      
        public ActionResult Dependent()
        {
            PboRequest model = new PboRequest();
            PboClaimStatusRequest request = new PboClaimStatusRequest();
            request.Subscriber = new PboClaimRequestSubscriber();
            request.Dependent = new Dependent();
        
            request.ServiceStartDate = DateTime.Now.ToString("MM/dd/yyyy");
            request.ServiceEndDate = DateTime.Now.ToString("MM/dd/yyyy");
            model.Request = request;
            model.Response = new APIResponse();
            model.Response.Token = GetToken();
            model.Response. ClientUserName = GetUserName();
          
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Dependent(PboRequest model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/ClaimStatusInquiry", Method.POST);
            //add headers
            request.AddHeader("content-type", "application/json");
            request.AddHeader("Authorization", "Bearer " + model.Response.Token);
            request.AddHeader("Client-User-Name", model.Response.ClientUserName);
          
            request.RequestFormat = DataFormat.Json;

            model.Request.IsPatientDependent = true;
            model.Request.RequestSource = "API";
            request.AddBody(model.Request);

            model.Response.ApiRequest = JsonConvert.SerializeObject(model.Request);

            model.Response.ApiRequest = model.Response.ApiRequest.Trim();

            RestClient client = new RestClient(apiBaseURL);
            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.Response.StatusCode = response.StatusCode;
            model.Response.ApiResponse = response.Content;

            return View(model);

        }

        #endregion



        #region Get Response API/GetEligibilityResponse/{id}

        public ActionResult GetResponse()
        {
            ClaimResponse model = new ClaimResponse();
            model.Token = GetToken();
            model.ClientUserName = GetUserName();
            model.ClientPassword = GetPassword();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetResponse(ClaimResponse model)
        {
            string apiBaseURL = ConfigurationManager.AppSettings["RestAPIURL"];
            var request = new RestRequest("/API/GetClaimStatusResponse/" + model.RequestId, Method.GET);
            //add headers
            request.AddHeader("Authorization", "Bearer " + model.Token);
            request.AddHeader("Client-User-Name", model.ClientUserName);
           
            //
            RestClient client = new RestClient(apiBaseURL);
            //execute the request using rest client object
            IRestResponse response = client.Execute(request);
            model.StatusCode = response.StatusCode;
            model.ApiResponse = response.Content;

            return View(model);

        }

      
        #endregion

  

        #region NON Actions
        [NonAction]
        private string GetToken()
        {
            if(Session["accessToken"] !=null)
            {
                return Session["accessToken"].ToString();
            }

            return "";
        }
        [NonAction]
        private string GetUserName()
        {
            if (Session["userName"] != null)
            {
                return Session["userName"].ToString();
            }

            return "";
        }

        [NonAction]
        private string GetPassword()
        {
            if (Session["password"] != null)
            {
                return Session["password"].ToString();
            }

            return "";
        }
#endregion

    }
}