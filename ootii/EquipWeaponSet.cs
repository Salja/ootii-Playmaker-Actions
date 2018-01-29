// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.Inventory;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Cast a Spell by Spell Index")]
    public class EquipWeaponSet : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(BasicInventory))]
        public FsmOwnerDefault gameObject;

        public bool UseCurrentSet = false;

        public FsmInt weaponSet;

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            gameObject = null;
            UseCurrentSet = false;
            weaponSet = null;
        }

        public override void OnEnter()
        {
            Equip();
            Finish();
        }

        public override void OnUpdate()
        {
            Equip();
        }

        void Equip()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mBasicInventory = go.GetComponent<BasicInventory>();

            int lWeaponSetIndex = (UseCurrentSet ? -1 : weaponSet.Value);

            if (mBasicInventory != null)
            {
                if (mBasicInventory.IsWeaponSetEquipped(lWeaponSetIndex))
                {
                    Finish();
                    return;
                }

                mBasicInventory.EquipWeaponSet(lWeaponSetIndex);
            }
        }
    }
}
