using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Mail;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Animation = UnityEngine.Animation;
using Attachment = Spine.Attachment;
using Color = UnityEngine.Color;
using Object = System.Object;
using Random = UnityEngine.Random;

public class SpineController : MonoBehaviour
{
    private string preActionName = "Idle Forward";
    public string curActionName = "Idle Forward";

    // head
    public Texture2D head_hair_front_1_texture;
    public Texture2D head_hair_front_3_texture;
    public Texture2D head_hair_back_1_texture;
    public Texture2D head_hair_back_3_texture;
    public Texture2D head_base_1_texture;
    public Texture2D head_base_3_texture;
    public Texture2D head_eye_texture;
    public Texture2D head_face_3_texture;
    public Texture2D head_face_1_texture;
    public Texture2D head_mous_texture;
    public Texture2D head_hats_1_texture;
    public Texture2D head_hats_3_texture;

    // body
    public Texture2D body_base_1_texture;
    public Texture2D body_base_3_texture;
    public Texture2D body_cost_1_texture;
    public Texture2D body_cost_3_texture;
    public Texture2D body_cost_dres_1_texture;
    public Texture2D body_cost_dres_3_texture;
    public Texture2D body_wing_1_texture;
    public Texture2D body_wing_3_texture;

    // barm
    // 背面右手
    public Texture2D barm_base_1_texture;

    // 正面右手
    public Texture2D barm_base_3_texture;

    public Texture2D barm_cost_1_texture;

    public Texture2D barm_cost_3_texture;

    // farm
    // 背面左手
    public Texture2D farm_base_1_texture;

    // 正面左手
    public Texture2D farm_base_3_texture;

    public Texture2D farm_cost_1_texture;

    public Texture2D farm_cost_3_texture;

    // bleg
    // 背面右腿
    public Texture2D bleg_base_1_texture;

    // 正面右腿
    public Texture2D bleg_base_3_texture;

    public Texture2D bleg_cost_1_texture;
    public Texture2D bleg_cost_3_texture;

    // fleg
    // 背面左腿
    public Texture2D fleg_base_1_texture;

    // 正面左腿
    public Texture2D fleg_base_3_texture;
    public Texture2D fleg_cost_1_texture;
    public Texture2D fleg_cost_3_texture;

    //======上一次替换的贴图，用于替换逻辑======
    private Texture2D _head_hair_front_1_texture;
    private Texture2D _head_hair_front_3_texture;
    private Texture2D _head_hair_back_1_texture;
    private Texture2D _head_hair_back_3_texture;
    private Texture2D _head_base_1_texture;
    private Texture2D _head_base_3_texture;
    private Texture2D _head_eye_texture;
    private Texture2D _head_face_1_texture;
    private Texture2D _head_face_3_texture;
    private Texture2D _head_mous_texture;
    private Texture2D _head_hats_1_texture;
    private Texture2D _head_hats_3_texture;

    // body
    private Texture2D _body_base_1_texture;
    private Texture2D _body_base_3_texture;
    private Texture2D _body_cost_1_texture;
    private Texture2D _body_cost_3_texture;
    private Texture2D _body_cost_dres_1_texture;
    private Texture2D _body_cost_dres_3_texture;
    private Texture2D _body_wing_1_texture;
    private Texture2D _body_wing_3_texture;

    // barm
    // 背面右手
    private Texture2D _barm_base_1_texture;

    // 正面右手
    private Texture2D _barm_base_3_texture;

    private Texture2D _barm_cost_1_texture;

    private Texture2D _barm_cost_3_texture;

    // farm
    // 背面左手
    private Texture2D _farm_base_1_texture;

    // 正面左手
    private Texture2D _farm_base_3_texture;

    private Texture2D _farm_cost_1_texture;

    private Texture2D _farm_cost_3_texture;

    // bleg
    // 背面右腿
    private Texture2D _bleg_base_1_texture;

    // 正面右腿
    private Texture2D _bleg_base_3_texture;
    private Texture2D _bleg_cost_1_texture;
    private Texture2D _bleg_cost_3_texture;

    // fleg
    // 背面左腿
    private Texture2D _fleg_base_1_texture;

    // 正面左腿
    private Texture2D _fleg_base_3_texture;
    private Texture2D _fleg_cost_1_texture;
    private Texture2D _fleg_cost_3_texture;


    public Texture2D dust_texture;
    private Texture2D _dust_texture;


    public SkeletonAnimation skeletonAnimation;

    private List<string> list;

    private string skinName = "default";

    private Skin skin;

    private MeshRenderer _meshRenderer;

    private SkeletonRenderer _skeletonRenderer;

    private Skeleton _skeleton;

    private bool initBoo = false;

    private bool updateRegion = false;

    private List<string> slotList;

    // 眼睛序列帧测试列表
    public List<Texture2D> eyesList = new List<Texture2D>();

    private SkeletonDataAsset originalSkeletonDataAsset;

    public SpriteRenderer sequenceRenderer;

    public Spine.AnimationState state;

    private bool eyeBoo = false;

    void Start()
    {
        // eyesList = new List<Texture2D>();

        skeletonAnimation = GetComponent<SkeletonAnimation>();

        originalSkeletonDataAsset = skeletonAnimation.SkeletonDataAsset;


        // SkeletonData skeletonData = originalSkeletonDataAsset.GetSkeletonData(true);

        // 创建动画实例
        // CreateAnimation(skeletonData);


        _meshRenderer = GetComponent<MeshRenderer>();

        _skeletonRenderer = GetComponent<SkeletonRenderer>();
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
            "head_base_0001_1", "head_hair_0005_1", "barm_base_0001_1", "bleg_base_0001_1", "head_hats_0001_1",
            "body_base_0001_1", "bleg_cost_0005_1", "body_cost_0005_1", "fleg_base_0001_1", "fleg_cost_0005_1",
            "body_cost_dres_0005_1", "farm_base_0001_1", "head_hair_back_0005_1", "body_wing_0001_1",
            "JumpEffect",
            "Dust1",
            "Dust2",
            "Dust3",
            "Rocks"
        };

        slotList.AddRange(slotsToAdd);

        list = new List<string>();
        string[] stringsToAdd =
        {
            "Axe", "Dust", "Idle Backward", "Idle Forward", "Jump Backward", "Jump Forward", "Laydown", "Pickup",
            "Run Backward", "Run Forward", "Sit", "Walk Backward", "Walk Forward"
        };
        list.AddRange(stringsToAdd);
        initBoo = true;

        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 13);
        var actionName = list[randomNumber];
        actionName = "Idle Forward";
        curActionName = actionName;

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

        checkSlots();
        if (curActionName != preActionName)
        {
            playSpine(curActionName);
            preActionName = curActionName;
        }

        if (eyesList != null && eyesList.Count == 4 && eyeBoo == false)
        {
            // ChangeEyesAnimations();
        }
    }

    void ChangeEyesAnimations()
    {
        var slotName = slotList[12];

        // Get the Spine slot
        var slot = skeletonAnimation.Skeleton.FindSlot(slotName);

        // Get the current attachment
        var attachment = slot.Attachment;


        var Count = eyesList.Count;

        var name = attachment.Name;
        Attachment spriteAttachment = new RegionAttachment(name);

        AtlasRegion region;
        if (attachment is MeshAttachment)
        {
            region = CreateRegion((MeshAttachment)attachment, eyesList[0]);
            MeshAttachment regionAttachment = (MeshAttachment)attachment;
            var baseRegion = (AtlasRegion)regionAttachment?.Region;
            if (baseRegion != null)
            {
                regionAttachment.Region = region;
                regionAttachment.UpdateRegion();
                // Replace the attachment in the skin
                skin.SetAttachment(slot.Data.Index, attachment.Name, regionAttachment);
                //
                // Set the attachment on the slot
                slot.Attachment = regionAttachment;
                slot.SetColor(Color.white);
            }
        }

        eyeBoo = true;
    }

    void checkSlots()
    {
        // ===== head
        // 前层头发正面
        if (head_hair_front_3_texture != _head_hair_front_3_texture)
        {
            var slotName = slotList[13];
            CreateRegionAttachmentByTexture(slotName, head_hair_front_3_texture);
            _head_hair_front_3_texture = head_hair_front_3_texture;
        }

        // 前层头发背面
        if (head_hair_front_1_texture != _head_hair_front_1_texture)
        {
            var slotName = slotList[21];
            CreateRegionAttachmentByTexture(slotName, head_hair_front_1_texture);
            _head_hair_front_1_texture = head_hair_front_1_texture;
        }

        // 后层头发正面
        if (head_hair_back_3_texture != _head_hair_back_3_texture)
        {
            var slotName = slotList[0];
            CreateRegionAttachmentByTexture(slotName, head_hair_back_3_texture);
            _head_hair_back_3_texture = head_hair_back_3_texture;
        }

        // 后层头发背面
        if (head_hair_back_1_texture != _head_hair_back_1_texture)
        {
            var slotName = slotList[32];
            CreateRegionAttachmentByTexture(slotName, head_hair_back_1_texture);
            _head_hair_back_1_texture = head_hair_back_1_texture;
        }

        // 正面头部
        if (head_base_3_texture != _head_base_3_texture)
        {
            var slotName = slotList[11];
            CreateRegionAttachmentByTexture(slotName, head_base_3_texture);
            _head_base_3_texture = head_base_3_texture;
        }

        // 背面头部
        if (head_base_1_texture != _head_base_1_texture)
        {
            var slotName = slotList[20];
            CreateRegionAttachmentByTexture(slotName, head_base_1_texture);
            _head_base_1_texture = head_base_1_texture;
        }

        // 嘴巴
        if (head_mous_texture != _head_mous_texture)
        {
            var slotName = slotList[15];
            CreateRegionAttachmentByTexture(slotName, head_mous_texture);
            _head_mous_texture = head_mous_texture;
        }

        // 眼睛
        if (head_eye_texture != _head_eye_texture)
        {
            var slotName = slotList[12];
            CreateRegionAttachmentByTexture(slotName, head_eye_texture);
            _head_eye_texture = head_eye_texture;
        }

        // 背面头饰
        if (head_hats_1_texture != _head_hats_1_texture)
        {
            var slotName = slotList[24];
            CreateRegionAttachmentByTexture(slotName, head_hats_1_texture);
            _head_hats_1_texture = head_hats_1_texture;
        }

        // 正面头饰
        if (head_hats_3_texture != _head_hats_3_texture)
        {
            var slotName = slotList[14];
            CreateRegionAttachmentByTexture(slotName, head_hats_3_texture);
            _head_hats_3_texture = head_hats_3_texture;
        }

        // 背面脸
        if (head_face_1_texture != _head_face_1_texture)
        {
            var slotName = slotList[19];
            CreateRegionAttachmentByTexture(slotName, head_face_1_texture);
            _head_face_1_texture = head_face_1_texture;
        }

        // 正面脸
        if (head_face_3_texture != _head_face_3_texture)
        {
            var slotName = slotList[17];
            CreateRegionAttachmentByTexture(slotName, head_face_3_texture);
            _head_face_3_texture = head_face_3_texture;
        }

        // ===== body
        // 背面翅膀
        if (body_wing_1_texture != _body_wing_1_texture)
        {
            var slotName = slotList[33];
            CreateRegionAttachmentByTexture(slotName, body_wing_1_texture);
            _body_wing_1_texture = body_wing_1_texture;
        }

        // 正面翅膀
        if (body_wing_3_texture != _body_wing_3_texture)
        {
            var slotName = slotList[5];
            CreateRegionAttachmentByTexture(slotName, body_wing_3_texture);
            _body_wing_3_texture = body_wing_3_texture;
        }

        // 正面身体
        if (body_base_3_texture != _body_base_3_texture)
        {
            var slotName = slotList[1];
            CreateRegionAttachmentByTexture(slotName, body_base_3_texture);
            _body_base_3_texture = body_base_3_texture;
        }

        // 背面身体
        if (body_base_1_texture != _body_base_1_texture)
        {
            var slotName = slotList[25];
            if (CreateRegionAttachmentByTexture(slotName, body_base_1_texture) != null)
            {
                _body_base_1_texture = body_base_1_texture;
            }
        }

        // 正面衣服
        if (body_cost_3_texture != _body_cost_3_texture)
        {
            var slotName = slotList[10];
            CreateRegionAttachmentByTexture(slotName, body_cost_3_texture);
            _body_cost_3_texture = body_cost_3_texture;
        }

        // 背面衣服
        if (body_cost_1_texture != _body_cost_1_texture)
        {
            var slotName = slotList[27];
            if (CreateRegionAttachmentByTexture(slotName, body_cost_1_texture) != null)
            {
                _body_cost_1_texture = body_cost_1_texture;
            }
        }

        // 正面裙子
        if (body_cost_dres_3_texture != _body_cost_dres_3_texture)
        {
            var slotName = slotList[9];
            CreateRegionAttachmentByTexture(slotName, body_cost_dres_3_texture);
            _body_cost_dres_3_texture = body_cost_dres_3_texture;
        }

        // 背面裙子
        if (body_cost_dres_1_texture != _body_cost_dres_1_texture)
        {
            var slotName = slotList[30];
            CreateRegionAttachmentByTexture(slotName, body_cost_dres_1_texture);
            _body_cost_dres_1_texture = body_cost_dres_1_texture;
        }

        // ===== 四肢
        // === 手
        // 正面左手
        if (farm_base_3_texture != _farm_base_3_texture)
        {
            var slotName = slotList[18];
            CreateRegionAttachmentByTexture(slotName, farm_base_3_texture);
            _farm_base_3_texture = farm_base_3_texture;
        }

        // 背面左手
        if (farm_base_1_texture != _farm_base_1_texture)
        {
            var slotName = slotList[31];
            CreateRegionAttachmentByTexture(slotName, farm_base_1_texture);
            _farm_base_1_texture = farm_base_1_texture;
        }

        // if (farm_cost_3_texture != _farm_cost_3_texture)
        // {
        //     var slotName = slotList[18];
        //     CreateRegionAttachmentByTexture(slotName, farm_cost_3_texture);
        //     _farm_cost_3_texture = farm_cost_3_texture;
        // }
        //
        // if (farm_cost_1_texture != _farm_cost_1_texture)
        // {
        //     var slotName = slotList[31];
        //     CreateRegionAttachmentByTexture(slotName, farm_cost_1_texture);
        //     _farm_cost_1_texture = farm_cost_1_texture;
        // }

        // 正面右手
        if (barm_base_3_texture != _barm_base_3_texture)
        {
            var slotName = slotList[2];
            CreateRegionAttachmentByTexture(slotName, barm_base_3_texture);
            _barm_base_3_texture = barm_base_3_texture;
        }

        // 背面右手
        if (barm_base_1_texture != _barm_base_1_texture)
        {
            var slotName = slotList[22];
            CreateRegionAttachmentByTexture(slotName, barm_base_1_texture);
            _barm_base_1_texture = barm_base_1_texture;
        }

        // if (barm_cost_3_texture != _barm_cost_3_texture)
        // {
        //     var slotName = slotList[18];
        //     CreateRegionAttachmentByTexture(slotName, barm_cost_3_texture);
        //     _barm_cost_3_texture = farm_cost_3_texture;
        // }
        //
        // if (barm_cost_1_texture != _barm_cost_1_texture)
        // {
        //     var slotName = slotList[31];
        //     CreateRegionAttachmentByTexture(slotName, barm_cost_1_texture);
        //     _barm_cost_1_texture = barm_cost_1_texture;
        // }

        // === 脚
        // 正面右脚
        if (bleg_base_3_texture != _bleg_base_3_texture)
        {
            var slotName = slotList[4];
            CreateRegionAttachmentByTexture(slotName, bleg_base_3_texture);
            _bleg_base_3_texture = bleg_base_3_texture;
        }

        // 背面右脚
        if (bleg_base_1_texture != _bleg_base_1_texture)
        {
            var slotName = slotList[23];
            CreateRegionAttachmentByTexture(slotName, bleg_base_1_texture);
            _bleg_base_1_texture = bleg_base_1_texture;
        }

        // 正面右脚装饰
        if (bleg_cost_3_texture != _bleg_cost_3_texture)
        {
            var slotName = slotList[8];
            CreateRegionAttachmentByTexture(slotName, bleg_cost_3_texture);
            _bleg_cost_3_texture = bleg_cost_3_texture;
        }

        // 背面右脚装饰
        if (bleg_cost_1_texture != _bleg_cost_1_texture)
        {
            var slotName = slotList[26];
            CreateRegionAttachmentByTexture(slotName, bleg_cost_1_texture);
            _bleg_cost_1_texture = bleg_cost_1_texture;
        }

        // 正面左脚
        if (fleg_base_3_texture != _fleg_base_3_texture)
        {
            var slotName = slotList[7];
            CreateRegionAttachmentByTexture(slotName, fleg_base_3_texture);
            _fleg_base_3_texture = fleg_base_3_texture;
        }

        // 背面左脚
        if (fleg_base_1_texture != _fleg_base_1_texture)
        {
            var slotName = slotList[28];
            CreateRegionAttachmentByTexture(slotName, fleg_base_1_texture);
            _fleg_base_1_texture = fleg_base_1_texture;
        }

        // 正面左脚装饰
        if (fleg_cost_3_texture != _fleg_cost_3_texture)
        {
            var slotName = slotList[6];
            CreateRegionAttachmentByTexture(slotName, fleg_cost_3_texture);
            _fleg_cost_3_texture = fleg_cost_3_texture;
        }

        // 背面左脚装饰
        if (fleg_cost_1_texture != _fleg_cost_1_texture)
        {
            var slotName = slotList[29];
            CreateRegionAttachmentByTexture(slotName, fleg_cost_1_texture);
            _fleg_cost_1_texture = fleg_cost_1_texture;
        }

        // dust
        if (dust_texture != _dust_texture)
        {
            var slotName = slotList[35];
            // Get the Spine slot 这段逻辑是用来替换序列帧动画的
            var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
            if (slot != null)
            {
                var list = new List<Skin.SkinEntry>();
                skin.GetAttachments(slot.Data.Index, list);
                var count = list.Count;
                if (count > 0)
                {
                    for (var i = 0; i < count; i++)
                    {
                        var skinEntry = list[i];
                        var skinEntryAttachment = skinEntry.Attachment;
                        AtlasRegion region = CreateRegion((RegionAttachment)skinEntryAttachment, dust_texture);
                        RegionAttachment regionAttachment = (RegionAttachment)skinEntryAttachment;
                        var baseRegion = (AtlasRegion)regionAttachment?.Region;
                        if (baseRegion != null)
                        {
                            regionAttachment.Region = region;
                            regionAttachment.UpdateRegion();
                            // Replace the attachment in the skin
                            skin.SetAttachment(slot.Data.Index, skinEntryAttachment.Name, regionAttachment);
                        }
                    }
                }
            }

            _dust_texture = dust_texture;
        }
    }

    public void changeTexture(string name, Texture2D tex)
    {
        switch (name)
        {
            case "head_hair_front_1_texture":
                head_hair_front_1_texture = tex;
                break;

            case "head_hair_front_3_texture":
                head_hair_front_3_texture = tex;
                break;

            case "head_hair_back_1_texture":
                head_hair_back_1_texture = tex;
                break;

            case "head_hair_back_3_texture":
                head_hair_back_3_texture = tex;
                break;
            case "head_base_1_texture":
                head_base_1_texture = tex;
                break;
            case "head_base_3_texture":
                head_base_3_texture = tex;
                break;

            case "head_eyes_3_texture":
                head_eye_texture = tex;
                break;

            case "head_face_3_texture":
                head_face_3_texture = tex;
                break;

            case "head_face_1_texture":
                head_face_1_texture = tex;
                break;

            case "head_mous_3_texture":
                head_mous_texture = tex;
                break;


            case "head_hats_1_texture":
                head_hats_1_texture = tex;
                break;
            case "head_hats_3_texture":
                head_hats_3_texture = tex;
                break;

            case "body_base_1_texture":
                body_base_1_texture = tex;
                break;
            case "body_base_3_texture":
                body_base_3_texture = tex;
                break;

            case "body_cost_1_texture":
                body_cost_1_texture = tex;
                break;
            case "body_cost_3_texture":
                body_cost_3_texture = tex;
                break;

            case "body_cost_dres_1_texture":
                body_cost_dres_1_texture = tex;
                break;
            case "body_cost_dres_3_texture":
                body_cost_dres_3_texture = tex;
                break;

            case "body_wing_1_texture":
                body_wing_1_texture = tex;
                break;
            case "body_wing_3_texture":
                body_wing_3_texture = tex;
                break;


            case "barm_base_1_texture":
                barm_base_1_texture = tex;
                break;
            case "barm_base_3_texture":
                barm_base_3_texture = tex;
                break;

            case "farm_base_1_texture":
                farm_base_1_texture = tex;
                break;
            case "farm_base_3_texture":
                farm_base_3_texture = tex;
                break;

            case "barm_cost_1_texture":
                barm_cost_1_texture = tex;
                break;
            case "barm_cost_3_texture":
                barm_cost_3_texture = tex;
                break;

            case "farm_cost_1_texture":
                farm_cost_1_texture = tex;
                break;
            case "farm_cost_3_texture":
                farm_cost_3_texture = tex;
                break;

            case "bleg_base_1_texture":
                bleg_base_1_texture = tex;
                break;
            case "bleg_base_3_texture":
                bleg_base_3_texture = tex;
                break;

            case "bleg_cost_1_texture":
                bleg_cost_1_texture = tex;
                break;
            case "bleg_cost_3_texture":
                bleg_cost_3_texture = tex;
                break;

            case "fleg_base_1_texture":
                fleg_base_1_texture = tex;
                break;
            case "fleg_base_3_texture":
                fleg_base_3_texture = tex;
                break;

            case "fleg_cost_1_texture":
                fleg_cost_1_texture = tex;
                break;
            case "fleg_cost_3_texture":
                fleg_cost_3_texture = tex;
                break;
            case "dust_texture":
                dust_texture = tex;
                break;
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

// ============= UI ===============


//============== 替换逻辑 ==============
    /**
 * todo替换整体皮肤贴图
 */
    void changeSkin()
    {
    }

    /**
 * 替换槽位资源
 */
    Attachment CreateRegionAttachmentByTexture(string slotName, Texture2D texture)
    {
        // Get the Spine slot
        var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
        // 新建一个slot并挂载到对应骨骼上
        if (slotName == "barm_base_0001_3")
        {
            var headSlot = skeletonAnimation.skeleton.FindSlot("head_base_0001_3");
            // 创建 Slot 并附着到骨骼上
            var slotData = new SlotData(slotList.Count, "testslot", slot.Bone.Data);
            slotData.AttachmentName = "testslot";
            var copySlot = new Slot(slotData, slot.Bone);
            copySlot.Attachment = headSlot.Attachment;
            skin.SetAttachment(slotList.Count, "testslot", headSlot.Attachment);
            _skeleton.Slots.Add(copySlot);
            _skeleton.DrawOrder.Add(copySlot);
            _skeleton.Data.Slots.Add(slotData);
            // 立刻更新新建的slot
            _skeleton.SetSlotsToSetupPose();
        }
        else if (slotName == "head_eyes_0001_3") // 创建一个文本贴脑门上
        {
            var go = new GameObject("follow");
            go.transform.SetParent(_skeletonRenderer.transform, false);
            BoneFollower follow = go.AddComponent<BoneFollower>();
            follow.boneName = slot.Bone.Data.Name;
            follow.SkeletonRenderer = _skeletonRenderer;
            go.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            var o = new GameObject("text");
            o.transform.SetParent(follow.transform, false);
            var _textMesh = o.GetOrAddComponent<TextMeshPro>();
            _textMesh.color = Color.black;
            _textMesh.fontSize = 1.0f;
            _textMesh.SetText("文本测试123");
            _textMesh.alignment = TextAlignmentOptions.Left;
            _textMesh.rectTransform.sizeDelta = new Vector2(0.5f, 0.1f);
            _textMesh.rectTransform.localRotation = new Quaternion(0.0f, 0.0f, -90.0f, 0.0f);
            o.transform.localScale = Vector3.one;
            // o.transform.position = Vector3.zero;
        }

        // Get the current attachment
        var attachment = slot.Attachment;

        AtlasRegion region;
        if (attachment is RegionAttachment)
        {
            region = CreateRegion((RegionAttachment)attachment, texture);
            RegionAttachment regionAttachment = (RegionAttachment)attachment;
            var baseRegion = (AtlasRegion)regionAttachment?.Region;
            if (baseRegion != null)
            {
                regionAttachment.Region = region;
                // Replace the attachment in the skin
                skin.SetAttachment(slot.Data.Index, attachment.Name, regionAttachment);
                //
                // Set the attachment on the slot
                // slot.Attachment = regionAttachment;
                // slot.SetColor(Color.white);
            }
        }
        else if (attachment is MeshAttachment)
        {
            region = CreateRegion((MeshAttachment)attachment, texture);
            MeshAttachment regionAttachment = (MeshAttachment)attachment;
            var baseRegion = (AtlasRegion)regionAttachment?.Region;
            if (baseRegion != null)
            {
                regionAttachment.Region = region;
                regionAttachment.UpdateRegion();
                // Replace the attachment in the skin
                skin.SetAttachment(slot.Data.Index, attachment.Name, regionAttachment);
                //
                // Set the attachment on the slot
                // slot.Attachment = regionAttachment;
                // slot.SetColor(Color.white);
            }
        }

        return attachment;
    }

    public void clearAllCustomTex()
    {
        head_hair_front_1_texture = null;
        head_hair_front_3_texture = null;
        head_hair_back_1_texture = null;
        head_hair_back_3_texture = null;
        head_base_1_texture = null;
        head_base_3_texture = null;
        head_eye_texture = null;
        head_face_3_texture = null;
        head_face_1_texture = null;
        head_mous_texture = null;
        head_hats_1_texture = null;
        head_hats_3_texture = null;
        body_base_1_texture = null;
        body_base_3_texture = null;
        body_cost_1_texture = null;
        body_cost_3_texture = null;
        body_cost_dres_1_texture = null;
        body_cost_dres_3_texture = null;
        body_wing_1_texture = null;
        body_wing_3_texture = null;
        barm_base_1_texture = null;
        barm_base_3_texture = null;
        farm_base_1_texture = null;
        farm_base_3_texture = null;
        bleg_base_1_texture = null;
        bleg_base_3_texture = null;
        bleg_cost_1_texture = null;
        bleg_cost_3_texture = null;
        fleg_base_1_texture = null;
        fleg_base_3_texture = null;
        fleg_cost_1_texture = null;
        fleg_cost_3_texture = null;
        dust_texture = null;

        _head_hair_front_1_texture = null;
        _head_hair_front_3_texture = null;
        _head_hair_back_1_texture = null;
        _head_hair_back_3_texture = null;
        _head_base_1_texture = null;
        _head_base_3_texture = null;
        _head_eye_texture = null;
        _head_face_3_texture = null;
        _head_face_1_texture = null;
        _head_mous_texture = null;
        _head_hats_1_texture = null;
        _head_hats_3_texture = null;
        _body_base_1_texture = null;
        _body_base_3_texture = null;
        _body_cost_1_texture = null;
        _body_cost_3_texture = null;
        _body_cost_dres_1_texture = null;
        _body_cost_dres_3_texture = null;
        _body_wing_1_texture = null;
        _body_wing_3_texture = null;
        _barm_base_1_texture = null;
        _barm_base_3_texture = null;
        _farm_base_1_texture = null;
        _farm_base_3_texture = null;
        _bleg_base_1_texture = null;
        _bleg_base_3_texture = null;
        _bleg_cost_1_texture = null;
        _bleg_cost_3_texture = null;
        _fleg_base_1_texture = null;
        _fleg_base_3_texture = null;
        _fleg_cost_1_texture = null;
        _fleg_cost_3_texture = null;
        _dust_texture = null;

        skeletonAnimation.skeletonDataAsset = originalSkeletonDataAsset;
        skeletonAnimation.Initialize(true);
    }

    public void checkSlotVisible()
    {
        string[] slots =
        {
            "head_hair_back_0005_3",
            "body_wing_0001_3",
            "Weapons",
            "fleg_cost_0005_3",
            "bleg_cost_0005_3",
            "body_cost_dres_0005_3",
            "body_cost_0005_3",
            "head_hats_0001_3",
            "Basket",
            "head_face_0001_3",
            "head_face_0001_1",
            "head_hair_0005_1",
            "head_hats_0001_1",
            "bleg_cost_0005_1",
            "body_cost_0005_1",
            "fleg_cost_0005_1",
            "body_cost_dres_0005_1",
            "head_hair_back_0005_1",
            "body_wing_0001_1",
            "JumpEffect",
            // "Dust1",
            "Dust2",
            "Dust3",
            "Rocks"
        };

        var len = slots.Length;
        for (var i = 0; i < len; i++)
        {
            var slotName = slots[i];
            var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
            slot.SetColor(new Color(0, 0, 0, 0));
        }
    }

    public void CreateAnimation(SkeletonData skeletonData)
    {
        AnimationStateData stateData = new AnimationStateData(skeletonData);
        state = new Spine.AnimationState(stateData);
        // state.SetAnimation(0, "eyefaint", true);
    }

    private AtlasRegion CreateRegion(RegionAttachment attachment, Texture2D texture)
    {
        var baseRegion = (AtlasRegion)attachment?.Region;

        if (baseRegion == null)
        {
            return null;
        }


        AtlasRegion region = new AtlasRegion();


        // 获取每个贴图自身实际的uv值
        calculateRuntimeUV(region, texture);

        attachment.Width = texture.width / 100.0f;
        attachment.Height = texture.height / 100.0f;

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
        newMaterial.SetColor("_EmissionColor", Color.white);
        // newMaterial.SetInt("_BlendOp", (int)UnityEngine.Rendering.BlendOp.Add);
        // newMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        // newMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // newMaterial.SetInt("_SrcAlpha", (int)UnityEngine.Rendering.BlendMode.One);
        // newMaterial.SetInt("_DstAlpha", (int)UnityEngine.Rendering.BlendMode.Zero);
        // newMaterial.SetInt("_AlphaSrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        // newMaterial.SetInt("_AlphaDstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        // newMaterial.SetInt("_AlphaBlend", 1);

        // 从纹理中加载材质
        newMaterial.mainTexture = texture;
        // newMaterial.SetTexture("_MainTex", texture);
        // newMaterial.SetTexture("_StraightAlphaTex", texture);
        // newMaterial.SetPass(0);
        // 将材质应用于渲染对象
        region.page.rendererObject = newMaterial;

        return region;
    }

    private AtlasRegion CreateRegion(MeshAttachment attachment, Texture2D texture)
    {
        var baseRegion = (AtlasRegion)attachment?.Region;

        if (baseRegion == null)
        {
            return null;
        }


        AtlasRegion region = new AtlasRegion();


        // 获取每个贴图自身实际的uv值
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
        newMaterial.mainTexture = texture;

        // 将材质应用于渲染对象
        region.page.rendererObject = newMaterial;

        return region;
    }

    /**
 * 动态计算region的参数
 */
    private void calculateRuntimeUV(AtlasRegion region, Texture2D texture)
    {
        // texture.alphaIsTransparency = true;
        //
        // texture.Apply(true);

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
                // 图片预乘
                // var tmppixel = texture.GetPixel(x, y);
                // tmppixel.r *= tmppixel.a;
                // tmppixel.g *= tmppixel.a;
                // tmppixel.b *= tmppixel.a;
                // tmppixel.a = Mathf.Clamp01(tmppixel.a);
                // texture.SetPixel(x, y, tmppixel);
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

        //texture.Apply(true);
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
        // var uvBottomLeft = new Vector2(rect.xMin / width, rect.yMin / height);
        // var uvBottomRight = new Vector2(rect.xMax / width, rect.yMin / height);
        // var uvTopLeft = new Vector2(rect.xMin / width, rect.yMax / height);
        // var uvTopRight = new Vector2(rect.xMax / width, rect.yMax / height);

        // 输出UV坐标
        // Debug.Log("UV: " + uvBottomLeft + ", " + uvBottomRight + ", " + uvTopLeft + ", " + uvTopRight);
    }

    // private void addEffect(string slotName)
    // {
    //     // 获取Spine骨骼插槽和附件
    //     var attachmentSlot = spineAnimation.Skeleton.FindSlot(slotName);
    //     attachment = attachmentSlot.Attachment;
    //
    //     // 从特效池中获取特效实例，并设置其位置和旋转
    //     var particleEffect = particleEffectPool.Spawn();
    //     particleEffect.transform.position = attachmentSlot.Bone.WorldToLocal(spineAnimation.transform.position);
    //     particleEffect.transform.rotation = spineAnimation.transform.rotation;
    //
    //     // 启动特效
    //     particleEffect.Play();
    // }
}