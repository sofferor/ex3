using MazeLib;

namespace Server {
    public interface IModel {
        Maze GenerateMaze(string name, int rows, int cols);
    }
}