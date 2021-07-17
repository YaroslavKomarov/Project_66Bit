let searchInput = document.querySelector(".search-input");
let projectParent = document.querySelector(".project");
let moduleParent = document.querySelector(".module");
let projects = [...projectParent.children];
let modules = [...moduleParent.children];
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
    let flagProj = true;
    let flagMod = true;
    if (searchInput.value != "") {
        for (let i = 0; i < projectNames.length; i++) {
            if (projectNames[i].includes(searchInput.value.toLowerCase())) {
                projectParent.appendChild(projects[i]);
                flagProj = false;
            }
        }
        if (flagProj) {
            let messageCard = document.createElement('div');
            messageCard.classList.add('card', 'not-have-elem');
            messageCard.innerHTML = 'Проектов с данным именем не найдено';
            projectParent.append(messageCard);
        }

        for (let j = 0; j < moduleNames.length; j++) {
            if (moduleNames[j].includes(searchInput.value.toLowerCase())) {
                moduleParent.appendChild(modules[j]);
                flagMod = false;
            }
        }
        if (flagMod && searchInput.value != "") {
            let messageCard = document.createElement('div');
            messageCard.classList.add('card', 'not-have-elem');
            messageCard.innerHTML = 'Модулей с данным именем не найдено';
            moduleParent.append(messageCard);
        }
    } else {
        for (let i = 0; i < projectNames.length; i++) {
            projectParent.appendChild(projects[i]);
        }

        for (let i = 0; i < moduleNames.length; i++) {
            moduleParent.appendChild(modules[i]);
        }
    }
};
