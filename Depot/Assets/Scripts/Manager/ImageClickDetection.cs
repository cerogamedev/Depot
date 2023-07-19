using UnityEngine;
using UnityEngine.UI;

public class ImageClickDetection : MonoBehaviour
{
    private RectTransform imageRectTransform;

    private void Start()
    {
        // Image'in RectTransform bileþenini alýyoruz
        imageRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        // Farenin sol tuþuna týklandýðýnda iþlem yapar
        if (Input.GetMouseButtonDown(0))
        {
            // Fare pozisyonu ekran koordinatlarý alýnýr
            Vector3 mousePosition = Input.mousePosition;

            // Image'in baþlangýç ve bitiþ koordinatlarýný dünya koordinatlarýnda buluyoruz
            Vector3[] corners = new Vector3[4];
            imageRectTransform.GetWorldCorners(corners);
            Vector3 imageMin = corners[0];
            Vector3 imageMax = corners[2];

            // Týklama koordinatlarý Image'in içinde mi diye kontrol ediyoruz
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
