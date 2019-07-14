using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class ColorVisualizer : MonoBehaviour {

    public bool waitForFrames;

    Colors colors;
    ColorBlockGenerator generator;
    Color32 current;

    [SerializeField]
    private Image colorImage;

    [SerializeField]
    private Text colorName, colorHue, colorHex, colorValue, colorRGB;

    private void Start() {
        colors = GetComponent<Colors>();
        generator = GetComponent<ColorBlockGenerator>();
        StartCoroutine(Drawer());
    }

    private IEnumerator Drawer() {

        yield return null;

        //Stopwatch s = new Stopwatch();
        //s.Start();

        while (!colors.State.done) {

            //if (s.ElapsedMilliseconds > 5000) {
            //    yield return null;
            //    s.Reset();
            //    s.Start();
            //}

            if (waitForFrames) {
                yield return null;
            }

            DrawNextColor();
        }

        yield return null;
    }

    private void DrawNextColor() {
        print("Draw next");
        Color32 color = colors.State.GetColor(0);
        generator.GenerateBlock(color);

        colorImage.color = color;
        colorRGB.text = Colors.ColorToRGB(color);
        colorName.text = ColorIdentifier.ColorPrimary(color);
        colorHue.text = ColorIdentifier.ColorHue(color);
    }

}
