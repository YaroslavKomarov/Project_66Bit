function reqReadyStateChange(){
    if (request.readyState == 4 && request.status == 200){
        modules = JSON.parse(request.responseText);
        console.log(modules);
    }
}


let modules;
let moduleNameInput = document.querySelector(".addModule .baton .login");

let request = new XMLHttpRequest();
request.open("GET", "https://localhost:5001/Mod?handler=Modules", true);
request.onreadystatechange = reqReadyStateChange;

moduleNameInput.onclick = function(){
    request.send();
    this.onclick = null;
}

moduleNameInput.oninput = function(){
    for (let module of modules){
        if (module["Name"].toLowerCase().includes(moduleNameInput.value.toLowerCase())){
            // Выводим модуль в списке поиска
        }
    }
}
