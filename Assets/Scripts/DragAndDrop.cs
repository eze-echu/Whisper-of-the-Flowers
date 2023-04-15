using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private GameObject selectObject;
    private Vector3 lastPosition;
    private bool isDragging = false;


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

        if (Input.GetMouseButtonDown(0))
        {
           
                RaycastHit hit = CastRay();
                if (hit.collider != null && hit.collider.CompareTag("drag"))
                {
                    selectObject = hit.collider.gameObject;
                    lastPosition = selectObject.transform.position;
                    isDragging = true;
                    Cursor.visible = false;
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
            selectObject = null;
            Cursor.visible = true;
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

        return hit;
    }

   
    
}