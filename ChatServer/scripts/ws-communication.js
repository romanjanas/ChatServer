var ws = new WebSocket("ws://chatserver6908.azurewebsites.net/chatws");
ws.onopen = function () {
    console.log("About to send data");
    ws.send("MDN");
    console.log("Message sent!");
};

msgOnMessage = function (event) {
    console.log("About to receive data");
    var received_msg = event.data;
    console.log("Message received = " + received_msg);
    addMessageToThread(received_msg);
}

ws.onmessage = msgOnMessage;
ws.onclose = function () {
    console.log("Connection is closed...");
};

var sendMessage = function () {
    console.log("message send clicked");
    ws.send(document.getElementById("messageInput").value);
}

var addMessageToThread = function (message) {
    var str =
        '<li class="left clearfix">'
            + '<div class="chat-body clearfix">'
                + '<div class="header">'
                    + '<strong class="primary-font">John Doe</strong>'
                    + '<small class="pull-right text-muted"><i class="fa fa-clock-o"></i> 12 mins ago</small>'
                + '</div>'
                + '<p>'
                + message
                + '</p>'
            + '</div>'
        + '</li>',
        div = document.getElementById("chatThread");
    div.insertAdjacentHTML("beforeend", str);
}