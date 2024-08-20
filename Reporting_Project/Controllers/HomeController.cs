using Microsoft.AspNetCore.Mvc;
using Frontend_Project.Models;
using System.Diagnostics;
using Frontend_Project.ViewModel;
using Newtonsoft.Json;
using AutoMapper;
using System.Text;
using Frontend_Project.Common;
using System.Net.Http.Headers;

namespace Frontend_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IMapper mapper;
        //Uri baseAddress = new Uri("http://10.0.27.100:80/AuditingBackend/api/Auditing/");
        Uri baseAddress = new Uri("https://localhost:44306/api/Auditing/");
        private readonly IHttpContextAccessor _contextAccessor;

        public HomeController(ILogger<HomeController> logger , IMapper _mapper, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _client = new HttpClient();
            mapper = _mapper;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        public IActionResult welcome()
        {
            //Check cookie
            //if (ResultLogin.username == "" || ResultLogin.auditorID == 0)
            if (_contextAccessor.HttpContext.Request.Cookies["username"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            //if(ResultLogin.username == "" || ResultLogin.auditorID ==0)
            if (_contextAccessor.HttpContext.Request.Cookies["username"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            SessionDetailDTO bigObj = new SessionDetailDTO();
            var auditorSession = new CommonResponse<List<AuditorGroupsDTO>>();
           
            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);

            

            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetAuditorGroups?auditor_ID="+ ResultLogin.auditorID).Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                auditorSession = JsonConvert.DeserializeObject<CommonResponse<List<AuditorGroupsDTO>>>(data);
            }
            bigObj.sessions = auditorSession.Data;
            bigObj.AuditorId = ResultLogin.auditorID;
            bigObj.reportStart = DateTime.Now;
            return View(bigObj);
        }

        [HttpGet]
        public IActionResult getStudent([FromQuery]string roundCode , [FromQuery] string Auditor_ID)
        {
            if (_contextAccessor.HttpContext.Request.Cookies["username"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            SessionDetailDTO bigObj = new SessionDetailDTO();
            var auditorSession = new CommonResponse<AuditingSessionCriteraDTO>();
            auditorSession.Data = new AuditingSessionCriteraDTO();
            string data = "";
            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetCriteria?roundcode="+ roundCode+ "&Auditor_ID="+ Auditor_ID).Result;
            if (response.IsSuccessStatusCode)
            {
                 data = response.Content.ReadAsStringAsync().Result;
                auditorSession = JsonConvert.DeserializeObject<CommonResponse<AuditingSessionCriteraDTO>>(data);
            }
            if(auditorSession.Data != null)
            {
                bigObj.StudentsAttendedList = auditorSession.Data.students;
                bigObj.auditing_Session_ID = auditorSession.Data.Auditing_Session_ID;
                bigObj.study_Group_ID = auditorSession.Data.Study_Group_ID;
                bigObj.NumberRegistered = auditorSession.Data.NumberRegistered;
                bigObj.TrainingProvider = auditorSession.Data.TrainingProvider;
                bigObj.InstructorList = auditorSession.Data.Instructors;
                bigObj.OtherInstructorName = auditorSession.Data.OtherInstructorName;
                bigObj.MeetingLink = auditorSession.Data.MeetingLink;
                bigObj.Track = auditorSession.Data.Track;
            }
           
           // bigObj.reportStart = DateTime.Now;
           
            return PartialView("_studentsSession", bigObj);
        }

        [HttpPost]
        public IActionResult AuditingReport(IFormCollection formItem, SessionDetailDTO sessionDetailDTO)
        {
            if (_contextAccessor.HttpContext.Request.Cookies["username"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var keySubmitpublish = formItem.Where(x=>x.Value == "btn_publish").FirstOrDefault();
            var keySubmitHold = formItem.Where(x=>x.Value == "btn_Hold").FirstOrDefault();
           // string keySubmit = formItem.T("" , );
           
            var seesionReportPost = new SessionDetailPostDTO();
            seesionReportPost = mapper.Map<SessionDetailPostDTO>(sessionDetailDTO);
            seesionReportPost.StudentsAttendedList = sessionDetailDTO.StudentsAttendedList.Where(x=>x.attend == true).ToList();
            seesionReportPost.ReportEnd = DateTime.Now;
            string seesionReportPostJson = JsonConvert.SerializeObject(seesionReportPost);

            var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            HttpResponseMessage response = null;
            if (keySubmitpublish.Value == "btn_publish")
            {
                 response = _client.PostAsync(baseAddress + "SaveAuditingReport", requestContent).Result;
            }
            else if (keySubmitHold.Value == "btn_Hold")
            {
                 response = _client.PostAsync(baseAddress + "HoldAuditingReport", requestContent).Result;
            }
            
            var responseSer = new CommonResponse<string>();


          //  SessionDetailDTO bigObj = new SessionDetailDTO();
          // var auditorSession = new CommonResponse<List<AuditorGroupsDTO>>();
            if (response.IsSuccessStatusCode)
            {

                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<string>>(content);
                if (resultCone.IsSuccess)
                {
                    TempData["suc"] = "Done";
                   // ViewBag.suc = "Done";

                }
                else {
                    TempData["fail"] = "fail";
                    //ViewBag.fail = "fail"; 
                }

            }
            else { TempData["fail"] = "fail"; }
            //HttpResponseMessage responseGet = _client.GetAsync(baseAddress + "GetAuditorGroups?auditor_ID=1").Result;
            //if (responseGet.IsSuccessStatusCode)
            //{
            //    string data = responseGet.Content.ReadAsStringAsync().Result;
            //    auditorSession = JsonConvert.DeserializeObject<CommonResponse<List<AuditorGroupsDTO>>>(data);
            //    if (auditorSession.Data != null)
            //    {

            //        bigObj.sessions = auditorSession.Data;
            //        bigObj.AuditorId = ResultLogin.auditorID;
            //    }
            //}


            return RedirectToAction("Index"/*, new { id = result }*/);
        }

        [HttpPost]
        public IActionResult HoldAuditingReport(SessionDetailDTO sessionDetailDTO)
        {
            if (_contextAccessor.HttpContext.Request.Cookies["username"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            var seesionReportPost = new SessionDetailPostDTO();
            seesionReportPost = mapper.Map<SessionDetailPostDTO>(sessionDetailDTO);
            seesionReportPost.StudentsAttendedList = sessionDetailDTO.StudentsAttendedList.Where(x => x.attend == true).ToList();
            seesionReportPost.ReportEnd = DateTime.Now;
            string seesionReportPostJson = JsonConvert.SerializeObject(seesionReportPost);

            var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);


            HttpResponseMessage response = _client.PostAsync(baseAddress + "HoldAuditingReport", requestContent).Result;
            var responseSer = new CommonResponse<string>();


            //  SessionDetailDTO bigObj = new SessionDetailDTO();
            // var auditorSession = new CommonResponse<List<AuditorGroupsDTO>>();
            if (response.IsSuccessStatusCode)
            {

                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<string>>(content);
                if (resultCone.IsSuccess)
                {
                    TempData["suc"] = "Done";
                    // ViewBag.suc = "Done";

                }
                else
                {
                    TempData["fail"] = "fail";
                    //ViewBag.fail = "fail"; 
                }

            }
            else { TempData["fail"] = "fail"; }


            return RedirectToAction("Index"/*, new { id = result }*/);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}