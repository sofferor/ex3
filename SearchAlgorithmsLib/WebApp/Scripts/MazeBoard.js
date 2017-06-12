(function($) {
    $.fn.mazeBoard = function (mazeData, startRow, startCol,
        exitRow, exitCol, playerImage, exitImage) {
        var mazeObj = {
            mazeData: mazeData,
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