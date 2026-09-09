using WYR.Pages;
using Xunit;

namespace WYR.Tests
{
    public class WYRTests
    {
        [Fact]
        public void QuestionResult_StoresPropertiesCorrectly()
        {
            var result = new QuestionResult
            {
                OptionA = "Fly",
                OptionB = "Be invisible",
                PercentA = 40.5,
                PercentB = 59.5
            };

            Assert.Equal("Fly", result.OptionA);
            Assert.Equal("Be invisible", result.OptionB);
            Assert.Equal(40.5, result.PercentA);
            Assert.Equal(59.5, result.PercentB);
        }

        [Fact]
        public void QuestionResult_PercentagesAddUpToOneHundred()
        {
            var result = new QuestionResult
            {
                OptionA = "Coffee",
                OptionB = "Tea",
                PercentA = 65.0,
                PercentB = 35.0
            };

            Assert.Equal(100.0, result.PercentA + result.PercentB);
        }
    }
}