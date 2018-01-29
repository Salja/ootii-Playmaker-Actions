// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors;
using com.ootii.Actors.Inventory;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Check Player IsWeaponSet")]
    public class IsWeaponSet : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("The first float variable.")]
        public FsmInt weaponSet;

        public bool checkCurrentSet = false;

        public FsmEvent trueEvent;

        public FsmEvent falseEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a Bool variable.")]
        public FsmBool store;

        [Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
        public bool everyFrame;

        private BasicInventory mBasicInventory;

        public override void Reset()
        {
            gameObject = null;
            weaponSet = null;
            checkCurrentSet = false;
            trueEvent = null;
            falseEvent = null;
            everyFrame = false;
            store = false;
        }

        public override void OnEnter()
        {
            DoCompare();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoCompare();
        }

        void DoCompare()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mBasicInventory = go.GetComponent<BasicInventory>();

            int lWeaponSetIndex = (checkCurrentSet ? -1 : weaponSet.Value);

            if (mBasicInventory != null)
            {
                if (mBasicInventory.IsWeaponSetEquipped(lWeaponSetIndex))
                {
                    store.Value = true;
                    Fsm.Event(trueEvent);
                }

                else
                {
                    store.Value = false;
                    Fsm.Event(falseEvent);
                }
            }
        }
    }
}