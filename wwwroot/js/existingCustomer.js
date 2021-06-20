let projectParent = document.querySelector(".cardFix");
let projects = [...projectParent.children];
let customerNameInput = document.querySelector(".customerName");
let customerNames = [];
for (let i = 0; i < projects.length; i++) {
    customerNames.push(projects[i].children[2].lastChild.text.toLowerCase());
}

customerNameInput.oninput = function () {
    projectParent.innerHTML = '';
    let flag = true;
    if (customerNameInput.value != "") {
        for (let i = 0; i < customerNames.length; i++) {
            if (customerNames[i].includes(customerNameInput .value.toLowerCase())) {
                projectParent.appendChild(projects[i]);
                flag = false;
            }
        }
    }
    if (flag) {
        for (let i = 0; i < projectNames.length; i++) {
            projectParent.appendChild(projects[i]);
        }
    }
};
