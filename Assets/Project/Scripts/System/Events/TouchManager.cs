using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour {
	void Update () {
        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        || (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject()))
        {
            //Debug.Log("Clicked on the UI");
            TouchEvents.CanvasTouch();
        }
        else
        {
            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                || (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()))
            {
                //Debug.Log("Clicked under UI");
                TouchEvents.CanvasUnTouch();
            }
        }
    }

    //---смотрим где мы
    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 objectPosition = eventData.pressEventCamera.ScreenToWorldPoint(eventData.position);
        Debug.Log("objectPosition = " + objectPosition);
        GraphicRay(eventData);
    }

    void GraphicRay(PointerEventData eventData)
    {
        List<RaycastResult> objectsHit = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, objectsHit);
        int count = objectsHit.Count;
    }
}
