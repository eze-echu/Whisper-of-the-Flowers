using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class PopUpSpawner : MonoBehaviour
    {
        public GameObject popUpPrefab;
        private GameObject popUpInstance;

        public PopUp Spawn()
        {
            if (popUpInstance == null)
            {
                var popUp = Instantiate(popUpPrefab, transform, true);
                popUpInstance = popUp;
                var popUpObj = popUp.GetComponent<PopUp>();
                popUpObj.OpenUp();
                return popUpObj;
            }

            return null;
        }

        public void SpawnButton()
        {
            if (popUpInstance == null)
            {
                var popUp = Instantiate(popUpPrefab, transform, true);
                popUpInstance = popUp;
                var popUpObj = popUp.GetComponent<PopUp>();
                popUpObj.OpenUp();
            }

        }
    }
}