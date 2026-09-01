using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace WYR.Pages
{
    public class ResultsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public List<QuestionResult> Questions { get; set; } = new List<QuestionResult>();

        public ResultsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("WYRAPI");
            var response = await client.GetAsync("/api/results");
            var json = await response.Content.ReadAsStringAsync();
            var results = JsonSerializer.Deserialize<List<JsonElement>>(json);

            foreach (var q in results)
            {
                int votesA = q.GetProperty("votesA").GetInt32();
                int votesB = q.GetProperty("votesB").GetInt32();
                if (votesA + votesB == 0)
                {
                    continue;
                }

                Questions.Add(new QuestionResult
                {
                    OptionA = q.GetProperty("optionA").GetString(),
                    OptionB = q.GetProperty("optionB").GetString(),
                    PercentA = q.GetProperty("percentA").GetDouble(),
                    PercentB = q.GetProperty("percentB").GetDouble()
                });
            }
        }
    }

    public class QuestionResult
    {
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public double PercentA { get; set; }
        public double PercentB { get; set; }
    }
}