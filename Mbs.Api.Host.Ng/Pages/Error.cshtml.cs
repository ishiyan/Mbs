using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable SA1649 // File name should match first type name

namespace Mbs.Api.Host.Ng.Pages
{
    /// <inheritdoc />
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        // ReSharper disable once MemberCanBePrivate.Global
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
        }
    }
}
