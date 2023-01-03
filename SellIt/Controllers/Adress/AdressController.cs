namespace SellIt.Controllers.Adress
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SellIt.Core.Contracts.Adress;
    using SellIt.Core.ViewModels.Adress;

    public class AdressController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly IAdressService adressService;

        public AdressController(HttpClient httpClient, IAdressService adressService)
        {
            _httpClient = httpClient;
            this.adressService = adressService;
        }


        //public async Task<IActionResult> GeoInfoProvider()
        //{

        //    var _httpClient = new HttpClient()
        //    {
        //        Timeout = TimeSpan.FromSeconds(5)
        //    };

        //}
        private async Task<string> GetIPAddress()
        {
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json = await ipAddress.Content.ReadAsStringAsync();
                return json.ToString();
            }
            return "";
        }

        public async Task<IActionResult> GetGeoInfo()
        {  
            var ipAddress = await GetIPAddress();
            var response = await _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=4aa9e127dc82475f6204f67c96f3cf53");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var model = new GeoInfoViewModel();
                model = JsonConvert.DeserializeObject<GeoInfoViewModel>(json);
                return this.View(model);
            }
            return null;
        }
    }
}

