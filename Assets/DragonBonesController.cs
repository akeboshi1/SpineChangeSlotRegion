using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class DragonBonesController : MonoBehaviour
{
    private string preActionName = "idle";
    public string curActionName = "";

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
            if (strArr[0] == "Type" || strArr[1] == "spec" || strArr[1] == "hats" || strArr[1] == "mask" ||
                strArr[1] == "weap" || strArr[1] == "scar" || strArr[2] == "1")
            {
                slot.visible = false;
                slot.displayIndex = -1;
            }

            Debug.Log("Slot name: " + slot.name);
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