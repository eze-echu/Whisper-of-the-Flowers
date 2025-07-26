using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStore : MonoBehaviour
{
   public GameObject storeObject;
    public Transform openPosition;
    public Transform closedPosition;
    public float animationDuration = 0.5f;

    private bool isOpen = false;
    private Coroutine currentAnimation;

    public void ToggleStore()
    {
        if (storeObject == null || openPosition == null || closedPosition == null) return;

        Vector3 startPos = storeObject.transform.position;
        Quaternion startRot = storeObject.transform.rotation;

        Vector3 endPos = isOpen ? closedPosition.position : openPosition.position;
        Quaternion endRot = isOpen ? closedPosition.rotation : openPosition.rotation;

        if (currentAnimation != null)
            StopCoroutine(currentAnimation);

        currentAnimation = StartCoroutine(AnimatePositionRotation(startPos, endPos, startRot, endRot, animationDuration));
        isOpen = !isOpen;
    }

    private IEnumerator AnimatePositionRotation(Vector3 startPos, Vector3 endPos, Quaternion startRot, Quaternion endRot, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            storeObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            storeObject.transform.rotation = Quaternion.Lerp(startRot, endRot, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        storeObject.transform.position = endPos;
        storeObject.transform.rotation = endRot;
    }
}
