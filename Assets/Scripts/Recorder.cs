using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Recorder : MonoBehaviour {

    int width;
    int height;

    public bool reset;
    public bool record;

    private Colors colors;

    [SerializeField]
    private string path;

    [SerializeField]
    private string secondaryPath;

    public string Path { get => path; }

    [SerializeField]
    private string filename;

    private int Quality {
        get {
            if (ColorSystem.Detailed) {
                return 90;
            } else {
                return 100;
            }
        }
    }

    private void Awake() {

        if (reset) {
            ResetImages();
        }

        if (!record) {
            return;
        }

        if (!Directory.Exists(path)) {
            Directory.CreateDirectory(path);
        }

    }

    private void Start() {

        width = Screen.width;
        height = Screen.height;

        colors = GetComponent<Colors>();
    }

    public void SaveImage() {

        if (!record) {
            return;
        }

        CaptureImage();

        if (colors.State.index < 0) {
            print("Stop record: " + colors.State.index);
            record = false;
        }
    }

    private void ResetImages() {
        if (Directory.Exists(path)) {
            foreach (string file in Directory.GetFiles(path)) {
                File.Delete(file);
            }
        }
    }

    private void CaptureImage() {

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToJPG(Quality);
        Destroy(tex);

        File.WriteAllBytes(FullPath() + filename + "_" + colors.State.index + ".jpg", bytes);
    }
    public void InitImage() {

        if (!Directory.Exists(FullPath())) {
            Directory.CreateDirectory(FullPath());
        }

        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        byte[] bytes = tex.EncodeToJPG(90);
        Destroy(tex);

        File.WriteAllBytes(path + "/init_image.jpg", bytes);
    }

    private string FullPath() {
        return path + "/" + secondaryPath + "/";
    }
}
