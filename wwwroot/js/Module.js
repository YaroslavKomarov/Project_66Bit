var coll = document.getElementsByClassName("module");

for (var i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
            document.getElementById(content.classList[0]).style.height = "150px";

        } else {
            content.style.display = "block";
            document.getElementById(content.classList[0]).style.height = "320px";
        }
    });
}