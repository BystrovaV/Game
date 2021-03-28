using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра.artifacts
{
    class MpWater : Artifact
    {
        public MpWater(BottleVolume volume) : base((int)volume) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status == Status_all.Dead) throw new GameException("Can't restore mana to a dead character!");
            if (character is MagicCharacter magician )
            {
                Power -= Power;
                magician.MP += Power;
                CanUse = false;
            }
        }
    }
}
