var btn = document.getElementById('toggle-link');
var text = document.querySelector('.main-menu');
var html = document.querySelector('html');

btn.addEventListener('click', function () {
    text.style.display = 'block';
    text.style.transition = 'all 0s';
    text.style.opacity = 1;
});

html.addEventListener('click', function (e) {
    if (e.target.id !== 'toggle-link' && e.target.tagName !== 'LI') {
        text.style.transition = 'all 0.5s';
        text.style.opacity = 0;
        setTimeout(function () {
            text.style.display = 'none';
        }, 500);
    }
});