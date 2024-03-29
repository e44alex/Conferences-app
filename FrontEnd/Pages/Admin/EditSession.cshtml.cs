﻿using System.Threading.Tasks;
using Backend.Common.DTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontEnd.Pages.Admin
{
    public class EditSessionModel : PageModel
    {
        private readonly IApiClient _apiClient;

        [BindProperty]
        public Session Session { get; set; }

        [TempData]
        public string Message { get; set; }

        public EditSessionModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGet(int id)
        {
            var session = await _apiClient.GetSessionAsync(id);

            Session = new Session()
            {
                Id = session.Id,
                Abstract = session.Abstract,
                EndTime = session.EndTime,
                StartTime = session.StartTime,
                Title = session.Title,
                TrackId = session.TrackId
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Message = "Session updated successfully";


            await _apiClient.PutSessionAsync(Session);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var session = await _apiClient.GetSessionAsync(id);

            if (session != null)
            {
                await _apiClient.DeleteSessionAsync(id);

            }

            Message = "Session deleted successfully";

            return RedirectToPage("/Index");
        }
    }
}