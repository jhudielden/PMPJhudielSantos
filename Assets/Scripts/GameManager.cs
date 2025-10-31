using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static MainGameLogic GameLogic;
    public class MainGameLogic
    {
        public delegate void OnGamePause();
        public OnGamePause onGamePause;
        public delegate void OnGameResume();
        public OnGameResume onGameResume;

        public void SetPauseState(bool state)
        {

        }
    }
}
