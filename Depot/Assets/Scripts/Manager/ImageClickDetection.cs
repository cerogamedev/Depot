using UnityEngine;
using UnityEngine.UI;

public class ImageClickDetection : MonoBehaviour
{
    private RectTransform imageRectTransform;

    private void Start()
    {
        imageRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;

            Vector3[] corners = new Vector3[4];
            imageRectTransform.GetWorldCorners(corners);
            Vector3 imageMin = corners[0];
            Vector3 imageMax = corners[2];

            if (mousePosition.x >= imageMin.x && mousePosition.x <= imageMax.x &&
                mousePosition.y >= imageMin.y && mousePosition.y <= imageMax.y)
            {
                return;
            }
            else
            {
                //GameObject canva = GameObject.Find("GetProdCanva");
                //canva.SetActive(false);
                transform.parent.gameObject.SetActive(false);

            }
        }
    }
}
