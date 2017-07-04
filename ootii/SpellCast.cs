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
        public FsmOwnerDefault gameObject;

        public FsmInt spellIndex;

        public FsmEvent finishEvent;

        [Tooltip("Repeat this action every frame. Useful if Activate changes over time.")]
        public bool everyFrame;

        private MotionController mMotionController;
        private SpellInventory mSpellInventory;

        public override void Reset()
        {
            gameObject = null;
            spellIndex = null;
            finishEvent = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            SpellCast();
            Fsm.Event(finishEvent);

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            SpellCast();

            if (finishEvent != null)
            {
                Fsm.Event(finishEvent);
            }
        }

        void SpellCast()
        {
            GameObject go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            mMotionController = go.GetComponent<MotionController>();
            mSpellInventory = go.GetComponent<SpellInventory>();

            if (mMotionController != null && mSpellInventory != null)
            {
                PMP_BasicSpellCastings lCastMotion = mMotionController.GetMotion<PMP_BasicSpellCastings>();
                if (!lCastMotion.IsActive && (!lCastMotion.RequiresStance || mMotionController.ActorController.State.Stance == EnumControllerStance.SPELL_CASTING))
                {
                    mMotionController.ActivateMotion(lCastMotion, spellIndex.Value);
                }
                Debug.Log("Cast Spell " + GetSpellName());
            }
        }

        public string GetSpellName()
        {
            string lName = mSpellInventory._Spells[spellIndex.Value].Name;
            return lName;
        }
    }
}
