(function ($) {
    $.fn.mazeBoard = function (
        mazeData, // the matrix containing the maze cells
        startRow, startCol, // initial position of the player
        exitRow, exitCol, // the exit position
        playerImage, // player's icon (of type Image)
        exitImage // exit's icon (of type Image)
        ) {
        var mazeObj = {
            
            mazeData: mazeData,
            startRow: startRow,
            startCol: startCol,
            exitRow: exitRow,
            exitCol: exitCol,
            playerImage: playerImage,
            exitImage: exitImage,
            isEnabled: true,

            //functions
            Move : function () {
                
            },

            Draw : function () {

            }
        }
    }
}) (jQuery)