using AutoMapper;
using Frontend_Project.Common;
using Frontend_Project.ViewModel;
using Frontend_Project.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Newtonsoft.Json;
using System.Text;

namespace Frontend_Project.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _client;
        private readonly IMapper mapper;
        private readonly ILogger<AuthenticationController> logger;
        Uri baseAddress = new Uri("https://localhost:44306/api/Authorize/");
        public AuthenticationController(ILogger<AuthenticationController> _logger, IMapper _mapper)
        {
            logger = _logger;
            _client = new HttpClient();
            mapper = _mapper;
        }
        [HttpGet] 
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {

            string loginPostJson = JsonConvert.SerializeObject(login);
            var requestContent = new StringContent(loginPostJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _client.PostAsync(baseAddress + "Login", requestContent).Result;

            var responseSer = new CommonResponse<LoginResposeDto>();

            if (response.IsSuccessStatusCode)
            {
                ViewBag.suc = "Done";
                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<LoginResposeDto>>(content);
                if(resultCone.IsSuccess)
                {
                    ResultLogin.username = resultCone.Data.username;
                    ResultLogin.password = resultCone.Data.password;
                    ResultLogin.email = resultCone.Data.email;
                    ResultLogin.auditorID = resultCone.Data.auditorID;
                    ResultLogin.nameAr = resultCone.Data.nameAr;
                    ResultLogin.nameEn = resultCone.Data.nameEn;
                    //return RedirectToAction("Index", "Home", new { id = 99 });
                    return RedirectToAction("welcome", "Home");

                }
                else
                {
                    ViewBag.fail = "fail";
                    ResultLogin.username = "";
                    ResultLogin.password = "";
                    ResultLogin.email = "";
                    ResultLogin.auditorID = 0;
                    ResultLogin.nameAr = "";
                    ResultLogin.nameEn = "";
                    return View();
                }
               
              
            }
            else {
                ViewBag.fail = "fail";
                ResultLogin.username = "";
                ResultLogin.password = "";
                ResultLogin.email = "";
                ResultLogin.auditorID = 0;
                ResultLogin.nameAr = "";
                ResultLogin.nameEn = "";
                return View();
            }

        }
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }
    }
}
