// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;
using com.ootii.Actors.Attributes;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Modify Player Float Attributes")]

    public class GetAttributeFloat : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(BasicAttributes))]
        public FsmOwnerDefault pPlayer = null;

        public FsmString attribute;

        [UIHint(UIHint.Variable)]
        public FsmFloat store;

        private BasicAttributes mAttributes = null;

        public bool everyFrame;

        private BasicAttributes mBasicAttributes;

        public override void Reset()
        {
            pPlayer = null;
            attribute = null;
            store = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            GetAttribute();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            GetAttribute();
        }

        void GetAttribute()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mBasicAttributes = go.GetComponent<BasicAttributes>();

            if (mBasicAttributes != null)
            {
                float lValue = mBasicAttributes.GetAttributeValue(attribute.Value);
                store.Value = mBasicAttributes.GetAttributeValue(attribute.Value, lValue);
            }
        }
    }
}

