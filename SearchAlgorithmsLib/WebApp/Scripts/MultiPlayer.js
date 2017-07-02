// Declare a proxy to reference the hub
var multiHub = $.connection.MazeHub;
var mazeName = $("#mazeName").val();
var mazeRows = $("#mazeRows").val();
var mazeCols = $("#mazeCols").val();
// Create a function that the hub can call to broadcast messages
multiHub.client.GameList = function (gameList) {
    var games = $("#gameList");
    // Add the message to the page
    for (var i = 0; i < gameList.length; i++) {
        var option = document.createElement("option");
        option.text = gameList[i];
        games.add(option);
    }
};

// Start the connection
$.connection.hub.start().done(function () {
    $("#startGame").click(function () {
        multiHub.server.connect(mazeName);
        multiHub.server.StartGame(mazeName, mazeRows, mazeCols);
    });
});