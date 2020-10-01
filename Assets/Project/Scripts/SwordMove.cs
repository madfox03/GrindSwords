using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class SwordMove : MonoBehaviour {
    [Header("Input parameters:")]
    public float RotationSpeed = 1;

    [Space(15)]
    [Header("Renderer glow material:")]
    public Renderer RendererGlowMaterial;

    private bool isDown = true;
    private float angle;
    private Material glowMaterial;
    bool RotateEnabled = true;

    void OnDisable()
    {
        //---отписа от события
        Events.instance.RemoveListener<EventCanvasTouch>(CanvasTouch);
        Events.instance.RemoveListener<EventCanvasUnTouch>(CanvasUnTouch);
    }

    // Use this for initialization
    void Start () {
        //---подписываемся на события
        Events.instance.AddListener<EventCanvasTouch>(CanvasTouch);
        Events.instance.AddListener<EventCanvasUnTouch>(CanvasUnTouch);

        glowMaterial = RendererGlowMaterial.sharedMaterial;
        IEnumerator cor = MoveCor();
        StartCoroutine(cor);
    }

    /// <summary>
    /// обработка события нажатия на канву
    /// </summary>
    void CanvasTouch(EventCanvasTouch arg)
    {
        Debug.Log("Camera - Clicked on the UI");
        RotateEnabled = false;
    }

    /// <summary>
    /// обработка события нажатия вне канвы
    /// </summary>
    void CanvasUnTouch(EventCanvasUnTouch arg)
    {
        Debug.Log("Camera - Clicked under UI");
        RotateEnabled = true;
    }


    IEnumerator MoveCor()
    {
        for (;;)
        {
            isDown = !isDown;
            DOVerticalMove();
            yield return new WaitForSeconds(2f);
        }
    }

    private void DOVerticalMove()
    {
        if (isDown)
            transform.DOLocalMoveY(0.02f, 2f);
        else
            transform.DOLocalMoveY(-0.02f, 2f);
    }

    void Update()
    {
        if (RotateEnabled)
        {
            if (Input.touches.Length > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                //---check UI
                EventSystem eventSystem = EventSystem.current;
                if (eventSystem.IsPointerOverGameObject())
                    return;

                Debug.Log("GetTouch");
                angle += Input.GetTouch(0).deltaPosition.y;
                transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
            }

            if (Input.GetMouseButton(0))
            {
                //---check UI
                EventSystem eventSystem = EventSystem.current;
                if (eventSystem.IsPointerOverGameObject())
                    return;

                Debug.Log("GetMouseButtonDown");
                angle = -Input.GetAxis("Mouse X") * RotationSpeed;
                transform.rotation *= Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}
