async function loadResults() {
    var response = await fetch('/api/results');
    var questions = await response.json();

    var container = document.getElementById('resultsContainer');
    container.innerHTML = '';

    for (var i = 0; i < questions.length; i++) {
        var q = questions[i];

        var totalVotes = q.votesA + q.votesB;
        if (totalVotes == 0) {
            continue;
        }

        var box = document.createElement('div');
        box.className = 'Questions';
        var html = '<div class="options">';
        html += '<div class="option" style="background-color: teal;">';
        html += '<p>' + q.optionA + '</p>';
        html += '<p>' + q.percentA + '%</p>';
        html += '</div>';
        html += '<div class="or-label">VS</div>';
        html += '<div class="option" style="background-color: purple;">';
        html += '<p>' + q.optionB + '</p>';
        html += '<p>' + q.percentB + '%</p>';
        html += '</div>';
        html += '</div>';

        box.innerHTML = html;
        container.appendChild(box);
    }
}
loadResults();