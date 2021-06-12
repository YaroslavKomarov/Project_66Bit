var modal = document.getElementById("myModal");
var btn = document.getElementById("myBtn");
var button = document.getElementsByClassName("close")[0];
btn.onclick = function () {
    modal.style.display = "block";
}
button.onclick = function () {
    modal.style.display = "none";
}