using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] List<string> _requieredSubject;
    [SerializeField] List<string> _history;

    int _currentStory;
    bool _goodOrBad;


    [SerializeField] DialogueController _dialogueController;

    public void Start()
    {
        _currentStory = 0;
        _dialogueController.ShowSpecificQuest(_history[_currentStory]);
        _currentStory++;

    }



    public void HandleStory(string subject, int intentValues, int formalityValues)
    {
        if (subject == _requieredSubject[_currentStory] && _currentStory < _history.Count && _currentStory < _requieredSubject.Count)
        {

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


           //_dialogueController.ShowSpecificQuest(_history[_currentStory]);
           //_currentStory++;
        }
        else
        {
            Debug.Log("Mal");
        }
      

    }
}


