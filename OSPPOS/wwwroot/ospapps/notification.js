"use strict";

    var connection = new signalR.HubConnectionBuilder().withUrl("/notificationHub").build();

    connection.on("ReceiveNotification", function (Message) {
        // do whatever you want to do with `message`

        var li = document.createElement("li");
        document.getElementById("MessageList").appendChild(li);
        li.textContent = `This is the $('<strong> ${Message}</strong>')`;
       
       li.style.fontSizeAdjust=""
    });

connection.start().catch(function (err) {
    return console.error(err.toString());
});

