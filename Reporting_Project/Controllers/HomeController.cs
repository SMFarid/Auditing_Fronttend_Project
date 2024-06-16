using Microsoft.AspNetCore.Mvc;
using Frontend_Project.Models;
using System.Diagnostics;
using Frontend_Project.ViewModel;
using Newtonsoft.Json;
using AutoMapper;
using System.Text;

namespace Frontend_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;
        private readonly IMapper mapper;
        Uri baseAddress = new Uri("https://localhost:7271/api/Auditing/");

        public HomeController(ILogger<HomeController> logger , IMapper _mapper)
        {
            _logger = logger;
            _client = new HttpClient();
            mapper = _mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SessionDetailDTO bigObj = new SessionDetailDTO();
            var auditorSession = new CommonResponse<List<AuditorGroupsDTO>>();
            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetAuditorGroups?auditor_ID=1").Result;
            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                auditorSession = JsonConvert.DeserializeObject<CommonResponse<List<AuditorGroupsDTO>>>(data);
            }
            bigObj.sessions = auditorSession.Data;
            bigObj.AuditorId = 2;
            return View(bigObj);
        }



        [HttpGet]
        public IActionResult getStudent([FromQuery]string roundCode)
        {
            SessionDetailDTO bigObj = new SessionDetailDTO();
            var auditorSession = new CommonResponse<AuditingSessionCriteraDTO>();
            auditorSession.Data = new AuditingSessionCriteraDTO();
            string data = "";
            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetCriteria?roundcode="+ roundCode).Result;
            if (response.IsSuccessStatusCode)
            {
                 data = response.Content.ReadAsStringAsync().Result;
                auditorSession = JsonConvert.DeserializeObject<CommonResponse<AuditingSessionCriteraDTO>>(data);
            }
            bigObj.StudentsAttendedList = auditorSession.Data.students;
            bigObj.reportStart = DateTime.Now;
           
            return PartialView("_studentsSession", bigObj);
        }

        [HttpPost]
        public IActionResult AuditingReport(SessionDetailDTO sessionDetailDTO)
        {
            var seesionReportPost = new SessionDetailPostDTO();
            seesionReportPost = mapper.Map<SessionDetailPostDTO>(sessionDetailDTO);
            seesionReportPost.ReportEnd = DateTime.Now;
            string seesionReportPostJson = JsonConvert.SerializeObject(seesionReportPost);

            var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response =  _client.PostAsync(baseAddress + "SaveAuditingReport", requestContent).Result;
            var responseSer = new CommonResponse<string>();
            
            if (response.IsSuccessStatusCode) 
            {
                ViewBag.suc = "Done"; 
                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<string>>(content);
            }
            else { ViewBag.fail = "fail"; }


            var auditorSession = new CommonResponse<List<AuditorGroupsDTO>>();
            HttpResponseMessage responseGet = _client.GetAsync(baseAddress + "GetAuditorGroups?auditor_ID=2").Result;
            if (responseGet.IsSuccessStatusCode)
            {
                string data = responseGet.Content.ReadAsStringAsync().Result;
                auditorSession = JsonConvert.DeserializeObject<CommonResponse<List<AuditorGroupsDTO>>>(data);
            }
            sessionDetailDTO.sessions = auditorSession.Data;
            sessionDetailDTO.AuditorId = 2;
            return View("Index", sessionDetailDTO/*, new { id = result }*/);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}