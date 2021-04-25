using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{
    public enum BottleVolume
    {
        Small = 10,
        Medium = 25,
        Large = 50
    }
    public abstract class Artifact : IMagic
    {

        protected int power;
        public virtual int Power
        {
            get { return power; }
            set
            {
                if (value < 0) throw new System.ArgumentException("Artifact Power < 0");
                power = value;
            }
        }
        public bool CanUse
        {
            get;
            protected set;
        }
        public Artifact(int _power)
        {
            power = _power;
            CanUse = true;
        }
        public virtual void Perform_a_magic_effect(Character character, int power) { throw new NotSupportedException(); }


        public virtual void Perform_a_magic_effect(Character character) { throw new NotSupportedException(); }

        public virtual void Perform_a_magic_effect(int power) { throw new NotSupportedException(); }
        public virtual void Perform_a_magic_effect() { throw new NotSupportedException(); }

    }

    class BasiliskEye : Artifact
    {
        public BasiliskEye() : base(0) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status != Status_all.Dead )
            {
                if (!character.isArmor)
                {
                    character.Status = Status_all.Paralyzed;
                    character.Move_ability = false;
                    character.Talk_ability = false;
                }
                CanUse = false;
            }
        }

    }

    class Decoction : Artifact
    {
        public Decoction() : base(0) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status == Status_all.Poisoned)
            {
                character.Status = Status_all.Healthy;
                character.HP = character.HP;
                CanUse = false;
            }
        }
    }
  
    class HpWater : Artifact
    {
        public HpWater(BottleVolume volume) : base((int)volume) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status == Status_all.Dead) throw new GameException("Can't restore health to a dead character!");
            character.HP += Power;
            CanUse = false;
        }
    }
    class MpWater : Artifact
    {
        public MpWater(BottleVolume volume) : base((int)volume) { }

        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status == Status_all.Dead) throw new GameException("Can't restore mana to a dead character!");
            if (character is MagicCharacter magician)
            {
                magician.MP += Power;
                CanUse = false;
            }
        }
    }

    class PoisonousSpit : Artifact
    {
        public PoisonousSpit(int power) : base(power) { }
        public override void Perform_a_magic_effect(Character character)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            if (character.Status != Status_all.Dead & !character.isArmor)
            {
                character.Status = Status_all.Poisoned;
                character.HP -= Power;
            }
        }
    }

    class StaffOfLightning : Artifact
    {
        public StaffOfLightning(int power) : base(power) { }
        public override int Power
        {
            get { return power; }
            set
            {
                if (power < value)
                    throw new GameException("Can't overuse the staff!");
                power = value;
            }
        }
        //////////////
        public override void Perform_a_magic_effect(Character character, int power)
        {
            if (!CanUse) throw new GameException("Can't use the artifact anymore");
            if (character == null) throw new ArgumentNullException("Character is null");
            //Power -= power;
            if (power > Power)
            {
                power = Power;
                Power = 0;
            }
            else
                Power -= power;

            if (!character.isArmor)
                character.HP = character.HP - power;
            CanUse = Power > 0;
        }
        public override void Perform_a_magic_effect(Character character)
        {
            Perform_a_magic_effect(character, Power);
        }
    }
}
