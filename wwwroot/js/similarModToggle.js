let similarModLen = document.getElementsByClassName('similar-mod-btn').length;

for (let i = 0; i < similarModLen; i++) {
    let similarBtnId = document.getElementById(`similar-mod-btn-${i + 1}`);
    let similarMod = document.getElementById(`similar-modules-${i + 1}`);

    similarBtnId.onclick = function () {
        similarMod.style.display = similarMod.style.display === 'none' ? 'block' : 'none';
    }
}
