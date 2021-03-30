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
        protected int min_mana;
        public int lost_mana{get; protected set;}//потраченная мана, для испольщования в другом классе, возможно уберем
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
                double HP_percent = 100 / (character.Max_HP / character.HP);
                if (HP_percent < 10)
                {
                    character.Status = Status_all.Weak;
                }
                if (HP_percent > 10)
                {
                    character.Status = Status_all.Healthy;
                }
            }
            lost_mana = 20;
            //-20 mana
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
                double HP_percent = 100 / (character.Max_HP / character.HP);
                if (HP_percent < 10)
                {
                    character.Status = Status_all.Weak;
                }
                if (HP_percent > 10)
                {
                    character.Status = Status_all.Healthy;
                }
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
        //public bool isArmor { get; protected set; }
        public Armor()
        {
            min_mana = 50;
            verbal_comp = true;
            motor_comp = true;
        }
        public override void Perform_a_magic_effect(Character character, int power)// power как время
        {
            // еще один способ, но не знаю, я оба не проверяла
            /*DateTime t1 = DateTime.Now;
            isArmor = true;
            if ((DateTime.Now - t1).TotalSeconds <= 0)
            {
                isArmor = false;
            }*/
            TimerCallback tm = new TimerCallback(Count);
            //нужно добавить в главный класс поле неуязвимости
            character.isArmor = true;
            Timer timer = new Timer(tm, character, 0, power*60000);
            lost_mana = power * 50;
            //за единицу времени 1 минута
            //надо поставить во всех вредоносных штуках проверку при отнятии чего-то
            //только при отнятии чего-то, заклинание будут произноситься, отнимать ману, но не действовать
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
            //mana=2*power
        }
    }

}
