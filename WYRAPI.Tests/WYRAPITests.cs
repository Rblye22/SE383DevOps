using WYRAPI.Models;
using Xunit;

namespace WYRAPI.Tests
{
    public class WYRAPITests
    {
        [Fact]
        public void Question_StoresPropertiesCorrectly()
        {
            var question = new Question
            {
                Id = 1,
                OptionA = "Fly",
                OptionB = "Be invisible",
                VotesA = 5,
                VotesB = 10
            };

            Assert.Equal(1, question.Id);
            Assert.Equal("Fly", question.OptionA);
            Assert.Equal("Be invisible", question.OptionB);
            Assert.Equal(5, question.VotesA);
            Assert.Equal(10, question.VotesB);
        }

        [Fact]
        public void VoteRequest_StoresPropertiesCorrectly()
        {
            var vote = new VoteRequest
            {
                QuestionId = 3,
                Choice = "A"
            };

            Assert.Equal(3, vote.QuestionId);
            Assert.Equal("A", vote.Choice);
        }
        [Fact]
        public void Question_DefaultsVotesToZero()
        {
            var question = new Question
            {
                Id = 2,
                OptionA = "Pizza",
                OptionB = "Tacos"
            };

            Assert.Equal(0, question.VotesA);
            Assert.Equal(0, question.VotesB);
        }
    }
}