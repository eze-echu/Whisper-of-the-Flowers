using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    private Vector3 lastPosition;
    private quaternion lastRotation;
    private Transform parent;
    private bool isDragging = false;
    private IDragable dragable;
    private Canvas canvas;

    private delegate void Drops();
    private Drops onDrop; // the event handler for the drop event.


    
    private void Start()
    {
        parent = transform.parent;
        dragable = GetComponent<IDragable>();
        Canvas[] temp;
        temp = FindObjectsOfType<Canvas>();
        foreach(var a in temp)
        {
            if (a.gameObject.gameObject.tag == "UI")
            {
                canvas = a;
            }
        }
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
        if (dragable.canBeDragged)
        {
            parent = transform.parent ?? null;
            isDragging = true;
            lastPosition = transform.position;
            lastRotation = transform.rotation;
            Cursor.visible = false;
            transform.SetParent(null);
            dragable.ObjectsToBeDraged(ref lastPosition);
            //GetComponent<IDragable>().canBeDragged = false;
        }
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = GameManager.instance.DragCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = GameManager.instance.distanceFromCamera;
            transform.position = (mousePosition);
        }
    }
    Vector2 UnscalePosition(Vector2 vec)
    {
        Vector2 referenceResolution = canvas.GetComponent<CanvasScaler>().referenceResolution;
        Vector2 currentResolution = new Vector2(Screen.width, Screen.height);

        float widthRatio = currentResolution.x / referenceResolution.x;
        float heightRatio = currentResolution.y / referenceResolution.y;

        float ratio = Mathf.Lerp(heightRatio, widthRatio, canvas.GetComponent<CanvasScaler>().matchWidthOrHeight);

        return vec / ratio;
    }

    private void OnMouseUp()
    {
        if (isDragging)
        {
            isDragging = false;
            bool dropped = false;
            Collider[] hitColliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1f);
            foreach (Collider hitCollider in hitColliders)
            {
                onDrop = delegate{
                    if (parent)
                    {
                        parent?.GetComponent<IOccupied>()?.RemoveAction();
                        parent.gameObject.tag = "DropZone";
                    }
                    parent = hitCollider.gameObject.transform;
                    //transform.SetParent(hitCollider.transform);
                    dropped = true;
                };
                print(hitCollider.tag);
                if (hitCollider.gameObject.CompareTag("DropZone"))
                {
                    bool a = hitCollider.transform.parent?.GetComponent<IDropZone>()?.DropAction(this.gameObject) ?? false;
                    bool b = hitCollider.GetComponent<IDropZone>()?.DropAction(this.gameObject) ?? false;
                    if(a || b){
                        onDrop();
                        break;
                    }
                    print(this.name);

                    //transform.position = hitCollider.gameObject.transform.position;
                    //transform.position = hitCollider.GetComponent<BucketOfFlowers>()?.OGflowerPosition ?? hitCollider.gameObject.transform.position;
                    
                }
            }
            if (!dropped)
            {
                transform.SetParent(parent);
                StartCoroutine(ReturnToLastPosition(lastPosition, transform, lastRotation));
            }

            Cursor.visible = true;
        }
    }



    private IEnumerator ReturnToLastPosition(Vector3 og, Transform obj, quaternion rot)
    {
        float elapsed = 0f;
        float duration = 0.3f;

        Vector3 from = obj.position;
        dragable.canBeDragged = false;
        while (elapsed < duration)
        {
            obj.transform.position = Vector3.Lerp(from, og, elapsed / duration);
            obj.transform.rotation = rot;
            elapsed += Time.deltaTime;
            yield return null;
        }
        dragable.canBeDragged = true;
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
