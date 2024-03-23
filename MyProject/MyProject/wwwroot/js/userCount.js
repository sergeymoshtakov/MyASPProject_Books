//створюємо з'єднання
var connectionUserCount = new signalR.HubConnectionBuilder().withUrl("/hubs/userCount").build();

//методи які можуть бути викликані з концентратора на стронці сервера
connectionUserCount.on("updateTotalViews", (value) => {
    var newCountSpan = document.getElementById("totalViewsCounter");
    newCountSpan.innerText = value.toString();
})
//методи які можуть бути викликані з концентратора на стронці сервера
connectionUserCount.on("updateConnectionCount", (count) => {
    var connectionCountSpan = document.getElementById("connectionCount");
    connectionCountSpan.innerText = count.toString();
})

//виклик метода з концентратора
function newWindowLoadedOnClient() {
    connectionUserCount.send("NewWindowLoaded");
}

//запуск активного з'днання
function fullfilled() {
    console.log("Succesfull");
    newWindowLoadedOnClient();
}

function reject() {
    console.log("No connection");
}

connectionUserCount.start().then(fullfilled, reject);