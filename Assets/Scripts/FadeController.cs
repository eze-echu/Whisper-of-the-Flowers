using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    private string SceneName;

    public void Start()
    {
        if (GameManager.instance.Fc == null) GameManager.instance.Fc = this;
        else Destroy(gameObject);
    }

    public void FadeToLevel(string Scene)
    {
        SceneName = Scene;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeCompleteLevel()
    {
        EventSystems.instance.LoadScene(SceneName);
    }
}
