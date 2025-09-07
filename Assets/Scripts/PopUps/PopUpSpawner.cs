using UnityEngine;

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
    }
}