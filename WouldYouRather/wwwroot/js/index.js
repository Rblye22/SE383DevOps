var currentQuestionId = null;
var selectedChoice = null;

async function loadRandomQuestion() {
    var response = await fetch('/api/questions/random');
    var question = await response.json();

    currentQuestionId = question.id;
    selectedChoice = null;

    document.getElementById('optionAText').innerHTML = '<p>' + question.optionA + '</p>';
    document.getElementById('optionBText').innerHTML = '<p>' + question.optionB + '</p>';
}

document.getElementById('optionA').addEventListener('click', function () {
    selectedChoice = 'A';
});

document.getElementById('optionB').addEventListener('click', function () {
    selectedChoice = 'B';
});

document.getElementById('nextBtn').addEventListener('click', async function () {
    if (selectedChoice != null) {
        await fetch('/api/vote', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ questionId: currentQuestionId, choice: selectedChoice })
        });
    }
    loadRandomQuestion();
});
loadRandomQuestion();