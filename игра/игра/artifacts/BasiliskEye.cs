using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра.artifacts
{
    class BasiliskEye : Artifact 
    { 
         public BasiliskEye(): base(0){ }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status != Status_all.Dead & !character.isArmor) {
                character.Status = Status_all.Paralyzed;
                character.Move_ability = false;
                character.Talk_ability = false;
                CanUse = false;
            }
        }
    
    }
}
