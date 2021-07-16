let moduleNameInput = document.querySelector('.module-name-input');
let similarModulesCard = document.querySelector('.similar-modules-card');
let modules;

let getRequest = new XMLHttpRequest();
getRequest.open("GET", `https://localhost:${location.port}/Mod?handler=Modules`, true);
getRequest.onreadystatechange = function(){
    if (getRequest.readyState == 4 && getRequest.status == 200){
        modules = JSON.parse(getRequest.responseText);
    }
};

moduleNameInput.onclick = function() {
    getRequest.send();
    this.onclick = null;
}

//function ajaxReq(event, modId, projId) {
//    const request = new XMLHttpRequest();
//    const params = `id=${module['Id']}&projId=${module['ProjId']}`;

//    request.open('POST', 'Mod?handler=CopyModule');
//    request.responseType = 'json';
//    request.setRequestHeader("XSRF-TOKEN", $('input:hidden[name="__RequestVerificationToken"]').val());

//    //$.ajax({
//    //    type: 'POST',
//    //    url: 'Mod?handler=CopyModule',
//    //    dataType: "json",
//    //    data: {
//    //        'id': modId,
//    //        'projId': projId
//    //    },
//    //    headers: {
//    //        RequestVerificationToken:
//    //            $('input:hidden[name="__RequestVerificationToken"]').val()
//    //    },
//    //    success: function (response) {
//    //        if (response.status === 'OK') {
//    //            window.location.href = response.Url
//    //        }
//    //        else {
//    //            alert("The status cannot be updated at this time");
//    //        }
//    //    }
//    //});
//}


moduleNameInput.oninput = function () {
    similarModulesCard.innerHTML = "";
    if (moduleNameInput.value !== "") {
        let count = 0;
        for (let module of modules) {
            count++;
            if (module['Name'].toLowerCase().includes(moduleNameInput.value.toLowerCase())) {

                //let copyModForm = document.createElement('form');
                //copyModForm.classList.add('copy-module-form')
                //copyModForm.method = 'post';
                //copyModForm.autocomplete = 'off';
                //copyModForm.id = `copy-module-form-${count}`;
                //similarModulesCard.append(copyModForm);

                let copyModBtn = document.createElement('button');
                copyModBtn.classList.add('module-name', 'module-info');
                copyModBtn.innerHTML = `${module['Name']} | ${module['ProjectName']}`;
                copyModBtn.id = `copy-mod-btn-${count}`;
                similarModulesCard.append(copyModBtn);

                $(`#copy-mod-btn-${count}`).on('click', function () {
                    $.ajax({
                        type: 'POST',
                        url: 'Mod?handler=CopyModule',
                        dataType: "json",
                        data: {
                            'id': modId,
                            'projId': projId
                        },
                        headers: {
                            RequestVerificationToken:
                                $(`#copy-mod-btn-${count}`).val()
                        },
                        success: function (response) {
                            if (response.status === 'OK') {
                                window.location.href = response.Url
                            }
                            else {
                                alert("The status cannot be updated at this time");
                            }
                        }
                    });
                });
            }
        }
    }
}
