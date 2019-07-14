using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Colors : MonoBehaviour {

    public int startIndex;
    public bool continueRecording;

    public ColorState State;

    struct ColorObject {
        public Vector3 color;
        public string hueColor;
    }

    private void Awake() {

        ColorState.path = GetComponent<Recorder>().Path;

        if (continueRecording) {
            //IdentifyStartIndex();
            State = ColorState.Load();
        } else {
            State = ColorState.NewState();
        }

        //InitializeColors();
    }

    private void IdentifyStartIndex() {

        string path = GetComponent<Recorder>().Path;

        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

        startIndex = 0;

        foreach (string file in Directory.GetFiles(path)) {
            if (file.Contains("colors_")) {

                string[] split = file.Split('.');

                string number = split[split.Length - 2];

                split = number.Split('_');

                number = split[split.Length - 1];

                int newIndex = int.Parse(number) * 256;

                if (newIndex > startIndex) {
                    startIndex = newIndex;
                }
            }
        }

    }

    private void SetStartIndex() {



    }

    public void SaveState() {
        return;
        ColorState.Save(State);
    }

    public static string ColorToRGB(Color32 color) {

        string rgb = "R: " + color.r + ", G: " + color.g + ", B: " + color.b;
        return rgb;
    }
}

[Serializable]
public class ColorState {

    private const string filename = "color_state";
    public static string path;

    private static string FilePath() {
        return path + "/" + filename;
    }

    public int index;

    public int g;
    public int b;

    public bool done;

    public ColorState() {
        g = 0;
        b = 0;
    }

    public void NextColors() {

        if (done) {
            return;
        }

        index++;

        if ((int)b % 2 == 0) {
            Up();
        } else {
            Down();
        }
    }

    private void Up() {
        g++;

        if (g > 255) {
            // Next down
            g = 255;
            if (done) {
                b--;
            } else {
                b++;
            }
            if (b > 255) {
                b = 255;
                done = true;
            } else if (b < 0) {
                b = 0;
                done = false;
            }
        }
    }
    private void Down() {
        g--;

        if (g < 0) {

            // Next up
            g = 0;
            if (done) {
                b--;
            } else {
                b++;
            }
            if (b > 255) {
                b = 255;
                done = true;
            } else if (b < 0) {
                b = 0;
                done = false;
            }
        }
    }

    //public Color32 ToColor() {
    //    return new Color32((byte)r, (byte)g, (byte)b, 255);
    //}
    public Color32 GetColor(byte r) {
        return new Color32(r, (byte)g, (byte)b, 255);
    }

    public static void Save(ColorState state) {

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(FilePath(), FileMode.Create, FileAccess.Write);

        formatter.Serialize(stream, state);
        stream.Close();
    }
    public static ColorState Load() {

        if (!File.Exists(FilePath())) {
            return NewState();
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(FilePath(), FileMode.Open, FileAccess.Read);

        ColorState state = (ColorState)formatter.Deserialize(stream);

        return state;
    }
    public static ColorState NewState() {

        ColorState state = new ColorState();

        state.b = 0;
        state.g = 0;
        state.index = 0;

        return state;
    }
}