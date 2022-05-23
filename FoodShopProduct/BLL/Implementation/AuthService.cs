using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using Newtonsoft.Json;

namespace BLL.Implementation
{
    public class AuthService : IAuthService
    {
        public async Task<bool> IsTokenValid(string token)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(
                new JwtModel
                {
                    Token = token
                });
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response =
                await httpClient.PostAsync("https://localhost:44303/api/Authorization/Validate", httpContent);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }

    public class JwtModel
    {
        public string Token { get; set; }
    }
}