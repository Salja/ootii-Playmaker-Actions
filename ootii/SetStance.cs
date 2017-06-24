// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Send Navigation Message")]

    public class SetStance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("Set a Stance for Player")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault pPlayer = null;

        public FsmInt stance = new FsmInt (0);

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private ActorController mActorController;

        public override void Reset()
        {
            pPlayer = null;
        }

        public override void OnEnter()
        {
            SetPlayerStance(stance);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SetPlayerStance(stance);
        }

        void SetPlayerStance(FsmInt stance)
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mActorController = go.GetComponent<ActorController>();

            mActorController.State.Stance = stance.Value;
        }
    }
}

