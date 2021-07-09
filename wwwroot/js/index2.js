var coll = document.getElementsByClassName("collapsible");

for (var i = 0; i < coll.length; i++) {
    coll[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
            document.getElementById(content.classList[0]).style.height = "140px";
        }
        else {
            content.style.display = "block";
            document.getElementById(content.classList[0]).style.height = "400px";
            content.style.top = "230px";

        }
    });
}

function buttonClick() {
    for (let i = 1; i < 10; i++) {
        let b = 'redact' + i;
        document.getElementById(b).removeAttribute('disabled');
    }
}

function buttonClick1() {
    for (let i = 1; i < 10; i++) {
        let b = 'redact' + i;
        document.getElementById(b).setAttribute('disabled', true);
    }
}
var red1 = document.getElementsByClassName("redact");

for (var i = 0; i < red1.length; i++) {
    red1[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this.nextElementSibling;
        if (content.style.display === "block") {
            content.style.display = "none";
        }
        else {
            content.style.display = "block";
        }
    });
}
var red = document.getElementsByClassName("redact1");

for (var i = 0; i < red.length; i++) {
    red[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var content = this;
        if (content.style.display === "block") {
            content.style.display = "none";
        }
        else {
            content.style.display = "block";
        }
    });
}