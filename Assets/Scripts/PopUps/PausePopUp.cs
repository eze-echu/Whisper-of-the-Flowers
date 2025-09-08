using Systems;
using UnityEngine;

namespace PopUps
{
    public class PausePopUp: MonoBehaviour
    {
        public void Resume()
        {
            GameState.Instance.UnpauseMenuClose();

        }
    }
}