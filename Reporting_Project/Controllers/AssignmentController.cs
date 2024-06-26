using AutoMapper;
using Frontend_Project.Common;
using Frontend_Project.ViewModel;
using Frontend_Project.ViewModel.Assignment;
using Frontend_Project.ViewModel.Auditing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Frontend_Project.Controllers
{
    public class AssignmentController : Controller
    {
        private readonly ILogger<AssignmentController> _logger;
        private readonly HttpClient _client;
        private readonly IMapper mapper;
        Uri baseAddress = new Uri("https://localhost:44306/api/Auditing/");

        //Uri baseAddress = new Uri("https://localhost:44306/api/Auditing/");
        public static List<AuditorRoundCodeAssignmentVM> roundCodeList = new List<AuditorRoundCodeAssignmentVM>();
         //{
         //       new AuditorRoundCodeAssignmentVM() { AuditingSessionId = 2, StudyGroupRoundCode = "CAI1_ISS7_G1e" , AuditorId = 1 , Conducted = true , Date = DateTime.Parse("2024-06-13")},
         //       new AuditorRoundCodeAssignmentVM() { AuditingSessionId = 0, StudyGroupRoundCode = "CAI1_SWD5_S13d" , AuditorId = 1 , Conducted = true , Date = DateTime.Parse("2024-06-19")},
         //       new AuditorRoundCodeAssignmentVM() { AuditingSessionId = 0, StudyGroupRoundCode = "ONL1_DAT2_G9e" , AuditorId = 1 , Conducted = true , Date = DateTime.Parse("2024-06-19")}
         //};
        //public static List<AuditorListVM> AuditorList = new List<AuditorListVM>
        //{
        //    new AuditorListVM{AuditorID = 1,NameAr="Admin" , NameEn="Admin" },
        //    new AuditorListVM{AuditorID = 3,NameEn="Mostafa Hatem Awam Mohamed"        , NameAr="مصطفى حاتم عوام محمد" },
        //    new AuditorListVM{AuditorID = 4,NameEn="Amira Elsayed Ramadan Soliman"     , NameAr="اميرة السيد رمضان سليمان" },
        //    new AuditorListVM{AuditorID = 5,NameEn="Nada Ali Abd-Elnabe Mahmoud"       , NameAr="ندا على عبدالنبي محمود" },
        //    new AuditorListVM{AuditorID = 6,NameEn="Saied Mahdy Saied Abdelazez"       , NameAr="سعيد مهدي سعيد عبدالعزيز" },
        //    new AuditorListVM{AuditorID = 7,NameEn="Rana Hesham Taha sayed"            , NameAr="رنا هشام طه سيد" },
        //    new AuditorListVM{AuditorID = 8,NameEn="Mohamed Badawy Shenawy Badawy"     , NameAr="محمد بدوي شناوي بدوي" },
        //    new AuditorListVM{AuditorID = 9,NameEn="Mohamed Turkey Mahmoud Abdelmwgoud", NameAr="محمد تركي محمود عبدالموجود" }
        //};
    public AssignmentController(ILogger<AssignmentController> logger, IMapper _mapper)
        {
            _logger = logger;
            _client = new HttpClient();
            mapper = _mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {


            if (ResultLogin.username == "" || ResultLogin.auditorID == 0)
            {
                return RedirectToAction("Login", "Authentication");
            }

            RoundCodeAssignmentIndexVM roundCodeIndex = new RoundCodeAssignmentIndexVM();
            var RoundCodeAssignment = new CommonResponse<List<AuditorRoundCodeAssignmentVM>>();

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);



            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetRoundCodesForEdit").Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                RoundCodeAssignment = JsonConvert.DeserializeObject<CommonResponse<List<AuditorRoundCodeAssignmentVM>>>(data);
                if (RoundCodeAssignment.Data != null)
                {
                    roundCodeIndex.roundCodeAssignmentsVM = RoundCodeAssignment.Data;
                    roundCodeList = RoundCodeAssignment.Data;
                }
            }
            return View(roundCodeIndex);












            //roundCodeIndex.roundCodeAssignmentsVM = roundCodeList;
            //return View(roundCodeIndex);
        }
        [HttpGet]
        public IActionResult Add([FromQuery]  int AuditingSessionId , [FromQuery] string roundCode)
        {
            RoundCodeAssignmentIndexVM roundCodeAssignmentIndex = new RoundCodeAssignmentIndexVM();


            if (ResultLogin.username == "" || ResultLogin.auditorID == 0)
            {
                return RedirectToAction("Login", "Authentication");
            }


            var AuditorsList = new CommonResponse<List<AuditorListVM>>();

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);


            HttpResponseMessage response = _client.GetAsync(baseAddress + "GetAuditorsList").Result;


            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                AuditorsList = JsonConvert.DeserializeObject<CommonResponse<List<AuditorListVM>>>(data);
                if (AuditorsList.Data != null)
                {
                    roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.auditors = AuditorsList.Data;
                }
            }












            if (AuditingSessionId == 0)
            {
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.RoundCode = roundCode;
                return PartialView("_FormAddAssignment", roundCodeAssignmentIndex);

            }
            else
            {
                var x = roundCodeList.FirstOrDefault(x => x.AssignmentID == AuditingSessionId);
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.AssignmentID = AuditingSessionId;
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.AuditorID = x.AuditorID;
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.Conducted = x.Conducted;
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.AssignmentDate = x.AssignmentDate;
                roundCodeAssignmentIndex.EditRoundCodeAssignmentVM.RoundCode = x.RoundCode;
                };
                return PartialView("_FormEditAssignment", roundCodeAssignmentIndex);
            }
            
        

        [HttpPost]
        public IActionResult Add(RoundCodeAssignmentIndexVM add)
        {




            if (ResultLogin.username == "" || ResultLogin.auditorID == 0)
            {
                return RedirectToAction("Login", "Authentication");
            }

            RoundCodeAssignmentIndexVM roundCodeIndex = new RoundCodeAssignmentIndexVM();
            var RoundCodeAssignment = new CommonResponse<List<AuditorRoundCodeAssignmentVM>>();

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);



            var requestbody = mapper.Map<AuditorRoundCodeAssignmentVM>(add.EditRoundCodeAssignmentVM);

            string seesionReportPostJson = JsonConvert.SerializeObject(requestbody);

            var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");


            HttpResponseMessage response = _client.PostAsync(baseAddress + "AddRoundCodeAssignment ", requestContent).Result;
            var responseSer = new CommonResponse<string>();

            if (response.IsSuccessStatusCode)
            {
                
                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<string>>(content);
                if (resultCone.IsSuccess)
                {
                    ViewBag.suc = "Done";
                }
                else
                {
                    ViewBag.fail = "fail";
                }
            }
            else { ViewBag.fail = "fail"; }




           // roundCodeList.Add(new AuditorRoundCodeAssignmentVM { AuditingSessionId = 4, AuditorId = add.EditRoundCodeAssignmentVM.AuditorId, StudyGroupRoundCode = add.EditRoundCodeAssignmentVM.StudyGroupRoundCode, Conducted = add.EditRoundCodeAssignmentVM.Conducted, Date = add.EditRoundCodeAssignmentVM.Date });
          //  RoundCodeAssignmentIndexVM roundCodeIndex = new RoundCodeAssignmentIndexVM();
            roundCodeIndex.roundCodeAssignmentsVM = roundCodeList;
            return RedirectToAction("index");

        }
        [HttpPost]
        public IActionResult Edit(RoundCodeAssignmentIndexVM add)
        {
            if (ResultLogin.username == "" || ResultLogin.auditorID == 0)
            {
                return RedirectToAction("Login", "Authentication");
            }

            RoundCodeAssignmentIndexVM roundCodeIndex = new RoundCodeAssignmentIndexVM();
            var RoundCodeAssignment = new CommonResponse<List<AuditorRoundCodeAssignmentVM>>();

            var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);



            var requestbody = mapper.Map<AuditorRoundCodeAssignmentVM>(add.EditRoundCodeAssignmentVM);

            string seesionReportPostJson = JsonConvert.SerializeObject(requestbody);

            var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");


            HttpResponseMessage response = _client.PostAsync(baseAddress + "EditRoundCodeAssignment ", requestContent).Result;
            var responseSer = new CommonResponse<string>();

            if (response.IsSuccessStatusCode)
            {

                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<string>>(content);
                if (resultCone.IsSuccess)
                {
                    ViewBag.suc = "Done";
                }
                else
                {
                    ViewBag.fail = "fail";
                }
            }
            else { ViewBag.fail = "fail"; }
            //roundCodeIndex.roundCodeAssignmentsVM = roundCodeList;
            return RedirectToAction("index" );

        }


        [HttpPost]
        public string delete([FromQuery] int AuditingSessionId)
        {
          
            var item = roundCodeList.FirstOrDefault(x => x.AssignmentID == AuditingSessionId);
            if(item == null)
            {
                return "الكود غير موجود لحذفه";
            }
            else
            {
                var authenticationString = $"{ResultLogin.username}:{ResultLogin.password}";
                var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);



                string seesionReportPostJson = JsonConvert.SerializeObject(item);

                var requestContent = new StringContent(seesionReportPostJson, Encoding.UTF8, "application/json");


                HttpResponseMessage response = _client.PostAsync(baseAddress + "DeleteRoundCodeAssignment ", requestContent).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                var msg= JsonConvert.DeserializeObject<CommonResponse<string>>(data);
                    if (msg.Data == "Success")
                    {
                        return "delete round cofe done";
                    }else
                    {
                        return "sorry, ";
                }
                }
                else
                {
                    return "sorry, ";
                }
                return "";
            }
        }
    
    }
}
