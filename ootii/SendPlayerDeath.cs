// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.Combat;
using com.ootii.Actors.LifeCores;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Send Player Death Status")]
    public class SendPlayerDeath : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(ActorCore))]
        public FsmOwnerDefault gameObject;

        private ActorCore mActorCore;

        public override void Reset()
        {
            gameObject = null;
        }

        public override void OnEnter()
        {
            SendDeath();
            Finish();
        }

        public override void OnUpdate()
        {
            SendDeath();
        }

        void SendDeath()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mActorCore = go.GetComponent<ActorCore>();

            DamageMessage lDamage = DamageMessage.Allocate();
            mActorCore.OnKilled(lDamage);
            lDamage.Release();
        }

    }
}
