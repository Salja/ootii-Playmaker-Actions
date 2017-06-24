// (c) Copyright HutongGames, LLC 2010-2014. All rights reserved.
/*--- __ECO__ __PLAYMAKER__ __ACTION__ ---*/

using UnityEngine;
using com.ootii.Actors.AnimationControllers;
using com.ootii.MotionControllerPacks;
using com.ootii.Actors.Magic;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("ootii")]
    [Tooltip("Cast a Spell by Spell Index")]

    public class CastSpell : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject Player.")]
        [CheckForComponent(typeof(MotionController))]
        [CheckForComponent(typeof(SpellInventory))]
        public FsmOwnerDefault pPlayer = null;

        public FsmInt spellIndex;

        public FsmEvent finishEvent;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private MotionController mMotionController;
        private SpellInventory mSpellInventory;

        public override void Reset()
        {
            pPlayer = null;
            spellIndex = 0;
            finishEvent = null;
        }

        public override void OnEnter()
        {
            SpellCast(spellIndex);
            Fsm.Event(finishEvent);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SpellCast(spellIndex);

            if (finishEvent != null)
            {
                Fsm.Event(finishEvent);
            }
        }

        void SpellCast(FsmInt spellIndex)
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(pPlayer);
            if (go == null)
            {
                return;
            }

            mMotionController = go.GetComponent<MotionController>();
            mSpellInventory = go.GetComponent<SpellInventory>();

            PMP_BasicSpellCastings lCastMotion = mMotionController.GetMotion<PMP_BasicSpellCastings>();
            mMotionController.ActivateMotion(lCastMotion, spellIndex.Value);

            Debug.Log("Cast Spell " + GetSpellName());
        }

        public string GetSpellName()
        {
            string lName = mSpellInventory._Spells[spellIndex.Value].Name;
            return lName;
        }

    }
}
