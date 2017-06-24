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
        public FsmOwnerDefault pPlayer = null;

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            pPlayer = null;
        }

        public override void OnEnter()
        {
            Store();
            Finish();
        }

        public override void OnUpdate()
        {
            Store();
        }

        void Store()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mBasicInventory = go.GetComponent<BasicInventory>();

            //MotionController.ActivateMotion(typeof(PSS_StoreSword));
            //MotionController.ActivateMotion(typeof(PSS_StoreShield));
            mBasicInventory.StoreWeaponSet();
        }
    }
}

