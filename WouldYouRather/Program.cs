var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


QuestionsDatabase db = new QuestionsDatabase();

app.MapGet("/api/questions/random", () =>
{
Question q = db.GetRandomQuestion();
return Results.Ok(q);
});

app.MapPost("/api/vote", (VoteRequest vote) =>
{
db.AddVote(vote.QuestionId, vote.Choice);
return Results.Ok(new { message = "Vote recorded" });
});

app.MapGet("/api/results", () =>
{
return Results.Ok(db.GetAllQuestions());
});

app.Run();
public class VoteRequest
{
    public int QuestionId { get; set; }
    public string Choice { get; set; }
}
public class Question
{
    public int Id { get; set; }
    public string OptionA { get; set; }
    public string OptionB { get; set; }
    public int VotesA { get; set; }
    public int VotesB { get; set; }
    public double PercentA { get; set; }
    public double PercentB { get; set; }

    public Question(int id, string optionA, string optionB)
    {
        Id = id;
        OptionA = optionA;
        OptionB = optionB;
    }
}
public class QuestionsDatabase
{
    private List<Question> questions = new List<Question>
    {
        new Question(1, "Have the ability to fly", "Have the ability to become invisible"),
        new Question(2, "Always be 10 minutes late", "Always be 20 minutes early"),
        new Question(3, "Give up pizza forever", "Give up burgers forever"),
        new Question(4, "Fight one horse-sized duck", "Fight 100 duck-sized horses"),
        new Question(5, "Have unlimited pancakes", "Have unlimited waffles")
    };
    public List<Question> GetAllQuestions()
    {
        return questions;
    }
    public Question GetRandomQuestion()
    {
        Random rnd = new Random();
        return questions[rnd.Next(questions.Count)];
    }
    public void AddVote(int questionId, string choice)
    {
        foreach (Question q in questions)
        {
            if (q.Id == questionId)
            {
                if (choice == "A") q.VotesA++;
                if (choice == "B") q.VotesB++;

                int total = q.VotesA + q.VotesB;
                if (total > 0)
                {
                    q.PercentA = Math.Round((double)q.VotesA / total * 100, 1);
                    q.PercentB = Math.Round((double)q.VotesB / total * 100, 1);
                }
            }
        }
    }
}