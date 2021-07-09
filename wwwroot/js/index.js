let modal = document.getElementById("myModal");
let btn = document.getElementById("myBtn");
let button = document.getElementsByClassName("close")[0];

btn.onclick = function () {
    modal.style.display = "block";
}

button.onclick = function () {
    modal.style.display = "none";
}