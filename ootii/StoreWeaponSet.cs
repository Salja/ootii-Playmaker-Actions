// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors.Inventory;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Store Weapon Set")]
    public class StoreWeaponSet : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject to check BasicInventory.")]
        [CheckForComponent(typeof(BasicInventory))]
        public FsmOwnerDefault gameObject;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            gameObject = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            Store();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            Store();
        }

        void Store()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mBasicInventory = go.GetComponent<BasicInventory>();

            if (mBasicInventory == null)
            {
                Finish();
                return;
            }

            mBasicInventory.StoreWeaponSet(-1);
        }
    }
}

