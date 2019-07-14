using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlockGenerator : MonoBehaviour {

    public bool generate;

    public float scale;

    private Transform blockParent;

    private ColorIdentifier identifier;
    GameObject blockPrefab;

    private void Start() {
        blockParent = GameObject.FindGameObjectWithTag("BlockParent").transform;
        identifier = GetComponent<ColorIdentifier>();
        blockPrefab = Resources.Load<GameObject>("ColorBlock");
    }

    public void GenerateBlock(Color32 color) {

        if (!generate) { return; }

        if (color.r != 0 && color.r != 255) {
            return;
        }
        if (color.g != 0 && color.g != 255) {
            return;
        }
        if (color.b != 0 && color.b != 255) {
            return;
        }

        GameObject newBlock = Instantiate(blockPrefab);

        newBlock.transform.parent = blockParent;
        newBlock.name = "(" + color.r + ", " + color.g + ", " + color.b + ") " + ColorIdentifier.ColorHue(color);
        newBlock.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = color;
        newBlock.transform.localScale *= scale;
        newBlock.transform.position = new Vector3(color.r * scale, color.g * scale, color.b * scale);
    }
}
