using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Btn_Card : General {

    Button _thumb;
    Text _title;
    Text _power;
    Text _life;
    Vector3 _iniPos;

    // Use this for initialization
    void Start () {
        _thumb = GetChildCalled("Card_Thumb").GetComponent<Button>();
        _title = GetChildCalled("Title").GetComponent<Text>();
        _power = GetChildCalled("Power").GetComponent<Text>();
        _life = GetChildCalled("Life").GetComponent<Text>();

        _iniPos = transform.position;
        Hide();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void Show(string key)
    {
        Program.GameScene.SoundManager.PlaySound(1);

        var stat = Building.ReturnBuildStat(key);

        _title.text = key;
        _power.text = stat.Cost+"";
        _life.text = stat.Health + "";

        AssignThumb(key);

        transform.position = _iniPos;
    }

    void AssignThumb(string key)
    {
        var editHold = FindObjectOfType<EditorPrefabHolder>();
        var index = Building.ReturnBuildIndex(key);
        var obj = editHold.Prefabs[index];

        var texture = AssetPreview.GetAssetPreview(obj);

        if (texture == null)
        {
            return;
        }

        var rect = new Rect(0, 0, texture.width, texture.height);


        texture.alphaIsTransparency = true;

        Sprite newSp = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        var objImg = _thumb.GetComponent<Image>();
        objImg.sprite = newSp;
    }

    internal void Hide()
    {
        transform.position += new Vector3(0, 5000, 0);
    }
}
