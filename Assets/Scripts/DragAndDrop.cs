using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 lastPosition;
    private Transform parent;
    private bool isDragging = false;
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        parent = GetComponentInParent<Transform>();
    }

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);

        print("ray");

        return hit;
    }

    private void OnMouseDown()
    {
        print("hit");
        //selectObject = hit.transform.gameObject.GetComponent<IDragable>().ObjectsToBeDraged(ref lastPosition);
        isDragging = true;
        lastPosition = transform.position;
        Cursor.visible = false;
        rb.transform.SetParent(null);
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = lastPosition.z - 2;
            rb.MovePosition(mousePosition);
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;

        Collider[] hitColliders = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z + 4), 0.1f);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.CompareTag("DropZone"))
            {
                transform.position = hitCollider.gameObject.transform.position;
                rb.transform.SetParent(hitCollider.transform);
                break;
            }
            else
            {
                StartCoroutine(ReturnToLastPosition(lastPosition, rb.transform));
                rb.transform.SetParent(parent);
            }
        }

        Cursor.visible = true;
    }



    private IEnumerator ReturnToLastPosition(Vector3 og, Transform obj)
    {
        float elapsed = 0f;
        float duration = 0.3f;

        Vector3 from = obj.position;

        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(from, og, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = og;
    }
    private void Update()
    {
        //CODIGO ANTERIOR
        /*
        if (Input.GetMouseButtonDown(0))
        {

            if (selectObject == null)
            {

                RaycastHit hit = CastRay();

                if (hit.collider != null && !hit.collider.CompareTag("drag"))
                {
                    return;
                }

                selectObject = hit.collider.gameObject;
                lastPosition = selectObject.transform.position;
                //DesactiveCollider(selectObject);

                //para no mostrar el cursor
                Cursor.visible = false;
            }
            else //if (Input.GetMouseButtonUp(0) && isDragging)
            {

                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                isDragging = false;
                //si se quiere en z cambiar el world position.y, en el 0
                selectObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
                //ActivateCollider(selectObject);
                selectObject = null;
                
                //para mostrar el cursor
                Cursor.visible = true;

              


            }
        }

        if (selectObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            //isDragging = true;
            //si se quiere en z cambiar el world position.y, en el 0
            selectObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, -2);
        }

        */

        /*if (Input.GetMouseButtonDown(0))
        {

            RaycastHit hit = CastRay();
            if (hit.collider != null && hit.collider.CompareTag("drag"))
            {
                selectObject = hit.transform.gameObject.GetComponent<IDragable>().ObjectsToBeDraged(ref lastPosition);
                isDragging = true;
                Cursor.visible = false;
                selectObject.transform.SetParent(null);
            }

        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, -2);


        }


        if (Input.GetMouseButtonUp(0) && isDragging)
        {

            isDragging = false;
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectObject.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);


            Vector3 screenPos = Camera.main.WorldToScreenPoint(selectObject.transform.position);
            if (screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height || !selectObject.GetComponent<IDragable>().WasUsed())
            {
                StartCoroutine(ReturnToLastPosition(lastPosition, selectObject));
            }

            selectObject.transform.SetParent(parent);
            parent = null;
            selectObject = null;
            Cursor.visible = true;
        }*/



    }

}