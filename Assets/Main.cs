using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    public GameObject spineGO;
    public GameObject dragonBoneGO;

    public string folderPath;  // 存储文件夹路径
    public Texture2D[] textures;  // 存储所有纹理
    // Start is called before the first frame update
    void Start()
    {
        // 加载获取到存储路径下的贴图资源
        LoadAllTextures();
    }

    // Update is called once per frame
    void Update()
    {
        if (spineGO)
        {
        }

        if (dragonBoneGO)
        {
        }
    }
    
    void LoadAllTextures()
    {
        string[] fileNames = Directory.GetFiles(folderPath);  // 获取文件夹中的所有文件名
        textures = new Texture2D[fileNames.Length];  // 初始化textures数组

        for (int i = 0; i < fileNames.Length; i++)
        {
            // 读取文件
            byte[] fileData = File.ReadAllBytes(fileNames[i]);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            // 将纹理存储到textures数组中
            textures[i] = tex;
        }
    }
}