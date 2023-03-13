using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpineController : MonoBehaviour
{
    public Texture2D _texture;
    private SkeletonAnimation skeletonAnimation;

    private List<string> list;

    private int count = 0;

    private string skinName = "default";

    private Skin skin;

    private MeshRenderer _meshRenderer;

    private Skeleton _skeleton;

    private bool initBoo = false;

    private bool updateRegion = false;

    private bool changeSlot = false;

    private List<string> slotList;

    // Start is called before the first frame update
    void Start()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();

        _meshRenderer = GetComponent<MeshRenderer>();

        // Get the Spine skin
        skin = skeletonAnimation.Skeleton.Data.FindSkin(skinName);

        // Get the Spine skeleton
        _skeleton = skeletonAnimation.skeleton;

        // Apply the Spine skin
        skeletonAnimation.skeleton.Skin = skin;


        slotList = new List<string>();
        string[] slotsToAdd =
        {
            "head_hair_back_0005_3",
            "body_wing_0001_3",
            "barm_base_0001_3",
            "Weapons",
            "bleg_base_0001_3",
            "body_base_0001_3",
            "fleg_cost_0005_3",
            "fleg_base_0001_3",
            "bleg_cost_0005_3",
            "body_cost_dres_0005_3",
            "body_cost_0005_3",
            "head_base_0001_3",
            "head_eyes_0001_3",
            "head_hair_0005_3",
            "head_hats_0001_3",
            "head_mous_0004_3",
            "Basket",
            "head_face_0001_3",
            "farm_base_0001_3",
            "head_face_0001_1",
            "head_base_0001_", "head_hair_0005_1", "barm_base_0001_1", "bleg_base_0001_1", "head_hats_0001_1",
            "body_base_0001_1", "bleg_cost_0005_1", "body_cost_0005_1", "fleg_base_0001_1", "fleg_cost_0005_1",
            "body_cost_dres_0005_1", "farm_base_0001_1", "head_hair_back_0005_1", "body_wing_0001_1",
            "JumpEffect",
            "Dust1",
            "Dust2",
            "Dust3",
            "Rocks"
        };

        slotList.AddRange(slotsToAdd);

        // var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
        // var attachment = slot.Attachment;

        // var type = attachment.GetType();
        // if (type is RegionAttachment)
        // {
        // }
        // else if (type is MeshAttachment)
        // {
        // }
        // else if (type is BoundingBoxAttachment)
        // {
        // }
        // else if (type is PathAttachment)
        // {
        // }

        list = new List<string>();
        string[] stringsToAdd =
        {
            "Axe", "Dust", "Idle Backward", "Idle Forward", "Jump Backward", "Jump Forward", "Laydown", "Pickup",
            "Run Backward", "Run Forward", "Sit", "Walk Backward", "Walk Forward"
        };
        list.AddRange(stringsToAdd);
        initBoo = true;
        Debug.Log("====>", skeletonAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        if (updateRegion)
        {
            updateRegion = false;
            return;
        }

        if (initBoo == false)
        {
            return;
        }

        if (count <= 300)
        {
            count++;
            return;
        }

        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 13);
        var actionName = list[randomNumber];
        playSpine(actionName);
        count = 0;
        if (_texture != null)
        {
            if (changeSlot != true)
            {
                changeSlot = true;
                var slotName = slotList[13];
                CreateRegionAttachmentByTexture(slotName, _texture);
            }
        }
    }

    void playSpine(string name)
    {
        // 播放动画
        skeletonAnimation.AnimationState.SetAnimation(0, name, true);
    }

    void pauseSpine()
    {
        skeletonAnimation.AnimationState.ClearTracks();
    }

    /**
     * 替换整体皮肤贴图
     */
    void changeSkin()
    {
    }

    /**
     * 替换槽位资源
     */
    void CreateRegionAttachmentByTexture(string slotName, Texture2D texture)
    {
        // Get the Spine slot
        var slot = skeletonAnimation.Skeleton.FindSlot(slotName);

        // Get the current attachment
        var attachment = slot.Attachment;

        var region = CreateRegion((RegionAttachment)attachment, texture);
        if (region == null)
        {
            return;
        }

        RegionAttachment regionAttachment = (RegionAttachment)attachment;
        var baseRegion = (AtlasRegion)regionAttachment?.Region;
        if (baseRegion != null)
        {
            regionAttachment.Region = region;
            regionAttachment.UpdateRegion();
            // _texture = null;
            // Replace the attachment in the skin
            skin.SetAttachment(slot.Data.Index, attachment.Name, regionAttachment);
            //
            // Set the attachment on the slot
            slot.Attachment = regionAttachment;
        }
    }

    private AtlasRegion CreateRegion(RegionAttachment attachment, Texture2D texture)
    {
        var baseRegion = (AtlasRegion)attachment?.Region;
        if (baseRegion == null)
        {
            return null;
        }


        AtlasRegion region = new AtlasRegion();


        // 获取没个贴图自身实际的uv值
        calculateRuntimeUV(region, texture);
        
        region.name = baseRegion.name;
        region.rotate = false;
        region.page = new AtlasPage();
        region.page.name = texture.name;
        region.page.width = texture.width;
        region.page.height = texture.height;
        region.page.format = baseRegion.page.format;
        region.page.magFilter = baseRegion.page.magFilter;
        region.page.minFilter = baseRegion.page.minFilter;
        region.page.pma = baseRegion.page.pma;
        region.page.uWrap = baseRegion.page.uWrap;
        region.page.vWrap = baseRegion.page.vWrap;

        // 创建一个新材质
        Material newMaterial = new Material(Shader.Find("Spine/Skeleton"));

        // 从纹理中加载材质
        newMaterial.mainTexture = _texture;

        // 将材质应用于渲染对象
        region.page.rendererObject = newMaterial;

        return region;
    }

    /**
     * 动态计算region的参数
     */
    private void calculateRuntimeUV(AtlasRegion region, Texture2D texture)
    {
        // 获取纹理的宽度和高度
        var width = texture.width;
        var height = texture.height;

        // 获取有效像素区域
        var pixels = texture.GetPixels();
        var rect = new Rect(0, 0, width, height);
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                var pixel = pixels[y * width + x];
                if (pixel.a > 0)
                {
                    rect.xMin = Mathf.Min(rect.xMin, x);
                    rect.xMax = Mathf.Max(rect.xMax, x + 1);
                    rect.yMin = Mathf.Min(rect.yMin, y);
                    rect.yMax = Mathf.Max(rect.yMax, y + 1);
                }
            }
        }
        region.originalWidth = texture.width;
        region.originalHeight = texture.height;
        var curWid = rect.xMax - rect.xMin;
        var curHei = rect.yMax - rect.yMin;
        // 定义图片的显示宽高
        region.width = (int)curWid;
        region.height = (int)curHei;
        region.packedWidth = (int)curWid;
        region.packedHeight = (int)curHei;
        // 计算UV坐标
        region.u = rect.xMin / width;
        region.v = rect.yMax / height;
        region.u2 = rect.xMax / width;
        region.v2 = rect.yMin / height;
        
        // 定义槽位图片的偏移
        region.offsetX = 0;
        region.offsetY = 0;
        var uvBottomLeft = new Vector2(rect.xMin / width, rect.yMin / height);
        var uvBottomRight = new Vector2(rect.xMax / width, rect.yMin / height);
        var uvTopLeft = new Vector2(rect.xMin / width, rect.yMax / height);
        var uvTopRight = new Vector2(rect.xMax / width, rect.yMax / height);

        // 输出UV坐标
        Debug.Log("UV: " + uvBottomLeft + ", " + uvBottomRight + ", " + uvTopLeft + ", " + uvTopRight);
    }
}