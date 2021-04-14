using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра.artifacts
{
    class StaffOfLightning : Artifact
    {
        public StaffOfLightning(int power) : base(power) { }
        public override int Power
        {
            get { return Power; }
            set
            {
                if (power < value)
                    throw new GameException
                        ("Can't overuse the staff!");
                power = value;
            }
        }
        public override void Perform_a_magic_effect(Character character, int power)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            Power -= power;
            if(!character.isArmor)
                character.HP -= power;
            CanUse = Power > 0;
        }
        public override void Perform_a_magic_effect(Character character)
        {
            Perform_a_magic_effect(character, Power); 
        }
    }
}
