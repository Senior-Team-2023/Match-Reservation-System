"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/reservationHub").build();
var sendButton = document.getElementById("sendButton");
var deleteButton = document.getElementById("deleteButton");

//Disable the send button until connection is established.
if (sendButton) {
    sendButton.disabled = true;
}
if(deleteButton) {
    deleteButton.disabled = true;
}

connection.on("ReceiveMessage", function (action, seatPos) {
    console.log(`ReceiveMessage: ${action} ${seatPos}`);
    console.log(`Seat id seat-${seatPos}`);
    var seatElement = document.getElementById(`seat-${seatPos}`);
    console.log(seatElement);
    if (action === "reserved") {
        seatElement.classList.add("grid-btn-rsrv");
        seatElement.classList.remove("grid-btn-selected");
    }
    else if (action === "cancelled") {
        seatElement.classList.remove("grid-btn-rsrv");
    }
});

connection.start().then(function () {
    if (sendButton) {
        sendButton.disabled = false;
    }
    if (deleteButton) {
        deleteButton.disabled = false;
    }
}).catch(function (err) {
    return console.error(err.toString());
});

if (sendButton) { 
    sendButton.addEventListener("click", function (event) {
        var action = "reserved";
        var seatPos = Number(document.getElementById("SeatPosInput").value);
        //console.log(`SendButton: ${action} ${seatPos}`);
        connection.invoke("SendMessage", action, seatPos).catch(function (err) {
            return console.error(err.toString());
        });
        //event.preventDefault();
    });
}
if (deleteButton) {
    deleteButton.addEventListener("click", function (event) {
        var action = "cancelled";
        //console.log(`DeleteButton: ${document.getElementById("seatPos").innerText}`);
        var seatPos = Number(document.getElementById("seatPos").innerText);
        //console.log(`DeleteButton: ${action} ${seatPos}`);
        connection.invoke("SendMessage", action, seatPos).catch(function (err) {
            return console.error(err.toString());
        });
        //event.preventDefault();
    });
}