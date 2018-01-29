// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Set a Stance for Player")]
    public class SetStance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Set a Stance for Player")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault gameObject;

        public FsmInt stance;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private ActorController mActorController;

        public override void Reset()
        {
            gameObject = null;
            stance = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            SetPlayerStance();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetPlayerStance();
        }

        void SetPlayerStance()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mActorController = go.GetComponent<ActorController>();

            if (mActorController != null)
            {
                mActorController.State.Stance = stance.Value;
            }
        }
    }
}

