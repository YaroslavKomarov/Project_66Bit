var coll = document.getElementsByClassName("collapsible");

for (var i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {    
            content.style.display = "none";
            document.getElementById(content.classList[0]).style.height = "130px";
            document.getElementById(content.classList[1]).style.display = "none";
            document.getElementById(content.classList[2]).style.top = "-80px";
            document.getElementById(content.classList[3]).style.top = "-200px";
            document.getElementById(content.classList[4]).style.top = "-80px";
        }
        else {
            content.style.display = "block";
            document.getElementById(content.classList[0]).style.height = "400px";
        }
    });
}