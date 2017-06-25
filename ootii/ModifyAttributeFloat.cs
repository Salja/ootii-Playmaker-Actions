// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;
using com.ootii.Actors.Attributes;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Modify Player Float Attributes")]
    public class ModifyAttributeFloat : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(BasicAttributes))]
        public FsmOwnerDefault gameObject;

        public FsmString attribute;

        public FsmFloat floatValue;

        [Tooltip("Used with Every Frame. Adds the value over one second to make the operation frame rate independent.")]
        public bool perSecond;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private BasicAttributes mBasicAttributes;

        public override void Reset()
        {
            gameObject = null;
            attribute = null;
            floatValue = null;
            perSecond = false;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            ModifyAttribute();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            ModifyAttribute();
        }

        void ModifyAttribute()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mBasicAttributes = go.GetComponent<BasicAttributes>();

            if (!perSecond)
            {
                float lValue = mBasicAttributes.GetAttributeValue(attribute.Value);
                mBasicAttributes.SetAttributeValue(attribute.Value, lValue + floatValue.Value);
            }
            else
            {
                float lValue = mBasicAttributes.GetAttributeValue(attribute.Value);
                mBasicAttributes.SetAttributeValue(attribute.Value, lValue + floatValue.Value * Time.deltaTime);
            }
        }
    }
}

