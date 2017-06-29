(function($) {
    $.fn.mazeBoard = function(mazeData,
        mazeString,
        startRow,
        startCol,
        exitRow,
        exitCol,
        cols,
        rows) {
        console.log("in plugin");
        var mazeObj = {
            mazeData: mazeData,
            mazeString: mazeString,
            startRow: startRow,
            startCol: startCol,
            exitRow: exitRow,
            exitCol: exitCol,
            rows: rows,
            cols: cols,
            currRow: startRow,
            currCol: startCol,
            cellWidth: 0,
            cellHight: 0,
            isEnabled: true,
            Draw: function() {
                console.log("in draw");
                var canvas = document.getElementById("boardCanvas");
                var context = canvas.getContext("2d");
                context.clearRect(0, 0, canvas.width, canvas.height);
                this.cellWidth = canvas.width / cols;
                this.cellHight = canvas.height / rows;
                console.log("cellWidth: " + this.cellWidth);
                console.log("cellHight: " + this.cellHight);
                var x = 0;
                var y = 0;
                for (var i = 0; i < this.mazeString.length; i++) {
                    if (i % cols == 0 && i != 0) {
                        x = 0;
                        y++;
                    }
                    if (this.mazeString[i] === "1") {
                        context.fillRect(this.cellWidth * x, this.cellHight * y, this.cellWidth, this.cellHight);
                    }
                    x++;
                }
                var p = document.getElementById("startImg");
                var e = document.getElementById("endImg");
                console.log(p);
                console.log(e);
                context.drawImage(p, startCol * this.cellWidth, startRow * this.cellHight, this.cellWidth, this.cellHight);
                context.drawImage(e, exitCol * this.cellWidth, exitRow * this.cellHight, this.cellWidth, this.cellHight);

            },
            Move: function (direction) {
                console.log("in move()");
                var canvas = document.getElementById("boardCanvas");
                var context = canvas.getContext("2d");
                var p = document.getElementById("startImg");
                //oneDimensionalArray[row * columns + column]
                if (this.currRow == this.exitRow && this.currCol == this.exitCol) {
                    return;
                }
                switch (direction) {
                    case "right":
                        console.log("currCol = " + this.currCol);
                        console.log("val in maze: " + mazeString[this.currRow * cols + (this.currCol + 1)]);
                        if (this.currCol + 1 < this.cols && mazeString[this.currRow * cols + (this.currCol + 1)] == 0) {
                            context.clearRect(this.currCol * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            context.drawImage(p, (this.currCol + 1) * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            this.currCol = this.currCol + 1;
                            console.log("newCurrCol = " + this.currCol);
                        }
                        break;

                    case "left":
                        console.log("currCol = " + this.currCol);
                        console.log("val in maze: " + mazeString[this.currRow * cols + (this.currCol - 1)]);
                        if (this.currCol - 1 > -1 && mazeString[this.currRow * cols + (this.currCol - 1)] == 0) {
                            context.clearRect(this.currCol * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            context.drawImage(p, (this.currCol - 1) * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            this.currCol = this.currCol - 1;
                            console.log("newCurrCol = " + this.currCol);
                        }
                        break;

                    case "up":
                        console.log("currRow = " + this.currRow);
                        console.log("val in maze: " + mazeString[(this.currRow - 1) * cols + this.currCol]);
                        if (this.currRow - 1 > -1 && mazeString[(this.currRow - 1) * cols + this.currCol] == 0) {
                            context.clearRect(this.currCol * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            context.drawImage(p, this.currCol * this.cellWidth, (this.currRow - 1) * this.cellHight, this.cellWidth, this.cellHight);
                            this.currRow = this.currRow - 1;
                            console.log("newCurrRow = " + this.currRow);
                        }
                        break;

                    case "down":
                        console.log("currRow = " + this.currRow);
                        console.log("val in maze: " + mazeString[(this.currRow + 1) * cols + this.currCol]);
                        if (this.currRow + 1 < this.rows && mazeString[(this.currRow + 1) * cols + this.currCol] == 0) {
                            context.clearRect(this.currCol * this.cellWidth, this.currRow * this.cellHight, this.cellWidth, this.cellHight);
                            context.drawImage(p, this.currCol * this.cellWidth, (this.currRow + 1) * this.cellHight, this.cellWidth, this.cellHight);
                            this.currRow = this.currRow + 1;
                            console.log("newCurrRow = " + this.currRow);
                        }
                        break;

                    default:
                        break;
                }
                
                if (this.currRow == this.exitRow && this.currCol == this.exitCol) {
                    setTimeout(function () { alert("Congrats !!! You Won !\nYou can start a New Game."); }, 100);
                }
            },
            Solve: function (solution) {
                this.currCol = this.startCol;
                this.currRow = this.startRow;
                this.Draw();
                var solString = solution.Solution;

                for (var i = 0; i < solString.length; i++) {
                    switch (solString[i]) {
                    case 0:
                        this.Move("left");
                        break;
                    case 1:
                        this.Move("right");
                        break;
                    case 2:
                        this.Move("up");
                        break;
                    case 3:
                        this.Move("down");
                        break;
                    default:
                        break;
                    }
                }

            }
        }
        return mazeObj;
    }
})(jQuery)