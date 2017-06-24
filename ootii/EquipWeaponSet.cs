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
        public FsmOwnerDefault pPlayer = null;

        public bool UseCurrentSet = false;

        public FsmInt WeaponSet = new FsmInt(0);

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            pPlayer = null;
            UseCurrentSet = false;
            WeaponSet = null;
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
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
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

            int lWeaponSetIndex = (UseCurrentSet ? -1 : WeaponSet.Value);

            // Check if it's already equipped
            if (mBasicInventory.IsWeaponSetEquipped(lWeaponSetIndex))
            {
                Finish();
                return;
            }

            // Equip
            mBasicInventory.EquipWeaponSet(lWeaponSetIndex);
        }
    }
}
