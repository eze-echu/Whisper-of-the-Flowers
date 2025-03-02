using System.Collections;
using System.Collections.Generic;
using Systems;
using UnityEngine;
using TMPro;

public class FadeController : MonoBehaviour
{
    private static readonly int FadeInAndOut = Animator.StringToHash("FadeInAndOut");
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");
    public Animator animator;
    private string _sceneName;
    public TextMeshProUGUI texto;


    public void Start()
    {
        if (GameManager.instance.Fc == null) GameManager.instance.Fc = this;
        else Destroy(gameObject);
    }

    public void FadeToLevel(string scene)
    {
        texto.text = "";
        _sceneName = scene;
        animator.SetTrigger(FadeOut);
    }

    /*
    public void FadeInAndOut(string Message)
    {
        texto.text = Message;

    }
    */

    public IEnumerator FadeInAndOutCoroutine(string message)
    {
        // Setea el mensaje
        texto.text = message;
        GameState.PauseGame();
        print("entro");
        // Inicia la animaci�n
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
        animator.Update(0f);


        animator.SetTrigger(FadeInAndOut);

        // Espera a que termine la animaci�n
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        GameState.ResumeGame();
        // Limpia el mensaje y carga la siguiente escena
        //texto.text = "";
        


    }

    public IEnumerator StartFadeIn(string message)
    {
        texto.text = message;
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, layer: -1, normalizedTime: 0f);
        animator.Update(Time.deltaTime);
        GameState.PauseGame();
        
        animator.SetBool(FadeOut, true);
        
        yield return new WaitForSeconds(5);
        
        animator.SetBool(FadeOut, false);
        animator.SetBool(FadeIn, true);
        
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        
        GameState.ResumeGame();
    }

    public IEnumerator StartFadeOut()
    {
        yield return null;
    }

}
