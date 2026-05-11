using UnityEngine;

public class SimpleMouseLook6 : MonoBehaviour
{
    public float mouseSensitivityX = 200f;
    public float mouseSensitivityY = 200f;
    public float minPitch = -80f;
    public float maxPitch = 80f;
    public Camera playerCamera;

    private float _yaw;   // обертання корпуса по Y
    private float _pitch; // нахил камери по X

    void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _yaw = transform.eulerAngles.y;
        if (playerCamera != null)
        {
            _pitch = playerCamera.transform.localEulerAngles.x;
        }
    }

    void Update()
    {
        if (playerCamera == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        // Обертаємо гравця (Y)
        _yaw += mouseX;
        transform.rotation = Quaternion.Euler(0f, _yaw, 0f);

        // Обертаємо камеру (X)
        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);
        playerCamera.transform.localRotation = Quaternion.Euler(_pitch, 0f, 0f);
    }
}