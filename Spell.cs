using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPG
{
    abstract class Spell : IMagic
    {
        public int min_mana { get; protected set; }
        public int lost_mana{get; protected set;}
        public bool verbal_comp { get; protected set; }
        public bool motor_comp { get; protected set; }
        
        public Spell() { }
        public virtual void Perform_a_magic_effect(Character character, int power) { throw new NotSupportedException(); }


        public virtual void Perform_a_magic_effect(Character character) { throw new NotSupportedException(); }

        public virtual void Perform_a_magic_effect(int power) { throw new NotSupportedException();}
        public virtual void Perform_a_magic_effect() { throw new NotSupportedException(); }
    }
    //1. Вылечить
    class Heal: Spell 
    {
        public Heal()
        {
            min_mana = 20;
            verbal_comp = true;
            motor_comp = false;
        }
        public override void Perform_a_magic_effect(Character character)
        {

            if (character.Status == Status_all.Ill)
            {
                character.Status = Status_all.Healthy;
                character.CheckHP();
                /*double HP_percent = 100 / (character.Max_HP / character.HP);
                if (HP_percent < 10)
                {
                    character.Status = Status_all.Weak;
                }
                if (HP_percent > 10)
                {
                    character.Status = Status_all.Healthy;
                }*/
            }
            lost_mana = 20;
        }
    }
    //2. Противоядие
    class Antidote: Spell 
    {
        public Antidote()
        {
            min_mana = 30;
            verbal_comp = true;
            motor_comp = true;
        }
        public override void Perform_a_magic_effect(Character character)
        {
            if (character.Status == Status_all.Poisoned)
            {
                character.CheckHP();
            }
            lost_mana = 30;
        }
    }
    //3. Оживить
    class Animate : Spell
    {
        public Animate()
        {
            min_mana = 150;
            verbal_comp = true;
            motor_comp = false;
        }
        public override void Perform_a_magic_effect(Character character)
        {
            if (character.Status == Status_all.Dead)
            {
                character.Status = Status_all.Weak;
                character.HP = 1;
            }
            lost_mana = 150;
        }
    }
    //4. Броня
    class Armor : Spell
    {
        public Armor()
        {
            min_mana = 50;
            verbal_comp = true;
            motor_comp = true;
        }
        public override void Perform_a_magic_effect(Character character, int power)// power как время
        {
            
            TimerCallback tm = new TimerCallback(Count);
            character.isArmor = true;
            Timer timer = new Timer(tm, character, 0, power*60000);
            lost_mana = power * 50;
            //за единицу времени 1 минута
        }
        public static void Count(object obj)
        {
            Character character = (Character)obj;
            character.isArmor = false;
            
        }
        
    }
    //5. Отомри
    class Die_off: Spell
    {
        public Die_off()
        {
            min_mana = 85;
            verbal_comp = true;
            motor_comp = false ;
        }
        public override void Perform_a_magic_effect(Character character)
        {
            if (character.Status == Status_all.Paralyzed)
            {
                character.Status = Status_all.Weak;
                character.HP = 1;
            }
            lost_mana = 85;
        }
    }

    class Add_HP : Spell 
    {
        public Add_HP()
        {
            min_mana = 2;
            verbal_comp = true;
            motor_comp = true;
        }
        public override void Perform_a_magic_effect(Character character, int power)// как power - насколько поднять НР
        {
            character.HP += power;
            lost_mana = power * 2;
        }
    }

}
