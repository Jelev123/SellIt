namespace SellIt.Core.Services.Adress
{
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using SellIt.Core.Contracts.Adress;
    using SellIt.Core.ViewModels.Adress;
    using System.Threading.Tasks;

    public class AdressService : IAdressService
    {
        private readonly HttpClient _httpClient;

        public AdressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  Task  GetGeoInfo()
        {
            var ipAddress = GetIPAddress();
            var response =  _httpClient.GetAsync($"http://api.ipstack.com/" + ipAddress + "?access_key=942bef5ae748409e6c20a78166afae23");
            if (response.IsCompletedSuccessfully)
            {
                var json =  response.Result.Content.ReadAsStringAsync();
                var model = new GeoInfoViewModel();
                model = JsonConvert.DeserializeObject<GeoInfoViewModel>(json.ToString());
            }
            return null;
        }

        public async Task<string> GetIPAddress()
        {
            var ipAddress = await _httpClient.GetAsync($"http://ipinfo.io/ip");
            if (ipAddress.IsSuccessStatusCode)
            {
                var json =  ipAddress.Content.ReadAsStringAsync();
                return json.ToString();
            }

            return null;
           
        }
    }
}
