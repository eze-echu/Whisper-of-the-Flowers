using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Commands : MonoBehaviour
{

    public IDialogueController dialogueController;


    private void Start()
    {
        dialogueController = FindObjectOfType<DialogueControllerOLD>();
    }

    public void Accept()
    {
        ICommand command = new AcceptCommand("Aceptar");
        command.Execute();
    }

    public void Decline()
    {
        ICommand command = new DeclineCommand("Declinar");
        command.Execute();
        dialogueController.ShowRandomRequest();
    }

    
}

public class AcceptCommand : ICommand
{
    private readonly string response;

    public AcceptCommand(string response)
    {
        this.response = response;
    }

    public void Execute()
    {
        Debug.Log(response);
    }

}

public class DeclineCommand : ICommand
{
    private readonly string response;

    public DeclineCommand(string response)
    {
        this.response = response;
    }

    public void Execute()
    {
        Debug.Log(response);
    }

}
