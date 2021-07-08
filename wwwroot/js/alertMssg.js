let deleteProj = document.getElementById(deleteProjId);
let deleteMod = document.getElementById(deleteProjId);
let deleteProblem = document.getElementById(deleteProjId);
let confirmSpaceProj = document.querySelector('.confirm-space-proj');
let confirmSpaceMod = document.querySelector('.confirm-space-mod');
let confirmSpaceProblem = document.querySelector('.confirm-space-problem');

let cancelMod = document.getElementById(cancelModId);
let cancelProj = document.getElementById(cancelProjId);
let cancelProblem = document.getElementById(cancelProblemId);

deleteProj.onclick = function () {
    confirmSpaceProj.style.display = 'block';
}

deleteMod.onclick = function () {
    confirmSpaceMod.style.display = 'block';
}

deleteProblem.onclick = function () {
    confirmSpaceMod.style.display = 'block';
}

cancelProj.onclick = function () {
    confirmSpaceProj.style.display = 'none';
}

cancelMod.onclick = function () {
    confirmSpaceMod.style.display = 'none';
}

cancelProblem.onclick = function () {
    confirmSpaceProblem.style.display = 'none';
}