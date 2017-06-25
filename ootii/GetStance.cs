// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;
using com.ootii.Actors;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Check Player IsRunning")]
    public class GetStance : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(ActorController))]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        public FsmInt store;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private ActorController mActorController;

        public override void Reset()
        {
            gameObject = null;
            store = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            GetPlayerStance();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            GetPlayerStance();
        }

        void GetPlayerStance()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mActorController = go.GetComponent<ActorController>();

            store.Value = mActorController.State.Stance;
        }
    }
}

