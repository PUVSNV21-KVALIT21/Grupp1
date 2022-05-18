using hakims_livs.Models;
using Newtonsoft.Json;
using RestSharp;

namespace hakims_livs.Services
{
    public class ApiService
    {
        public async Task CreatePickingList(string url, int id)
        {
            using (var client = new RestClient())
            {
                var request = new RestRequest(url + id);
                var response = await client.ExecuteAsync(request);
                var order = JsonConvert.DeserializeObject<Order>(response.Content);
                
            }

        }
    }
}
