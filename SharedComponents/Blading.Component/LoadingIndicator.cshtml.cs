using Microsoft.AspNetCore.Blazor.Components;

namespace Blading.Component
{
    public class LoadingIndicatorModel : BlazorComponent
    {
        [Parameter]
        protected bool IsShow { get; set; }

        public string HiddenClass => IsShow ? "hidden" : "";
    }
}