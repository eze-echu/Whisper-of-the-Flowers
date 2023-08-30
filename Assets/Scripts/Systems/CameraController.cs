using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public List<Transform> cameraPositions;
    private int currentPositionIndex = 0;
    public float movementSpeed = 5f;

    private bool finishAnimation = true;


    private void Update()
    {
#if UNITY_EDITOR
        if (finishAnimation && (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)))
        {
            int direction = Input.GetKeyDown(KeyCode.LeftArrow) ? -1 : 1;
            ChangeCameraPosition(direction);
        }
#endif
    }


    public void MoveToNextPosition()
    {
        ChangeCameraPosition(1);
    }

    public void MoveToPreviousPosition()
    {
        ChangeCameraPosition(-1);
    }

    private void ChangeCameraPosition(int offset)
    {
        if (!finishAnimation)
            return;

        currentPositionIndex += offset;
        currentPositionIndex = Mathf.Clamp(currentPositionIndex, 0, cameraPositions.Count - 1);

        StartCoroutine(SmoothMove(cameraPositions[currentPositionIndex]));
    }

    private IEnumerator SmoothMove(Transform target)
    {
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;
        float startTime = Time.time;
        finishAnimation = false;

        while (Time.time - startTime <= 1f / movementSpeed)
        {
            float t = (Time.time - startTime) * movementSpeed;
            transform.position = Vector3.Lerp(initialPosition, target.position, t);
            transform.rotation = Quaternion.Lerp(initialRotation, target.rotation, t);
            yield return null;
        }

        transform.position = target.position;
        transform.rotation = target.rotation;

        finishAnimation = true;
    }
}
