let customerNameInput = document.querySelector(".CustomerCard .baton .customerName");
let customers;

let request = new XMLHttpRequest();
request.open("GET", "https://localhost:5001/Index?handler=Customers", true);
request.onreadystatechange = function(){
    if (request.readyState == 4 && request.status == 200){
        customers = JSON.parse(request.responseText);
    }
};

customerNameInput.onclick = function(){
    request.send();
    this.onclick = null;
}

customerNameInput.oninput = function(){
    for (let customer of customers){
        if (customer["Name"].toLowerCase().includes(customerNameInput.value.toLowerCase())){
            console.log(customer);
            // Выводим модуль в списке поиска
        }
    }
}
