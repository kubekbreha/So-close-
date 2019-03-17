var hostname = "mqttws.kpi.fei.tuke.sk";
var port = Number(80);
var uid = "hackathon2k18" + Date.now();
var gameId = new URLSearchParams(window.location.search).get('id');
var destination = "openlab/game/" + gameId + "/" + uid;
// var destination = "openlab2/game/" + gameId + "/" + uid;
// alert(destination);
// called when the client connects
console.log("Client ID: " + uid);

// Create a client instance
var client = new Paho.Client(hostname, port, uid);
// set callback handlers
client.onConnectionLost = onConnectionLost;

client.onMessageArrived = onMessageArrived;


// connect the client
client.connect({onSuccess: onConnect, reconnect:true, keepAliveInterval: 10, timeout: 10});

function onConnect() {
  connected = true;
  notificationConnectionSuccess();
    // Once a connection has been made, make a subscription and send a message.
    // message = new Paho.Message("Hello");
    // client.subscribe("World");
    // console.log(message);
    // message.destinationName = destination;
    // client.send(message);
    sendToDefaultDestination("new");
    checkName();
}

// called when the client loses its connection
function onConnectionLost(responseObject) {
    if (responseObject.errorCode !== 0) {
        console.error("onConnectionLost:" + responseObject.errorMessage);
        connected = false;
        notificationConnectionLost()
    }
}

// called when a message arrives
function onMessageArrived(message) {
    console.log("onMessageArrived:" + message.payloadString);
}

var connected = false;
var connectionSnackbar = undefined;

function notificationConnectionLost() {
  if (!connected) {
    connectionSnackbar = $.snackbar(
      {
        content:"Stratili sme spojenie",
        timeout:0,
        onClose: notificationConnectionLost
      }
    );
  }
}

function notificationConnectionSuccess() {
  if (connectionSnackbar) {
    connectionSnackbar.snackbar("hide");
  }
}

function sendOrientationVector(leftRight, frontBack, rotation) {
    message = "move:" + leftRight + "," + frontBack + "," + rotation;
    // client.send(new Paho.Message(message));
    sendToDefaultDestination(message);
}

function sendDisplayClickCoordinates(x, y) {
    message = "click:" + x + "," + y;
    // client.send(new Paho.Message(message));
    sendToDefaultDestination(message);
}

function sendDisplayClickOnHalfOfDisplay(isRight) {
    message = "click:";
    if (isRight) {
        message += "right";
    } else {
        message += "left";
    }
    // client.send(new Paho.Message(message));
    sendToDefaultDestination(message);
}

function sendClick() {
    message = "click";
    // client.send(new Paho.Message(message));
    sendToDefaultDestination(message);
}

function msgInit(message) {
    msg = new Paho.Message(message);
    msg.destinationName = destination;
    return msg;
}

function sendToDefaultDestination(msg) {
    message = msgInit(msg);
    client.send(message);
}

function fire() {
    message = msgInit("fire");
    client.send(message);
}

function sendName(name) {
    sendToDefaultDestination("name:" + name);
}