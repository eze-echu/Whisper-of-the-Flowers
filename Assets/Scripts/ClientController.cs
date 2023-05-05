using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientController : MonoBehaviour
{
    [SerializeField] Image _imagePosition;
    [SerializeField] List<Sprite> _clients;
   
    public void ChangeClient(int currentClient)
    {
        _imagePosition.sprite = _clients[currentClient];
    }
    
}
