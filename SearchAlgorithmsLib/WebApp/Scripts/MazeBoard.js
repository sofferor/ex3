(function($) {
    $.fn.mazeBoard = function (mazeData, mazeString, startRow, startCol,
        exitRow, exitCol, playerImage, exitImage) {
        console.log("in plugin");
        var mazeObj = {
            mazeData: mazeData,
            mazeString: mazeString,
            startRow: startRow,
            startCol: startCol,
            exitRow: exitRow,
            exitCol: exitCol,
            playerImage: playerImage,
            exitImage: exitImage,
            isEnabled: true,
            Draw: function() {
                
            },
            Move: function() {
                
            }
        }
        return mazeObj;
    }
})(jQuery)