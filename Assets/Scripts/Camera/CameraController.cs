using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float sensitivity = 1f; // Adjust the speed of the camera movement
    public float minX = -10f; // Set the minimum X position
    public float maxX = 10f;  // Set the maximum X position

    private Camera mainCamera;  // Reference to the Camera component

    void Start()
    {
        mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
        float mouseX = Input.mousePosition.x;
        float cameraX = mainCamera.transform.position.x;

        float desiredX = cameraX + (mouseX - (Screen.width / 2)) * sensitivity * Time.deltaTime;
        desiredX = Mathf.Clamp(desiredX, minX, maxX);

        mainCamera.transform.position = new Vector3(desiredX, mainCamera.transform.position.y, mainCamera.transform.position.z);
    }
}
