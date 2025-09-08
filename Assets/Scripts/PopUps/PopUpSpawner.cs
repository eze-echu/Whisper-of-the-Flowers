using UnityEngine;
using UnityEngine.UI;

namespace PopUps
{
    public class PopUpSpawner : MonoBehaviour
    {
        public GameObject popUpPrefab;

        public PopUp Spawn()
        {
            var popUp = Instantiate(popUpPrefab, transform, true);
            var popUpObj =  popUp.GetComponent<PopUp>();
            popUpObj.OpenUp();
            return popUpObj;

        }

        public void SpawnButton()
        {
            var popUp = Instantiate(popUpPrefab, transform, true);
            var popUpObj = popUp.GetComponent<PopUp>();
            popUpObj.OpenUp();
        }
    }
}