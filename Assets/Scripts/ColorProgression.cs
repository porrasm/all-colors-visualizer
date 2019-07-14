using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProgression : MonoBehaviour {

    private int startIndex = 0;
    private int frame;

    ColorPlacer colorPlacer;
    Colors colors;

    void Start() {

        colorPlacer = GetComponent<ColorPlacer>();
        colors = GetComponent<Colors>();

        if (colors.startIndex == 0) {
            frame = -2;
        } else {
            frame = 0;
        }

        DrawCurrent();
    }

    public void DrawCurrent() {
        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 16; x++) {
                byte r = (byte)(y * 16 + x);
                colorPlacer.UpdateImage(x, y, colors.State.GetColor(r));
            }
        }
    }

    void Update() {

        if (Input.GetKey(KeyCode.Space)) {
            Application.Quit();
            return;
        }

        frame++;

        if (frame < 1) {
            return;
        }

        DrawCurrent();
    }
}
