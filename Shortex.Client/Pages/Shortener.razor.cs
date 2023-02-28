using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.Common;

namespace Shortex.Client.Pages
{
    public partial class Shortener
    {
        [Inject]
        private IShortUrlService _shortUrlService { get; set; }
        [Inject]
        private IJSRuntime _jSRuntime { get; set; }
        [Inject]
        private IMatToaster _toaster { get; set; }

        private string Url { get; set; } = string.Empty;

        private bool IsLoading { get; set; }
        private bool ToShortUrl { get; set; } = true;

        protected override void OnInitialized()
        {

        }

        private async Task OnLinkProceed()
        {
            ChangeLoadingState(true);

            var response = await _shortUrlService.ProcessLinkAsync(
                Url,
                ToShortUrl
                    ? LinkProcessType.Shortening
                    : LinkProcessType.Forwarding);

            if (response.Keys.First() == 200)
            {
                Toast("Redirection in 3 seconds...", MatToastType.Info, "Redirection", null);
                await Task.Delay(3000);

                var link = response.Values.First();
                await _jSRuntime.InvokeVoidAsync("open", link, "_blank");
            }
            else
            {
                // if (response.Keys.First() == 404 || response.Keys.First() == 302 || response.Keys.First() == 403)
                Toast(response.Values.First(), MatToastType.Warning, "Attention", null);
            }

            Url = string.Empty;

            ChangeLoadingState(false);
        }

        private async Task GenerateShortLink()
        {
            var result = _shortUrlService.CreateAsync(Url);
        }

        private async Task ProceedLongLink()
        {
            var result = await _shortUrlService.GetOriginalUrl(Url);
            if (!string.IsNullOrEmpty(result.LongUrl))
            {
                Toast("Redirection in 3 seconds...", MatToastType.Info, "Redirection", null);
                await Task.Delay(3000);

                // _navManager.NavigateTo(result.LongUrl);
                await _jSRuntime.InvokeVoidAsync("open", result.LongUrl, "_blank");
                Url = string.Empty;
            }
            else
            {
                Toast("Oops, something wend wrong", MatToastType.Danger, "Redirection", null);
            }
        }

        private void ChangeLoadingState(bool isActive)
        {
            IsLoading = isActive;
            StateHasChanged();
        }

        private void Toast(string text, MatToastType type, string? title, string? icon)
        {
            _toaster.Add(text, type, title, icon);
        }
    }
}
