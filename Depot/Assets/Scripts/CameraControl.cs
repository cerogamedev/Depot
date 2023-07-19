using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kamera hareket hýzý
    public float zoomSpeed = 5f;  // Kamera yakýnlaþma/uzaklaþma hýzý
    public float borderThickness = 20f;  // Kenar algýlama kalýnlýðý

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

        // Mouse ekranýn sol kenarýnda mý?
        if (Input.mousePosition.x < borderThickness)
        {
            newPosition.x -= moveSpeed * Time.deltaTime;
        }

        // Mouse ekranýn sað kenarýnda mý?
        if (Input.mousePosition.x > screenWidth - borderThickness)
        {
            newPosition.x += moveSpeed * Time.deltaTime;
        }

        // Mouse ekranýn üst kenarýnda mý?
        if (Input.mousePosition.y > screenHeight - borderThickness)
        {
            newPosition.y += moveSpeed * Time.deltaTime;
        }

        // Mouse ekranýn alt kenarýnda mý?
        if (Input.mousePosition.y < borderThickness)
        {
            newPosition.y -= moveSpeed * Time.deltaTime;
        }

        // Kameranýn yeni pozisyonunu ayarla
        mainCamera.transform.position = newPosition;

        // Mouse tekerleði yukarý kaydýrýldýysa kamerayý yakýnlaþtýr
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            mainCamera.orthographicSize -= zoomSpeed * Time.deltaTime;
        }

        // Mouse tekerleði aþaðý kaydýrýldýysa kamerayý uzaklaþtýr
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            mainCamera.orthographicSize += zoomSpeed * Time.deltaTime;
        }


    }
}
