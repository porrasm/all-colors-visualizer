using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorImage : MonoBehaviour {

    private Color32 color = Color.clear;

    [SerializeField]
    private Text rgbText, colorText, hueText;

    private Image image;

    private void Start() {

        image = GetComponent<Image>();

        if (color.a == 0) {
            return;
        }

        SetUI();
    }

    public void SetColor(Color32 color) {
        this.color = color;
        SetUI();
    }

    private void SetUI() {
        if (ColorSystem.Detailed) {
            rgbText.text = "(" + color.r + " ," + color.g + ", " + color.b + ")";
            colorText.text = ColorIdentifier.ColorPrimary(color);
            hueText.text = ColorIdentifier.ColorHue(color);
        }
        image.color = color;
    }

}
