using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shortex.BusinessLogic.Services.IServices;

namespace Shortex.Client.Pages
{
    public partial class Shortener
    {
        [Inject]
        private IShortUrlService _shortUrlService { get; set; }

        //[Inject]
        //private NavigationManager _navManager { get; set; }

        [Inject]
        private IJSRuntime _jSRuntime { get; set; }

        private string Url { get; set; } = string.Empty;

        private bool IsLoading { get; set; }
        private bool ToShortUrl { get; set; } = true;

        protected override void OnInitialized()
        {

        }

        private async Task OnLinkProceed()
        {
            ChangeLoadingState(true);

            if (ToShortUrl)
            {
                GenerateShortLink();
            }
            else
            {
                await ProceedLongLink();
            }

            ChangeLoadingState(false);
        }

        private void GenerateShortLink()
        {
            var response = _shortUrlService.CreateAsync(Url);
        }

        private async Task ProceedLongLink()
        {
            var result = await _shortUrlService.GetOriginalUrl(Url);
            if (!string.IsNullOrEmpty(result.LongUrl))
            {
                // _navManager.NavigateTo(result.LongUrl);
                await _jSRuntime.InvokeVoidAsync("open", result.LongUrl, "_blank");
            }
        }

        private void ChangeLoadingState(bool isActive)
        {
            IsLoading = isActive;
            StateHasChanged();
        }
    }
}
