using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera inputCamera;
    [SerializeField] private TraySpawner traySpawner;

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
#elif UNITY_IOS || UNITY_ANDROID
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleInput(touch.position);
            }
        }
#endif
    }

    private void HandleInput(Vector2 screenPos)
    {
        Ray ray = inputCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out RailObject railObj))
            {
                TraySpawner.Instance?.NotifyTrayObjects(railObj);
            }
        }
    }
}