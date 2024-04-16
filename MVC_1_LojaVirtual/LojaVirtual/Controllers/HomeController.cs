using Humanizer;
using Infra.Data.Domain.Dtos;
using Infra.Data.Domain.Interfaces;
using Infra.Data.Domain.Models;
using LojaServices_Application.Repositories;
using LojaVirtual.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using NuGet.Common;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace LojaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioDtoMethods usuarioDtoMethods;
        public HomeController(ILogger<HomeController> logger, IUsuarioDtoMethods usuarioDtoMethods)
        {
            _logger = logger;
            this.usuarioDtoMethods = usuarioDtoMethods;
        }

       public IActionResult Privacy()
        {
            return View();
        }

        //Metodos de cadastro de Usuario
        public IActionResult SignUp()
        {
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UsuarioDto usuarioDto)
        {
            if(ModelState.IsValid) {
                var CheckName = await usuarioDtoMethods.CheckAvailbilityOfName(usuarioDto.NomeUsuario!);
                if (!CheckName)
                    return BadRequest("Digite outro nome.\nEste já é utilizado por outro usuario.");

                var CheckEmail = await usuarioDtoMethods.CheckAvailbilityOfEmail(usuarioDto.EmailUsuario!);
                if (!CheckEmail)
                    return BadRequest("Email já foi utilizado por outro usuario.");
                await usuarioDtoMethods.cadastroDeUsuario(usuarioDto);

                return RedirectToAction("Index");
            }
            
                return View();
           
        }

        //Metodo Página inicial
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost] Metodos de Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LogginUsuario login)
        {
            /*var Http = new HttpClient();
            Http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(Token);
            //Autoriza o Usuario através do jWT*/
            if (ModelState.IsValid)
            {
                var isTrueOrNot= await usuarioDtoMethods.Login(login);
                if(isTrueOrNot!=null)
                    return RedirectToAction("Index");
            }
            
            return View();
        }
        

        //Metodo Padrão
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
