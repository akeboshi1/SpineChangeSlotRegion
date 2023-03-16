using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class DragonBonesController : MonoBehaviour
{
    private string preActionName = "idle";
    public string curActionName = "";
    
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

    // farm
    // 背面左手
    public Texture2D farm_base_1_texture;

    // 正面左手
    public Texture2D farm_base_3_texture;

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
    public Texture2D _barm_base_1_texture;

    // 正面右手
    public Texture2D _barm_base_3_texture;

    // farm
    // 背面左手
    public Texture2D _farm_base_1_texture;

    // 正面左手
    public Texture2D _farm_base_3_texture;

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

    private UnityArmatureComponent armatureComponent;

    // Start is called before the first frame update
    void Start()
    {
        // 获取场景中的龙骨对象
        armatureComponent = GameObject.Find("dragonBone").GetComponent<UnityArmatureComponent>();

        var armature = armatureComponent.armature;
        foreach (var slot in armature.GetSlots())
        {
            var slotName = slot.name;
            var strArr = slotName.Split("_");
            if (slotName == "barm_cost_3" || slotName == "farm_cost_3" || slotName == "head_chin_3" ||
                slotName == "farm_shld_3" || strArr[0] == "Type" || strArr[1] == "spec" || strArr[1] == "hats" ||
                strArr[1] == "mask" ||
                strArr[1] == "weap" || strArr[1] == "scar" || strArr[2] == "1")
            {
                slot.visible = false;
                slot.displayIndex = -1;
            }

            // Debug.Log("Slot name: " + slot.name);
        }


        armatureComponent.CloseCombineMeshs();
    }

    // Update is called once per frame
    void Update()
    {
        if (curActionName != preActionName)
        {
            // 播放动画
            armatureComponent.animation.Play(curActionName);
            preActionName = curActionName;
        }
    }
}