using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра.artifacts
{
    class HpWater : Artifact
    {
        public HpWater(BottleVolume volume) : base((int)volume) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status == Status_all.Dead) throw new GameException("Can't restore health to a dead character!");
                Power -= Power;
                character.HP += Power;
                CanUse = false;
        }
    }
}
