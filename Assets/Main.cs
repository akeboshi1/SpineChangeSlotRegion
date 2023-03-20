using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject spineGO;
    public GameObject dragonBoneGO;

    public GameObject ScrolView;

    public Button resetBtn;

    private string folderPath = "Assets/Resources/"; // 存储文件夹路径

    public Texture2D[] textures; // 存储所有纹理

    Dictionary<string, List<Texture2D>> dict = new Dictionary<string, List<Texture2D>>();

    private int scrollHei;

    private int btnIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        // 加载获取到存储路径下的贴图资源
        LoadAllTextures();
        if (resetBtn) resetBtn.onClick.AddListener(() => resetClick());
    }

    void resetClick()
    {
        if (spineGO)
        {
            var spineController = spineGO.GetComponent<SpineController>();
            spineController.clearAllCustomTex();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (spineGO)
        // {
        // }
        //
        // if (dragonBoneGO)
        // {
        // }
    }

    void LoadAllTextures()
    {
        string[] fileNames = Directory.GetFiles(folderPath); // 获取文件夹中的所有文件名
        textures = new Texture2D[fileNames.Length]; // 初始化textures数组
        var spineController = spineGO?.GetComponent<SpineController>();
        spineController.sequenceRenderer = spineGO?.AddComponent<SpriteRenderer>();
        spineController.animation = spineGO?.AddComponent<Animation>();
        var index = 0;
        for (int i = 0; i < fileNames.Length; i++)
        {
            var fileName = fileNames[i];
            var fileList = fileName.Split(".");
            if (fileList[1] != "png" || fileList.Length > 2)
            {
                continue;
            }

            var nameList = fileName.Split("/");

            // 读取文件
            byte[] fileData = File.ReadAllBytes(fileNames[i]);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(fileData);

            // 设定可读写
            tex.Apply(true);
            tex.name = nameList[2];

            var textureNames = tex.name.Split("_");
            if (textureNames.Length < 3 && textureNames[0] == "eyes")
            {
                spineController?.eyesList.Add(tex);
                continue;
            }

            // 将纹理存储到textures数组中
            textures[index] = tex;

            if (ScrolView)
            {
                CreateButtons(nameList[2], index);
            }

            index++;
        }

        if (scrollHei > 300)
        {
            ((RectTransform)ScrolView.transform).sizeDelta =
                new Vector2(((RectTransform)ScrolView.transform).sizeDelta.x, scrollHei);
        }
        else
        {
            ((RectTransform)ScrolView.transform).sizeDelta =
                new Vector2(((RectTransform)ScrolView.transform).sizeDelta.x, 300);
        }
    }

    void CreateButtons(string fileName, int i)
    {
        var nameList = fileName.Split("_");
        var checkSn = nameList[2].Length > 1 ? nameList[2] : nameList[3];
        var tex = textures[i];
        if (dict.ContainsKey(checkSn))
        {
            dict[checkSn].Add(tex);
            return;
        }

        var sn = checkName(fileName, tex);
        // 创建Button GameObject
        GameObject buttonGameObject = new GameObject($"Button {btnIndex + 1}");
        buttonGameObject.transform.SetParent(ScrolView.transform, false);
        // 添加Button组件
        Button buttonComponent = buttonGameObject.AddComponent<Button>();


        // 添加Button文本
        Text buttonText = buttonGameObject.AddComponent<Text>();
        buttonText.text = sn;
        buttonText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        buttonText.fontSize = 24;
        buttonText.alignment = TextAnchor.MiddleLeft;

        // 设置Button的RectTransform
        RectTransform buttonRectTransform = buttonGameObject.GetComponent<RectTransform>();
        buttonRectTransform.sizeDelta = new Vector2(580, 50);
        buttonRectTransform.anchorMin = new Vector2(0, 1);
        buttonRectTransform.anchorMax = new Vector2(0, 1);
        buttonRectTransform.pivot = new Vector2(0, 1);
        buttonRectTransform.anchoredPosition = new Vector2(0, -btnIndex * 50);


        buttonComponent.onClick.AddListener(() => btnClick(sn));

        scrollHei += 50;
        btnIndex++;
    }

    void btnClick(string sn)
    {
        var spineController = spineGO.GetComponent<SpineController>();
        spineController.checkSlotVisible();
        var list = dict[sn];
        var len = list.Count;
        for (var i = 0; i < len; i++)
        {
            var tex = list[i];
            var name = tex.name;
            var newName = "";
            var nameList = name.Split("_");
            if (nameList[1] == "hair")
            {
                newName = nameList[0] + "_" + nameList[1] + "_front_" + nameList[3] + "_texture";
            }
            else
            {
                newName = nameList[0] + "_" + nameList[1] + "_" + nameList[3] + "_texture";
            }


            spineController.changeTexture(newName, tex);
        }
    }

    string checkName(string name, Texture2D tex)
    {
        var nameList = name.Split("_");
        var newName = "";

        var sn = nameList[2].Length > 1 ? nameList[2] : nameList[3];

        var has = dict.ContainsKey(sn);
        List<Texture2D> list;
        if (has == false)
        {
            dict[sn] = new List<Texture2D>();
        }

        list = dict[sn];
        list.Add(tex);


        return sn.ToString();
    }
}