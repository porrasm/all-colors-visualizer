using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPlacer : MonoBehaviour {

    private ColorImage[,] colorImages;

    private GameObject colorImage;

    [SerializeField]
    private RectTransform imageParent;

    float width = 120;
    float height = 67.5f;

    // Use this for initialization
    void Start () {

        colorImages = new ColorImage[16, 16];

        InitializeImages();	
	}

    public void UpdateImage(int x, int y, Color32 color) {
        colorImages[x, y].SetColor(color);
    }

    private void InitializeImages() {

        colorImage = Resources.Load<GameObject>("ColorImage");

        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 16; x++) {
                SpawnImage(x, y);
            }
        }

    }
    private void SpawnImage(int x, int y) {

        GameObject newImage = Instantiate(colorImage);

        RectTransform t = newImage.GetComponent<RectTransform>();
        t.name = "Color Image (" + x + ", " + y + ")";

        t.SetParent(imageParent);
        t.localPosition = GetPosition(x, y);

        t.localScale = new Vector3(1, 1, 1);

        colorImages[x, y] = t.GetComponent<ColorImage>();

        if (!ColorSystem.Detailed) {
            foreach (Transform child in newImage.transform) {
                Destroy(child.gameObject);
            }
        }
    }

    private Vector2 GetPosition(int x, int y) {

        Vector2 position = new Vector2(width * 0.5f, -height * 0.5f);

        position.x += x * width;
        position.y -= y * height;

        return position;
    }
}
