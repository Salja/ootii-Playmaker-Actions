// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors.AnimationControllers;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Check Player IsRunning")]

    public class IsRunning : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(MotionController))]
        public FsmOwnerDefault pPlayer = null;

        public FsmEvent trueEvent;

        public FsmEvent falseEvent;

        [UIHint(UIHint.Variable)]
        public FsmBool store;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private MotionController mMotionController;

        public override void Reset()
        {
            pPlayer = null;
            trueEvent = null;
            falseEvent = null;
            store = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            IsRun();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            IsRun();
        }

        void IsRun()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mMotionController = go.GetComponent<MotionController>();

            IWalkRunMotion lMotion = mMotionController.ActiveMotion as IWalkRunMotion;

            if (lMotion != null)
            {
                store.Value = lMotion.IsRunActive;
                Fsm.Event(lMotion.IsRunActive ? trueEvent : falseEvent);
            }
        }
    }
}

