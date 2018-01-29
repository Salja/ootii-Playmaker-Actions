// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Check Player IsStance")]
    public class IsStance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [Tooltip("Set Stance")]
        public FsmInt stance;

        public FsmEvent trueEvent;

        public FsmEvent falseEvent;

        [UIHint(UIHint.Variable)]
        [Tooltip("Store the result in a Bool variable.")]
        public FsmBool store;

        [Tooltip("Repeat every frame. Useful if the variables are changing and you're waiting for a particular result.")]
        public bool everyFrame;

        private ActorController mActorController;

        public override void Reset()
        {
            gameObject = null;
            stance = null;
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

            mActorController = go.GetComponent<ActorController>();

            if (mActorController != null)
            {
                if (stance.Value == mActorController.State.Stance)
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