using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryController : MonoBehaviour
{
    [SerializeField] List<string> _requieredSubject;
    [SerializeField] List<string> _history;

    int _currentStory;

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
           _dialogueController.ShowSpecificQuest(_history[_currentStory]);
           _currentStory++;
        }
        else
        {
            Debug.Log("Mal");
        }
      

    }
}


