"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

connection.on("ReceiveNotification", (message) => {
    // Play notification sound
    const notificationSound = new Audio('/path/to/notification_sound.mp3');
    notificationSound.play();

    // Update notification counter
    updateNotificationCounter();

    // Display notification
    displayNotification(message);
});

connection.start()
    .then(() => {
        console.log('SignalR connected');
    })
    .catch((error) => {
        console.error('SignalR connection error: ' + error);
    });

function updateNotificationCounter() {
    const notificationCounter = document.getElementById('notificationCounter');
    const currentCount = parseInt(notificationCounter.innerText) || 0;
    notificationCounter.innerText = currentCount + 1;
}

function displayNotification(message) {
    // Implement notification display (e.g., using toast or custom UI)
    alert(message); // Example: Show an alert with the message
}

// Function to send notification to another user
function sendNotificationToUser(recipientUsername, message) {
    connection.invoke("SendNotificationToUser", recipientUsername, message)
        .catch((error) => {
            console.error('Error sending notification:', error);
        });
}
