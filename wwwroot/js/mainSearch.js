let projectParent = document.querySelector(".project");
let moduleParent = document.querySelector(".module");
let projects = [...projectParent.children];
let modules = [...moduleParent.children];
let searchInput = document.querySelector(".search-input");
let projectNames = [];
let moduleNames = [];

for (let i = 0; i < projects.length; i++) {
    projectNames.push(projects[i].children[0].children[1].text.toLowerCase());
}

for (let i = 0; i < modules.length; i++) {
    moduleNames.push(modules[i].children[0].children[1].text.toLowerCase());
}

searchInput.oninput = function () {
    projectParent.innerHTML = '';
    moduleParent.innerHTML = '';
    let flag = true;
    if (searchInput.value != "") {
        for (let i = 0; i < projectNames.length; i++) {
            if (projectNames[i].includes(searchInput.value.toLowerCase())){
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
