using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ColorSystem : MonoBehaviour {

    #region fields
    private ColorPlacer colorPlacer;
    private Colors colors;
    private Recorder recorder;

    private int safeFrames = 60;

    private bool stop = false;

    [SerializeField]
    private bool detailed;

    public static bool Detailed;
    #endregion

    private void Start() {
        colorPlacer = GetComponent<ColorPlacer>();
        colors = GetComponent<Colors>();
        recorder = GetComponent<Recorder>();

        Detailed = detailed;

        StartCoroutine(ColorSequence());
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            stop = true;
        }
    }

    private IEnumerator ColorSequence() {

        for (int i = 0; i < 30; i++) {
            yield return null;
        }

        yield return new WaitForEndOfFrame();
        recorder.InitImage();
        yield return null;

        while (true) {
            UpdateColors();

            yield return new WaitForEndOfFrame();
            if (recorder.record) {
                recorder.SaveImage();
            }

            colors.State.NextColors();
            colors.SaveState();

            yield return null;

            if (colors.State.done || stop) {
                break;
            }
        }

        Shutdown();
    }

    private void Shutdown() {

        if (!stop) {
            print("Shutdown: " + colors.State.done);
            Process.Start(new ProcessStartInfo("shutdown", "/s /t 10") {
                CreateNoWindow = true, UseShellExecute = false
            });
        }

        Application.Quit();
    }

    private void UpdateColors() {
        for (int y = 0; y < 16; y++) {
            for (int x = 0; x < 16; x++) {
                byte r = (byte)(y * 16 + x);
                colorPlacer.UpdateImage(x, y, colors.State.GetColor(r));
            }
        }
    }
}
