// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.Inventory;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Enable/Disable WeaponSet")]
    public class EnableWeaponSet : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(BasicInventory))]
        public FsmOwnerDefault gameObject;

        public FsmString mWeaponSet;

        public bool enable = false;

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            gameObject = null;
            mWeaponSet = null;
            enable = false;
        }

        public override void OnEnter()
        {
            isWeaponSet();
            Finish();
        }

        public override void OnUpdate()
        {
            isWeaponSet();
        }

        void isWeaponSet()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mBasicInventory = go.GetComponent<BasicInventory>();

            BasicInventorySet lWeaponSet = mBasicInventory.GetWeaponSet(mWeaponSet.Value);

            if (mBasicInventory != null && lWeaponSet != null)
            {
                lWeaponSet.IsEnabled = enable;
            }
        }
    }
}
