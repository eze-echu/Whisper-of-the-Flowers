using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StoryControllerOLD : MonoBehaviour
{
    //[SerializeField] List<string> _requieredSubject;
    [SerializeField] List<string> _history;

    int _currentStory;
    bool _giveGood;
    bool _giveBad;
    public TextMeshProUGUI Result;

    [SerializeField] DialogueControllerOLD _dialogueController;
    [SerializeField] ClientController _clientController;
   

    public void Start()
    {
        _currentStory = 0;
        _dialogueController.ShowSpecificRequest(_history[_currentStory]);
        NextClient();
        //_currentStory++;

        _giveGood = false;
        _giveBad = false;

    }
#if UNITY_EDITOR
    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            HandleStory("Decrease_Of_Love", 1, 1);
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            HandleStory("Love", 1, 1);
        }
    }
#endif


    public void HandleStory(string subject, int intentValues, int formalityValues)
    {
      


        if (subject == "Decrease_of_Love" || subject == "Mourning" || subject == "Jealousy" || subject == "Hatred")//_requieredSubject[_currentStory] && _currentStory < _history.Count && _currentStory < _requieredSubject.Count)
        {
            if(_currentStory == 1 && !_giveGood)
            {
                _currentStory++;
            }
            else if (_currentStory == 3 && !_giveGood)
            {
                _currentStory++;
                _currentStory++;
            }
            _currentStory++;
            _giveBad = true;
            //_partCycleController.PlayParticle(0);
            if (_giveGood && _giveBad && _currentStory >= 5)
            {
                EndMhe();
                return;
            }
            else if(_giveGood && _giveBad)
            {
                _currentStory++;
                _currentStory++;
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else if(_currentStory < 5)
            {
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else
            {
                NextClient();
                Result.text = "Resultado Malo";
                _dialogueController.ShowSpecificRequest(_history[6], true);
            }
            print(_giveBad);
            //IGNORAR
            /*
            if (intentValues > 0 && formalityValues > 0)
            {
                _dialogueController.ShowSpecificQuest(_history[_currentStory]);
                _currentStory += 2;
            }
            else if(intentValues <= 0 && formalityValues > 0 || intentValues > 0 && formalityValues <= 0)
            {
                _currentStory += 2;
                _dialogueController.ShowSpecificQuest(_history[_currentStory]);
                _currentStory++;
                _goodOrBad = true;
            }
            else
            {
                _currentStory += 3;
                _dialogueController.ShowSpecificQuest(_history[_currentStory]);
                _goodOrBad = false;
            }
            */


            //_dialogueController.ShowSpecificQuest(_history[_currentStory]);
            //_currentStory++;
        }
        else 
        {
            if(_currentStory == 3 && _giveBad)
            {
                _currentStory++;
            }
            _currentStory ++;
            _giveGood = true;
            //_partCycleController.PlayParticle(1);
            if (_giveGood && _giveBad && _currentStory >= 5)
            {
                EndMhe();
                return;
            }
            else if (_giveGood && _giveBad)
            {
                _currentStory++;
                _currentStory++;
                _currentStory++;
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else if (_currentStory < 5)
            {
                _currentStory++;
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else
            {
                NextClient();
                Result.text = "Resultado Bueno";
                _dialogueController.ShowSpecificRequest(_history[7], true);
            }


            print(_giveGood);
        }
       
      

    }

    public void EndMhe()
    {
        _currentStory = 8;
        NextClient();
        Result.text = "Resultado Neutro";
        _dialogueController.ShowSpecificRequest(_history[_currentStory], true);

    }

    public void NextClient()
    {
        _clientController.ChangeClient(_currentStory);
    }
}


