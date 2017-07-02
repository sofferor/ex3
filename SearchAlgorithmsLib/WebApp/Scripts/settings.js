function SaveSettings() {
    localStorage.setItem("Rows", $("#mazeRows").val());
    localStorage.setItem("Cols", $("#mazeCols").val());
}