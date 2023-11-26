using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    private string SceneName;
    public TextMeshProUGUI texto;


    public void Start()
    {
        if (GameManager.instance.Fc == null) GameManager.instance.Fc = this;
        else Destroy(gameObject);
    }

    public void FadeToLevel(string Scene)
    {
        texto.text = "";
        SceneName = Scene;
        animator.SetTrigger("FadeOut");
    }

    /*
    public void FadeInAndOut(string Message)
    {
        texto.text = Message;

    }
    */

    public void OnFadeCompleteLevel()
    {
        texto.text = "";
        EventSystems.instance.LoadScene(SceneName);
    }

    public IEnumerator FadeInAndOutCoroutine(string message)
    {
        // Setea el mensaje
        texto.text = message;
        print("entro");
        // Inicia la animación
        animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, -1, 0f);
        animator.Update(0f);


        animator.SetTrigger("FadeInAndOut");

        // Espera a que termine la animación
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Limpia el mensaje y carga la siguiente escena
        //texto.text = "";
        


    }

}
