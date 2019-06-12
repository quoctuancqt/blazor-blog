using System.Threading.Tasks;
using BlazorBlogApp2.Shared;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;

namespace BlazorBlogApp2.Client.Pages
{
    public class LoginModel : BlazorComponent
    {
        [Inject] private AppState _appState { get; set; }

        [Inject] private IUriHelper _uriHelper { get; set; }

        protected Account LoginDetails { get; set; } = new Account();

        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {
            await _appState.Login(LoginDetails);

            if (_appState.IsLoggedIn)
            {
                _uriHelper.NavigateTo("/");
            }
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}