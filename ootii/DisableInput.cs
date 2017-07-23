// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.AnimationControllers;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Cast a Spell by Spell Index")]
    public class DisableInput : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(MotionController))]
        public FsmOwnerDefault gameObject;

        public FsmBool disable;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private MotionController mMotionController;

        public override void Reset()
        {
            gameObject = null;
            disable = false;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            Disable();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            Disable();
        }

        void Disable()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mMotionController = go.GetComponent<MotionController>();

            if (mMotionController != null)
            {
                if (disable.Value == true)
                    mMotionController.InputSource.IsEnabled = false;
                else
                    mMotionController.InputSource.IsEnabled = true;
            }
        }
    }
}
