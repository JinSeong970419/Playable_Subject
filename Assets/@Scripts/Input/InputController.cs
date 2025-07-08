using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera inputCamera;
    [SerializeField] private TraySpawner traySpawner;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleInput(Input.mousePosition);
        }
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