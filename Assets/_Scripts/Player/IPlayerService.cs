using Code;

namespace Code
{
    public interface IPlayerService 
    {
        public Player GetPlayer();
        public void CreatePlayer();
        public void StartPlayer();
        public void StopPlayer();
        public void DeadPlayer();
        public void RestartPlayer();
    }
}