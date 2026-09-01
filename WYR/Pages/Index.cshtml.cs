using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WYR.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public int QuestionId { get; set; }

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("WYRAPI");
            var response = await client.GetAsync("/api/questions/random");
            var json = await response.Content.ReadAsStringAsync();
            var question = JsonSerializer.Deserialize<JsonElement>(json);

            QuestionId = question.GetProperty("id").GetInt32();
            OptionA = question.GetProperty("optionA").GetString();
            OptionB = question.GetProperty("optionB").GetString();
        }

        public async Task<IActionResult> OnPostAsync(string choice)
        {
            var client = _httpClientFactory.CreateClient("WYRAPI");
            var voteData = new { QuestionId = QuestionId, Choice = choice };
            await client.PostAsJsonAsync("/api/vote", voteData);
            return RedirectToPage();
        }
    }
}