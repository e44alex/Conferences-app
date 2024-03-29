﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Common.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages
{
    public class SearchModel : PageModel
    {
        private readonly IApiClient _apiClient;

        public string Term { get; set; }
        public List<SearchResult> SearchResults { get; set; }

        public SearchModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task OnGetAsync(string term)
        {
            Term = term;
            SearchResults = await _apiClient.SearchAsync(term);
        }


    }
}