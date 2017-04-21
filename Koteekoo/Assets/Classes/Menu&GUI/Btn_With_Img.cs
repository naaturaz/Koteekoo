using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Btn_With_Img : MonoBehaviour
{
    public GameObject obj;//the prefab needs to be droped here


    // Use this for initialization
    void Start()
    {
        var texture = AssetPreview.GetAssetPreview(obj);
        var rect = new Rect(0, 0, texture.width, texture.height);
        texture.alphaIsTransparency = true;

        Sprite newSp = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        var objImg = GetComponent<Image>();
        objImg.sprite = newSp;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
