let modal = document.getElementById("myModal");
let btn2 = document.getElementById("myBtn");
let button2 = document.getElementsByClassName("close")[0];
btn2.onclick = function () {
    modal.style.display = "block";
}
button2.onclick = function () {
    modal.style.display = "none";
}