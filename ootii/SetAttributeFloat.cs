// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;
using com.ootii.Actors.Attributes;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Modify Player Float Attributes")]

    public class SetAttributeFloat : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(BasicAttributes))]
        public FsmOwnerDefault pPlayer = null;

        public FsmString attribute;

        public FsmFloat floatValue;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private BasicAttributes mBasicAttributes;

        public override void Reset()
        {
            pPlayer = null;
            attribute = null;
            floatValue = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            SetAttribute();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetAttribute();
        }

        void SetAttribute()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mBasicAttributes = go.GetComponent<BasicAttributes>();

            float lValue = mBasicAttributes.GetAttributeValue(attribute.Value);
            mBasicAttributes.SetAttributeValue(attribute.Value, floatValue.Value);
        }
    }
}

