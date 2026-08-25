using MySqlConnector;

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


string connectionString = builder.Configuration.GetConnectionString("AivenDb");
QuestionsDatabase db = new QuestionsDatabase(connectionString);

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

}
public class QuestionsDatabase
{
    private string connectionString;
    public QuestionsDatabase(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public List<Question> GetAllQuestions()
    {
        List<Question> questions = new List<Question>();
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Questions", connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            questions.Add(ReadQuestion(reader));
        }
        connection.Close();
        return questions;
    }
    public Question GetRandomQuestion()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Questions ORDER BY RAND() LIMIT 1", connection);
        MySqlDataReader reader = command.ExecuteReader();

        Question question = null;
        if (reader.Read())
        {
            question = ReadQuestion(reader);
        }
        connection.Close();
        return question;
    }
    public void AddVote(int questionId, string choice)
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        connection.Open();

        string sql = "UPDATE Questions SET VotesA = VotesA + 1 WHERE Id = @id";
        if (choice == "B")
        {
            sql = "UPDATE Questions SET VotesB = VotesB + 1 WHERE Id = @id";
        }

        MySqlCommand command = new MySqlCommand(sql, connection);
        command.Parameters.AddWithValue("@id", questionId);
        command.ExecuteNonQuery();
        connection.Close();
    }
    private Question ReadQuestion(MySqlDataReader reader)
    {
        Question q = new Question();
        q.Id = reader.GetInt32("Id");
        q.OptionA = reader.GetString("OptionA");
        q.OptionB = reader.GetString("OptionB");
        q.VotesA = reader.GetInt32("VotesA");
        q.VotesB = reader.GetInt32("VotesB");

        int total = q.VotesA + q.VotesB;
        if (total > 0)
        {
            q.PercentA = Math.Round((double)q.VotesA / total * 100, 1);
            q.PercentB = Math.Round((double)q.VotesB / total * 100, 1);
        }
        return q;
    }
}