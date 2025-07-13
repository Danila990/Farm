using Code;

namespace Code
{
    public interface IGridService
    {
        public IGridMap GetGridMap();
        public void CreateGrid();
        public void RestartGrid();
    }
}