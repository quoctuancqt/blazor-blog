using BlazorBlogApp2.Shared;
using Blazored.Storage;
using Microsoft.AspNetCore.Blazor.Services;
using Microsoft.JSInterop;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BlazorBlogApp2.Client
{
    public class AppState
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorage _localStorage;
        private readonly IUriHelper _uriHelper;

        public bool IsLoggedIn { get; set; }
        public bool InProcessing { get; set; }

        public AppState(HttpClient httpClient,
                        ILocalStorage localStorage,
                        IUriHelper uriHelper)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
            _uriHelper = uriHelper;
        }

        public async Task Login(Account loginDetails)
        {
            var response = await _httpClient.PostAsync("api/login", new StringContent(Json.Serialize(loginDetails), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                await SaveToken(response);

                await SetAuthorizationHeader();

                IsLoggedIn = true;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItem("authToken");

            _httpClient.DefaultRequestHeaders.Clear();

            IsLoggedIn = false;

            _uriHelper.NavigateTo("/login");
        }

        private async Task SaveToken(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            var jwt = Json.Deserialize<JwtToken>(responseContent);

            await _localStorage.SetItem("authToken", jwt.Token);
        }

        public async Task SetAuthorizationHeader()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = await _localStorage.GetItem<string>("authToken");

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
