let moduleCount = document.getElementsByClassName('module-card').length;

for (let i = 0; i < moduleCount; i++) {
    let confirmSpaceMod = document.getElementById(`confirm-space-mod-${i + 1}`);
    let showDeleteModBtn = document.getElementById(`show-delete-mod-btn-${i + 1}`);
    let hideDeleteModBtn = document.getElementById(`hide-delete-mod-btn-${i + 1}`);


    showDeleteModBtn.onclick = function () {
        confirmSpaceMod.style.display = "block";
    }

    hideDeleteModBtn.onclick = function () {
        confirmSpaceMod.style.display = "none";
    }
}