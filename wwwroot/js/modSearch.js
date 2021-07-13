class Request {
    static ajaxRequest(urn) {
        console.log("Зашел в ajaxRequest");
        let request = new XMLHttpRequest();
        let result;
        request.open("GET", `https://localhost:5001/${urn}`, true);
        request.onreadystatechange = function(){
            if (request.readyState == 4 && request.status == 200){
                result = JSON.parse(request.responseText);
            }
        };
        request.send();

        return result;
    }
}


class Search {
    constructor(modelInput, handler){
        this.modelInput = modelInput;
        this.handler = handler;
        
        this.modelInput.onclick = this.onClick;
        this.modelInput.oninput = this.onInput;
    }

    onClick() {
        console.log("Зашел в onclick");
        console.log(window.location);
        models = Request.ajaxRequest();
        this.onclick = null;
    }

    onInput() {
        for (let model of this.models){
            console.log("Осуществляю поиск");
            if (model["Name"].toLowerCase().includes(modelInput.value.toLowerCase())){
                // Выводим результат в списке поиска
                console.log(`Нашел ${model["Name"]}`);
            }
        }
    }
}


let models;
let handler = "Modules";
let modelInput = document.querySelector(".addModule .baton .login");
let search = new Search(modelInput, handler);
console.log("OPA");
