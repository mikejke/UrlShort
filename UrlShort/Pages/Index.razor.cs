using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using UrlShort.Domain;

namespace UrlShort.Pages
{
    public partial class Index
    {
        private List<ShortUrl> _shortUrls = new List<ShortUrl>();
        
        private string _url;

        protected override async Task OnInitializedAsync()
        {
            await using var context = DbFactory.CreateDbContext();
            _shortUrls = await context.ShortUrls.ToListAsync();
        }

        private async Task ShortUrl()
        {
            var shortUrl = new ShortUrl() { Full = _url};
            _shortUrls.Add(shortUrl);
            await using var context = DbFactory.CreateDbContext();
            await context.ShortUrls.AddAsync(shortUrl);
            await context.SaveChangesAsync();
        }
    }
}