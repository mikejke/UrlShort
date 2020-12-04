using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace UrlShort.Pages
{
    public partial class RedirectionPage
    {
        [Parameter] 
        public string ShortUrl { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await using var context = DbFactory.CreateDbContext();
            var url = await context.ShortUrls.FirstOrDefaultAsync(u => u.Short == ShortUrl);

            if (url != null)
            {
                url.Clicks++;
                context.Entry(url).State = EntityState.Modified;
                await context.SaveChangesAsync();
                NavigationManager.NavigateTo(new[] {"http", "://"}.Any(s => url.Full.Contains(s)) ? url.Full : $"http://{url.Full}");
            }
            else
            {
                NavigationManager.NavigateTo("/");
            }
        }
    }
}