using ClientApp.Logger;
using ClientApp.Models;
using ClientApp.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoggerService _logger;
        private readonly ITokenService _tokenService;
        private readonly ApiScopeConfigurationModel _apiScopeConfiguration;
        private readonly HostApiConfigurationModel _hostApiConfiguration;
        public HomeController(ILoggerService logger, ITokenService tokenService, IOptions<ApiScopeConfigurationModel> apiScopeConfiguration, IOptions<HostApiConfigurationModel> hostApiConfiguration)
        {
            _logger = logger;
            _tokenService = tokenService;
            _apiScopeConfiguration = apiScopeConfiguration.Value;
            _hostApiConfiguration = hostApiConfiguration.Value;
        }

        public async Task<IActionResult> Weather()
        {
            _logger.LogInfo("Get Read Token");
            var token = await _tokenService.GetToken(_apiScopeConfiguration.read);
            using (var client = new HttpClient())
            {
                client.SetBearerToken(token.AccessToken);
                var result = await client.GetAsync(_hostApiConfiguration.Api);
                if (result.IsSuccessStatusCode)
                {
                    var model = await result.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<List<WeatherModel>>(model);
                    return View(data);
                }
                else
                {
                    _logger.LogInfo("Failed to get Data from API");
                    throw new Exception("Failed to get Data from API");
                }
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
