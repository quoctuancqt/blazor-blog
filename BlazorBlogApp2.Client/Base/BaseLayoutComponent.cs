using System;
using System.Threading.Tasks;
using Blazored.Storage;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Layouts;
using Microsoft.AspNetCore.Blazor.Services;
using Toolbelt.Blazor;

namespace BlazorBlogApp2.Client
{
    public abstract class BaseLayoutComponent : BlazorLayoutComponent
    {
        [Inject]
        private IUriHelper _uriHelper { get; set; }

        [Inject]
        private ILocalStorage _localStorage { get; set; }

        [Inject]
        private AppState _appState { get; set; }

        [Inject]
        private HttpClientInterceptor _interceptor { get; set; }

        protected Task _callbackAsync { get; set; }

        protected Action _callback { get; set; }

        protected override async Task OnInitAsync()
        {
            if (!string.IsNullOrEmpty(await _localStorage.GetItem<string>("authToken")))
            {
                _appState.IsLoggedIn = true;

                await _appState.SetAuthorizationHeader();

                if (_callback != null) _callback.Invoke();

                if (_callbackAsync != null) await _callbackAsync;

                _interceptor.BeforeSend += Interceptor_BeforeSend;

                _interceptor.AfterSend += Interceptor_AfterSend;
            }
            else
            {
                _uriHelper.NavigateTo("/login");
            }
        }

        private void Interceptor_BeforeSend(object sender, EventArgs e)
        {
            _appState.InProcessing = true;

            StateHasChanged();
        }

        private void Interceptor_AfterSend(object sender, EventArgs e)
        {
            _appState.InProcessing = false;

            StateHasChanged();
        }
    }
}
