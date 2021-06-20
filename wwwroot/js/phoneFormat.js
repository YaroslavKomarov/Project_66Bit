let phoneNumber = document.querySelector(".customerPhoneNumber");

phoneNumber.onchange = function () {
    let cleaned = ('' + phoneNumber.value).replace(/\D/g, '');
    let match = cleaned.match(/^(\d{1})(\d{3})(\d{3})(\d{2})(\d{2})$/);
    if (match) {
        phoneNumber.value = '+7 (' + match[2] + ') ' + match[3] + '-' + match[4] + '-' + match[5];
    } else {
        phoneNumber.value = '';
    }
}