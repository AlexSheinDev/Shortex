using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Shortex.BusinessLogic.Services.IServices;
using Shortex.Common;
using Shortex.Common.Models.DTO;

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
        private IEnumerable<ShortUrlDTO>? ShortUrls { get; set; }

        private bool IsLoading { get; set; }
        private bool ToShortUrl { get; set; } = true;

        protected override void OnInitialized()
        {
            ShortUrls = _shortUrlService.GetAll();
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
                UpdateShortUrls();

                if (ToShortUrl)
                {
                    Toast(response.Values.First(), MatToastType.Success, "Success", null);
                }
                else
                {
                    Toast("Redirection in 3 seconds...", MatToastType.Info, "Redirection", null);
                    await Task.Delay(3000);

                    await _jSRuntime.InvokeVoidAsync("open", response.Values.First(), "_blank");
                }
            }
            else
            {
                // if (response.Keys.First() == 404 || response.Keys.First() == 302 || response.Keys.First() == 403)
                Toast(response.Values.First(), MatToastType.Warning, "Attention", null);
            }

            Url = string.Empty;

            ChangeLoadingState(false);
        }

        private async Task OnClearHistory()
        {
            ChangeLoadingState(true);

            var result = await _shortUrlService.ClearHistoryAsync();

            if (result == 0)
            {
                Toast("Oops. Something went wrong...", MatToastType.Danger, "Error", null);
            }
            else
            {
                Toast($"Records were successfully deleted: {result}.", MatToastType.Success, "Success", null);
                ShortUrls = null;
            }

            ChangeLoadingState(false);
        }

        private void ChangeLoadingState(bool isActive)
        {
            IsLoading = isActive;
            StateHasChanged();
        }

        private void UpdateShortUrls()
        {
            ShortUrls = _shortUrlService.GetAll();
            StateHasChanged();
        }

        private void Toast(string text, MatToastType type, string? title, string? icon)
        {
            _toaster.Add(text, type, title, icon);
        }
    }
}
