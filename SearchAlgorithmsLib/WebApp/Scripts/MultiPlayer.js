// Declare a proxy to reference the hub
var multiHub = $.connection.mazeHub;
var mazeName;
var mazeRows;
var mazeCols;
var myMaze, otherMaze;
// Create a function that the hub can call to broadcast messages
multiHub.client.gameList = function (gameList) {
    console.log("in gameList()");
    $("#gameList").html("");
    var games = document.getElementById("gameList");
    // Add the message to the page
    for (var i = 0; i < gameList.length; i++) {
        var option = document.createElement("option");
        option.text = gameList[i];
        games.add(option);
    }
};

multiHub.client.startFromJoin = function (data) {
    console.log("in function startGameFromJoin");
    console.log(data);
    myMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas1"));
    otherMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas2"));
    console.log(maze);
    myMaze.Draw();
    otherMaze.Draw();
    console.log("after Draw mazes");
};

// Start the connection
$.connection.hub.start().done(function () {
    $("#startGame").click(function () {
        mazeName = $("#mazeName").val();
        mazeRows = $("#mazeRows").val();
        mazeCols = $("#mazeCols").val();
        console.log("in startGame()");
        document.title = mazeName;
        multiHub.server.connect(mazeName);
        multiHub.server.startGame(mazeName, mazeRows, mazeCols);
    });

    $("#gameList").click(function () {
        console.log("in gameList()");
        multiHub.server.gameList();
    });

    $("#joinButton").click(function () {
        var gameToJoin = $("#gameList").val();
        multiHub.server.joinGame(gameToJoin);
        document.title = gameToJoin;
        console.log("in function startGameFromJoin");
        console.log(data);
        myMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas1"));
        otherMaze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas2"));
        console.log(maze);
        myMaze.Draw();
        otherMaze.Draw();
        console.log("after Draw mazes");
    });
});

