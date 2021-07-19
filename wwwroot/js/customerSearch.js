let customerNameInput = document.querySelector(".customer-name-input");
let customerEmailInput = document.querySelector(".customer-email-input");
let customerPhoneInput = document.querySelector(".customer-phone-input");
let existCustomersCard = document.querySelector('.exist-customers-card');
let customers;

let request = new XMLHttpRequest();
request.open("GET", `https://localhost:${location.port}/Index?handler=Customers`);
request.onreadystatechange = function(){
    if (request.readyState == 4 && request.status == 200){
        customers = JSON.parse(request.responseText);
    }
};

customerNameInput.onclick = function(){
    request.send();
    this.onclick = null;
}

customerNameInput.oninput = function () {
    existCustomersCard.style.display = 'none';
    existCustomersCard.innerHTML = "";
    if (customerNameInput.value !== "") {
        for (let customer of customers) {
            if (customer["Name"].toLowerCase().includes(customerNameInput.value.toLowerCase())) {
                existCustomersCard.style.display = 'block';
                let addExistCustomer = document.createElement('a');
                addExistCustomer.classList.add('module-name', 'module-info');
                addExistCustomer.innerHTML = `<div>${customer['Name']}</div><div>${customer['Email']}</div><div>${customer['PhoneNumber']}</div>`;
                existCustomersCard.append(addExistCustomer);

                addExistCustomer.addEventListener('click', function () {
                    customerNameInput.value = customer['Name'];
                    customerEmailInput.value = customer['Email'];
                    customerPhoneInput.value = customer['PhoneNumber'];
                });
            }
        }
    } else {
        existCustomersCard.style.display = 'none';
    }
}
