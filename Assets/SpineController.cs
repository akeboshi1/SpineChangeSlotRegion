using System.Collections;
using System.Collections.Generic;
using Spine;
using Spine.Unity;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpineController : MonoBehaviour
{
    private SkeletonAnimation skeletonAnimation;

    private List<string> list;

    private int count = 0;

    private string skinName = "default";

    private Skin skin;

    private MeshRenderer _meshRenderer;
    
    private Skeleton _skeleton;

    private bool initBoo = false;

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

        list = new List<string>();
        string[] stringsToAdd = { "Axe", "Dust", "Idle Backward", "Idle Forward", "Jump Backward","Jump Forward","Laydown","Pickup", "Run Backward","Run Forward","Sit","Walk Backward","Walk Forward" };
        list.AddRange(stringsToAdd);
        initBoo = true;
        Debug.Log("====>",skeletonAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        if (initBoo == false)
        {
            return;
        }
        if (count<=300)
        {
            count++;
            return;
        }
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 13);
        var actionName = list[randomNumber];
        playSpine(actionName);
        count = 0;
    }

    void playSpine(string name)
    {
        // 播放动画
        skeletonAnimation.AnimationState.SetAnimation(0, name, true); 
    }

    void changeSlot(string slotName,Attachment newAttachment)
    {
        // Get the Spine slot
        var slot = skeletonAnimation.Skeleton.FindSlot(slotName);
        
        // Get the current attachment
        var attachment = slot.Attachment;

        // Replace the attachment in the skin
        skin.SetAttachment(slot.Data.Index, attachment.Name, newAttachment);
        
        // Set the attachment on the slot
        slot.Attachment = newAttachment;
    }
}