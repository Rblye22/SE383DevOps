using WYRAPI.Models;
using MySqlConnector;

namespace WYRAPI.Data
{
    public class QuestionsDatabase
    {
        private readonly string connectionString;

        public QuestionsDatabase(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Question> GetAllQuestions()
        {
            List<Question> questions = new List<Question>();
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Questions", connection);
            using MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                questions.Add(ReadQuestion(reader));
            }
            return questions;
        }

        public Question GetRandomQuestion()
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM Questions ORDER BY RAND() LIMIT 1", connection);
            using MySqlDataReader reader = command.ExecuteReader();
            Question question = null;
            if (reader.Read())
            {
                question = ReadQuestion(reader);
            }
            return question;
        }

        public void AddVote(int questionId, string choice)
        {
            using MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();
            string sql = "UPDATE Questions SET VotesA = VotesA + 1 WHERE Id = @id";
            if (choice == "B")
            {
                sql = "UPDATE Questions SET VotesB = VotesB + 1 WHERE Id = @id";
            }
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", questionId);
            command.ExecuteNonQuery();
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
}
