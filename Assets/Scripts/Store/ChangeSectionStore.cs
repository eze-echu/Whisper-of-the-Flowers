using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;

public class ChangeSectionStore : MonoBehaviour
{
   public List<GameObject> sections;

    private int currentIndex = 0;

    private void Start()
    {
        // Asegurar que solo el primer objeto esté activo al inicio
        ActivateSection(currentIndex);
    }

    public void NextSection()
    {
        if (sections == null || sections.Count == 0)
            return;

        currentIndex = (currentIndex + 1) % sections.Count;
        ActivateSection(currentIndex);
    }

    private void ActivateSection(int index)
    {
        for (int i = 0; i < sections.Count; i++)
        {
            sections[i].SetActive(i == index);
        }
    }
}
