using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorIdentifier : MonoBehaviour {

    private static Dictionary<Vector3, string> primaryColors;
    private static Dictionary<Vector3, string> hueColors;

    void Awake() {
        InitializePrimaryColors();
        ParseColors();
	}
	
    private void InitializePrimaryColors() {

        primaryColors = new Dictionary<Vector3, string>();

        primaryColors.Add(new Vector3(0, 0, 0), "Black");
        primaryColors.Add(new Vector3(0, 0, 255), "Blue");
        primaryColors.Add(new Vector3(76, 46, 40), "Brown");
        primaryColors.Add(new Vector3(0, 255, 255), "Cyan");
        primaryColors.Add(new Vector3(0, 255, 0), "Green");
        primaryColors.Add(new Vector3(127, 127, 127), "Grey");
        primaryColors.Add(new Vector3(255, 0, 255), "Magenta");
        primaryColors.Add(new Vector3(255, 127, 0), "Orange");
        primaryColors.Add(new Vector3(255, 192, 203), "Pink");
        primaryColors.Add(new Vector3(127, 0, 255), "Purple");
        primaryColors.Add(new Vector3(255, 0, 0), "Red");
        primaryColors.Add(new Vector3(255, 255, 255), "White");
        primaryColors.Add(new Vector3(255, 255, 0), "Yellow");
    }
    private void ParseColors() {

        hueColors = new Dictionary<Vector3, string>();

        TextAsset colorsText = Resources.Load<TextAsset>("colors");

        string[] split = colorsText.text.Split('\n');

        foreach (string line in split) {

            if (!line.Contains(";")) {
                continue;
            }

            string[] color = line.Split(';');

            Vector3 colorVector = ParseVector(color[0]);

            if (!hueColors.ContainsKey(colorVector)) {
                hueColors.Add(colorVector, color[1]);
            }
        }

    }

    private Vector3 ParseVector(string stringVector) {

        string fixedVector = stringVector.Split('(')[1].Split(')')[0];
        string[] rgb = fixedVector.Split(',');

        Vector3 color = Vector3.zero;

        color.x = int.Parse(rgb[0]);
        color.y = int.Parse(rgb[1]);
        color.z = int.Parse(rgb[2]);

        return color;
    }

    public static string ColorPrimary(Color32 color) {

        Vector3 colorVector = new Vector3(color.r, color.g, color.b);

        Vector3 closest = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Vector3 v in primaryColors.Keys) {

            float distance = Vector3.Distance(colorVector, v);

            if (distance < closestDistance) {
                closest = v;
                closestDistance = distance;
            }
        }

        return primaryColors[closest];
    }	
    public static string ColorHue(Color32 color) {

        Vector3 colorVector = new Vector3(color.r, color.g, color.b);

        Vector3 closest = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        foreach (Vector3 v in hueColors.Keys) {

            float distance = Vector3.Distance(colorVector, v);

            if (distance < closestDistance) {
                closest = v;
                closestDistance = distance;
            }
        }

        return hueColors[closest];
    }
}


public struct ColorObject {
    public Vector3 color;
    public string colorName;
}
