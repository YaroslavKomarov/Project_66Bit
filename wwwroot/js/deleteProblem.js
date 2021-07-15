let problemCount = document.getElementsByClassName('problem-card').length;

for (let i = 0; i < problemCount; i++) {
    let confirmSpaceProblem = document.getElementById(`confirm-space-problem-${i + 1}`);
    let showDeleteProblemBtn = document.getElementById(`show-delete-problem-btn-${i + 1}`);
    let hideDeleteProblemBtn = document.getElementById(`hide-delete-problem-btn-${i + 1}`);


    showDeleteProblemBtn.onclick = function () {
        confirmSpaceProblem.style.display = "block";
    }

    hideDeleteProblemBtn.onclick = function () {
        confirmSpaceProblem.style.display = "none";
    }
}