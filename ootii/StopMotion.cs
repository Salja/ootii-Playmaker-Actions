// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.AnimationControllers;
using com.ootii.MotionControllerPacks;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Cast a Spell by Spell Index")]
    public class StopMotion : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(MotionController))]
        public FsmOwnerDefault gameObject;

        public FsmInt mLayer;

        public FsmString motionName;


        public FsmEvent finishEvent;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private MotionController mMotionController;

        public override void Reset()
        {
            gameObject = null;
            mLayer = null;
            motionName = null;
            finishEvent = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            MotionStop();
            Fsm.Event(finishEvent);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            MotionStop();

            if (finishEvent != null)
            {
                Fsm.Event(finishEvent);
            }
        }

        void MotionStop()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mMotionController = go.GetComponent<MotionController>();

            if (mMotionController != null)
            {
                MotionControllerMotion lMotion = mMotionController.GetMotion(mLayer.Value, motionName.Value);
                lMotion.Deactivate();
            }
        }
    }
}
