var ctx = document.getElementById('myChart');
let borderColorArr = [];
let backColorArr = [];

function getRandomColor() {
    return "#" + ("000000" + Math.floor(Math.random() * 16777216).toString(16)).substr(-6);
}

function hexToRGB(hex, alpha) {
    var r = parseInt(hex.slice(1, 3), 16),
        g = parseInt(hex.slice(3, 5), 16),
        b = parseInt(hex.slice(5, 7), 16);

    if (alpha) {
        return "rgba(" + r + ", " + g + ", " + b + ", " + alpha + ")";
    } else {
        return "rgb(" + r + ", " + g + ", " + b + ")";
    }
}

for (let i = 0; i < labelsArr.length; i++) {
    let randColor = getRandomColor();
    borderColorArr.push(randColor);
    backColorArr.push(hexToRGB(randColor, 0.2));
}

var myChart = new Chart(ctx, {
    type: 'doughnut',
    data: {
        labels: labelsArr,
        datasets: [{
            data: dataArr,
            backgroundColor: backColorArr,
            borderColor: borderColorArr,
            borderWidth: 1
        }]
    },
    options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
            legend: {
                position: 'top',
            }
        }
    },
});