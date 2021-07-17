let moduleNameInput = document.querySelector('.module-name-input');
let copyModulesCard = document.querySelector('.copy-modules-card');
let projId = document.getElementById('project-id-hidden').innerHTML;
let modules;

let getRequest = new XMLHttpRequest();
getRequest.open("GET", `https://localhost:${location.port}/Mod?handler=Modules`);
getRequest.onreadystatechange = function () {
    if (getRequest.readyState == 4 && getRequest.status == 200) {
        modules = JSON.parse(getRequest.responseText);
    }
};

moduleNameInput.onclick = function () {
    getRequest.send();
    this.onclick = null;
}

moduleNameInput.oninput = function () {
    copyModulesCard.innerHTML = "";
    for (let module of modules) {
        if (module['Name'].toLowerCase().includes(moduleNameInput.value.toLowerCase()) && moduleNameInput.value !== "") {
            copyModulesCard.style.display = 'block';
            let copyModBtn = document.createElement('a');
            copyModBtn.classList.add('module-name', 'module-info');
            copyModBtn.innerHTML = `${module['Name']} | ${module['ProjectName']}`;
            copyModulesCard.append(copyModBtn);

            copyModBtn.addEventListener('click', function () {
                $.ajax({
                    type: 'POST',
                    url: '/Mod?handler=CopyModule',
                    headers: {
                        "XSRF-TOKEN": $('input:hidden[name="__RequestVerificationToken"]').val()
                    },
                    dataType: "json",
                    data: {
                        'id': module['Id'],
                        'projId': projId
                    },
                    success: function (response) {
                        if (response.status === 'OK') {
                            window.location.reload();
                        }
                        else {
                            alert("The status cannot be updated at this time");
                        }
                    }
                });
            });
        } else {
            copyModulesCard.style.display = 'none';
        }
    }
}
