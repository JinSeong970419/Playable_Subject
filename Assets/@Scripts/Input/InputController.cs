using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class InputController : MonoBehaviour
{
    public Camera inputCamera;
    public Action<Vector3> OnClickPosition;

    public void Enable() => enabled = true;
    public void Disable() => enabled = false;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            HandleRay(Input.mousePosition);
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleRay(touch.position);
            }
        }
#endif
    }

    private void HandleRay(Vector2 screenPosition)
    {
        var ray = inputCamera.ScreenPointToRay(screenPosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var railObj = hit.collider.GetComponentInParent<RailObject>();
            OnClickPosition?.Invoke(hit.point);
        }
    }
}