// Declare a proxy to reference the hub
var multiHub = $.connection.MazeHub;
var mazeName = $("#mazeName").val();
var mazeRows = $("#mazeRows").val();
var mazeCols = $("#mazeCols").val();
var myMaze, otherMaze;
// Create a function that the hub can call to broadcast messages
multiHub.client.GameList = function (gameList) {
    console.log("in gameList()");
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

    $("#gameList").click(function() {
        multiHub.server.GameList();
    });
});

multiHub.client.StartGameFromJoin = function (data) {
    console.log(data);
    myMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas1"));
    otherMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas2"));
    console.log(maze);
    myMaze.Draw();
    otherMaze.Draw();
    console.log("after Draw mazes");
};