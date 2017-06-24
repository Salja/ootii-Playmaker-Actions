// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Check Player IsRunning")]
    public class IsStance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault pPlayer = null;

        [RequiredField]
        [Tooltip("The first float variable.")]
        public FsmInt stance = null;

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
            pPlayer = null;
            stance = null;
            trueEvent = null;
            falseEvent = null;
            everyFrame = false;
            store = null;
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
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mActorController = go.GetComponent<ActorController>();

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