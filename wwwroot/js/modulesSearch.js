function reqReadyStateChange(){
    if (request.readyState == 4 && request.status == 200){
        let modules = JSON.parse(request.responseText);
        console.log(modules);
        searchForModules(modules);
    }
}


function searchForModules(modules){
    let moduleNames = [];
    for (let module of modules){
        moduleNames.push(module["Name"]);
    }

    
}


let moduleNameInput = document.querySelector(".addModule .baton .login");

let request = new XMLHttpRequest();
request.open("GET", "https://localhost:5001/Mod?handler=Modules", true);
request.onreadystatechange = reqReadyStateChange;

moduleNameInput.onclick = function(){
    request.send();
    this.onclick = null;
}
