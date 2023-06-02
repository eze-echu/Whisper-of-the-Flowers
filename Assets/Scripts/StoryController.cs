using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    //[SerializeField] List<string> _requieredSubject;
    [SerializeField] List<string> _history;

    int _currentStory;
    bool _giveGood;
    bool _giveBad;


    [SerializeField] DialogueController _dialogueController;
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
      


        if (subject == "Decrease_of_Love")//_requieredSubject[_currentStory] && _currentStory < _history.Count && _currentStory < _requieredSubject.Count)
        {
            _currentStory++;
            _giveBad = true;
            //_partCycleController.PlayParticle(0);
            if (_giveGood && _giveBad)
            {
                EndMhe();
                return;
            }
            if(_currentStory < 3)
            {
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else
            {
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory], true);
            }
            _currentStory++;
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
            _currentStory += 2;
            _giveGood = true;
            //_partCycleController.PlayParticle(1);
            if (_giveGood && _giveBad)
            {
                EndMhe();
                return;
            }
            if (_currentStory < 3)
            {
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory]);
            }
            else
            {
                NextClient();
                _dialogueController.ShowSpecificRequest(_history[_currentStory], true);
            }


            print(_giveGood);
        }
       
      

    }

    public void EndMhe()
    {
        _currentStory = 5;
        NextClient();
        _dialogueController.ShowSpecificRequest(_history[_currentStory], true);

    }

    public void NextClient()
    {
        _clientController.ChangeClient(_currentStory);
    }
}


