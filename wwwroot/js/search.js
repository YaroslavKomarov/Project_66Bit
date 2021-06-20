let projectParent = document.querySelector(".cardFix");
let projects = [...projectParent.children];
let searchInput = document.querySelector(".search");
let projectNames = [];
for (let i = 0; i < projects.length; i++) {
    projectNames.push(projects[i].children[0].firstChild.text.toLowerCase());

}

searchInput.oninput = function() {
    projectParent.innerHTML = '';
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
