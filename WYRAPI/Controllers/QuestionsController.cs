using Microsoft.AspNetCore.Mvc;
using WYRAPI.Data;
using WYRAPI.Models;

namespace WYRAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionsDatabase db;

        public QuestionsController(QuestionsDatabase db)
        {
            this.db = db;
        }

        [HttpGet("questions/random")]
        public IActionResult GetRandomQuestion()
        {
            Question q = db.GetRandomQuestion();
            return Ok(q);
        }

        [HttpPost("vote")]
        public IActionResult AddVote([FromBody] VoteRequest vote)
        {
            db.AddVote(vote.QuestionId, vote.Choice);
            return Ok(new { message = "Vote recorded" });
        }

        [HttpGet("results")]
        public IActionResult GetResults()
        {
            return Ok(db.GetAllQuestions());
        }
    }
}