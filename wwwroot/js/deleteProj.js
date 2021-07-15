let projCount = document.getElementsByClassName('proj-card').length;

for (let i = 0; i < projCount; i++) {
    let confirmSpaceProj = document.getElementById(`confirm-space-proj-${i + 1}`);
    let showDeleteProjBtn = document.getElementById(`show-delete-proj-btn-${i + 1}`);
    let hideDeleteProjBtn = document.getElementById(`hide-delete-proj-btn-${i + 1}`);


    showDeleteProjBtn.onclick = function() {
        confirmSpaceProj.style.display = "block";
        console.log(i + 1);
    }

    hideDeleteProjBtn.onclick = function() {
        confirmSpaceProj.style.display = "none";
    }
}