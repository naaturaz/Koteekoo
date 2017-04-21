using UnityEngine;
using System.Collections;
using UnityEditor;

public class ObjectBuilderScript : MonoBehaviour
{
    public GameObject obj;
    public Vector3 spawnPoint;


    public void BuildObject()
    {
        Instantiate(obj, spawnPoint, Quaternion.identity);
    }

    public void SaveTexture()
    {
        var texture = AssetPreview.GetAssetPreview(obj);

        var a = 1;
    }
}