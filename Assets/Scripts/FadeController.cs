using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public Animator animator;
    private string SceneName;

    // Update is called once per frame
    void Update()
    {
        
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
