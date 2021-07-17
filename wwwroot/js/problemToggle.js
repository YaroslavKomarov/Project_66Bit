let modulesCount = document.getElementsByClassName('module-card').length;

for (let i = 0; i < modulesCount; i++) {
    let addProblemBtn = document.getElementById(`add-problem-btn-${i + 1}`);
    let addProblemCard = document.getElementById(`add-problem-card-${i + 1}`);

    addProblemBtn.onclick = function () {
        addProblemCard.style.display = addProblemCard.style.display === 'none' ? 'block' : 'none';
    }

    let showProblemsBtn = document.getElementById(`show-problems-list-btn-${i + 1}`);
    let scrollProblemList = document.getElementById(`scroll-problem-list-${i + 1}`);

    showProblemsBtn.onclick = function () {
        scrollProblemList.style.display = scrollProblemList.style.display === 'none' ? 'block' : 'none';
    }
}
