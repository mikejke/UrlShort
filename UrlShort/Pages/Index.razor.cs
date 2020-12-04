using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShort.Domain;

namespace UrlShort.Pages
{
    public partial class Index
    {
        private List<ShortUrl> _shortUrls;
        
        private string _url;

        protected override async Task OnInitializedAsync()
        {
            this._shortUrls = new List<ShortUrl>();
        }

        private void ShortUrl()
        {
            var shortUrl = new ShortUrl() {Full = _url};
            _shortUrls.Add(shortUrl);
        }
    }
}