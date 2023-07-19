using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kamera hareket h�z�
    public float zoomSpeed = 5f;  // Kamera yak�nla�ma/uzakla�ma h�z�
    public float borderThickness = 20f;  // Kenar alg�lama kal�nl���

    private Camera mainCamera;
    private float screenWidth;
    private float screenHeight;

    void Start()
    {
        mainCamera = Camera.main;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        Vector3 newPosition = mainCamera.transform.position + new Vector3(horizontalMovement, verticalMovement, 0f );
        mainCamera.transform.position = newPosition;

        // Mouse ekran�n sol kenar�nda m�?
        if (Input.mousePosition.x < borderThickness)
        {
            newPosition.x -= moveSpeed * Time.deltaTime;
        }

        // Mouse ekran�n sa� kenar�nda m�?
        if (Input.mousePosition.x > screenWidth - borderThickness)
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }

        // Mouse ekran�n �st kenar�nda m�?
        if (Input.mousePosition.y > screenHeight - borderThickness)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
        }

        // Mouse ekran�n alt kenar�nda m�?
        if (Input.mousePosition.y < borderThickness)
        {
            newPosition.y -= moveSpeed * Time.deltaTime;
        }

        // Kameran�n yeni pozisyonunu ayarla
        mainCamera.transform.position = newPosition;

        // Mouse tekerle�i yukar� kayd�r�ld�ysa kameray� yak�nla�t�r
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mainCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
        }

        // Mouse tekerle�i a�a�� kayd�r�ld�ysa kameray� uzakla�t�r
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
        }


    }
}
