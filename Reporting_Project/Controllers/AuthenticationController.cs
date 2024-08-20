using AutoMapper;
using Frontend_Project.Common;
using Frontend_Project.ViewModel;
using Frontend_Project.ViewModel.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Globalization;
using System.Text;
using System.Web;

namespace Frontend_Project.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly HttpClient _client;
        private readonly IMapper mapper;
        private readonly ILogger<AuthenticationController> logger;
        private readonly IHttpContextAccessor _httpContextAccessor ;
        //Uri baseAddress = new Uri("http://10.0.27.100:80/AuditingBackend/api/Authorize/");
        //Uri baseAddress = new Uri("https://localhost:80/api/Authorize/");
        Uri baseAddress = new Uri("https://localhost:44306/api/Authorize/");
        public AuthenticationController(ILogger<AuthenticationController> _logger, IMapper _mapper, IHttpContextAccessor httpContextAccessor)
        {
            logger = _logger;
            _client = new HttpClient();
            mapper = _mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet] 
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginDto login)
        {
            try
            {

            
            string loginPostJson = JsonConvert.SerializeObject(login);
            var requestContent = new StringContent(loginPostJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response =  _client.PostAsync(baseAddress + "Login", requestContent).Result;

            var responseSer = new CommonResponse<LoginResposeDto>();

            if (response.IsSuccessStatusCode)
            {
                    HttpContext.Response.Cookies.Append("User", "test");
                ViewBag.suc = "Done";
                string content = "";
                content = response.Content.ReadAsStringAsync().Result;
                var resultCone = JsonConvert.DeserializeObject<CommonResponse<LoginResposeDto>>(content);
                if(resultCone.IsSuccess)
                {
                    if(resultCone.Data != null && resultCone.Data.username != null && resultCone.Data.username != "")
                    {
                        ResultLogin.username = resultCone.Data.username;
                        ResultLogin.password = resultCone.Data.password;
                        ResultLogin.email = resultCone.Data.email;
                        ResultLogin.auditorID = resultCone.Data.auditorID;
                        ResultLogin.nameAr = resultCone.Data.nameAr;
                        ResultLogin.nameEn = resultCone.Data.nameEn;
                        ResultLogin.role = resultCone.Data.role;
                            //return RedirectToAction("Index", "Home", new { id = 99 });

                            // set cookies
                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddDays(1);

                            //options.Expires = DateTime.Now.AddSeconds(60); 
                            _httpContextAccessor.HttpContext.Response.Cookies.Append("username", ResultLogin.username, options);
                            _httpContextAccessor.HttpContext.Response.Cookies.Append("nameEn", ResultLogin.nameEn, options);
                            _httpContextAccessor.HttpContext.Response.Cookies.Append("role", ResultLogin.role.ToString(), options);
                            return RedirectToAction("welcome", "Home");
                    }
                    

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
                    return View("Login");
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
                return View("Login");
            }
            }
            catch(Exception ex)
            {
                ViewBag.fail = "fail";
            }
          
            return View("Login");

        }

        public IActionResult test()
        {
            return View("testV2");
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Logout()
        {
            ResultLogin.username = "";
            ResultLogin.password = "";
            ResultLogin.email = "";
            ResultLogin.auditorID = 0;
            ResultLogin.nameAr = "";
            ResultLogin.nameEn = "";
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("nameEn");
            Response.Cookies.Delete("role");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("username");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("nameEn");
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("role");
            return RedirectToAction("login", "Authentication");
        }
    }
}
