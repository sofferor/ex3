var maze;
function StartGame() {
    console.log("in StartGame()");
    var url = "/api/SinglePlayer?" +
        "name=" + $("#mazeName").val() +
        "&" +
        "rows=" + $("#mazeRows").val() +
        "&" +
        "cols=" + $("#mazeCols").val();
    console.log(url);
    $.getJSON(url).done(function (data) {
        console.log(data);
        maze = $.fn.mazeBoard(data, data.Maze, data.Start.Row, data.Start.Col, data.End.Row, data.End.Col, data.Cols, data.Rows, document.getElementById("boardCanvas"));
        console.log(maze);
        maze.Draw();
        console.log("after Draw");
    });
};

$(this).keydown(function (e) {
    console.log("in keydown()");
    switch (e.which) {
        case 37:
            maze.Move("left");
            break;
        case 38:
            maze.Move("up");
            break;
        case 39:
            maze.Move("right");
            break;
        case 40:
            maze.Move("down");
            break;
        default:
            break;
    }
});

function SolveMaze() {
    console.log("in SolveMaze()");
    var url = "/api/SinglePlayer/" +
       $("#mazeName").val() +
        "/" +
         $("#mazeAlg").val();
    console.log(url);
    $.getJSON(url).done(function (data) {
        console.log(data);
        maze.Solve(data);

        maze.currCol = this.startCol;
        maze.currRow = this.startRow;
        maze.Draw();

        console.log("after Solve");
    });
}