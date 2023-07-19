using UnityEngine;
using UnityEngine.UI;

public class ImageClickDetection : MonoBehaviour
{
    private RectTransform imageRectTransform;

    private void Start()
    {
        // Image'in RectTransform bile�enini al�yoruz
        imageRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Farenin sol tu�una t�kland���nda i�lem yapar
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonu ekran koordinatlar� al�n�r
            Vector3 mousePosition = Input.mousePosition;

            // Image'in ba�lang�� ve biti� koordinatlar�n� d�nya koordinatlar�nda buluyoruz
            Vector3[] corners = new Vector3[4];
            imageRectTransform.GetWorldCorners(corners);
            Vector3 imageMin = corners[0];
            Vector3 imageMax = corners[2];

            // T�klama koordinatlar� Image'in i�inde mi diye kontrol ediyoruz
            if (mousePosition.x >= imageMin.x && mousePosition.x <= imageMax.x &&
                mousePosition.y >= imageMin.y && mousePosition.y <= imageMax.y)
            {
                return;
            }
            else
            {
                GameObject canva = GameObject.Find("GetProdCanva");
                canva.SetActive(false);
            }
        }
    }
}
