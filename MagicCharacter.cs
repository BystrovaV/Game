using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
{

    class MagicCharacter : Character
    {
        protected int mp;
        protected int Max_mp;
        //protected Dictionary<Spell, bool> spells = new Dictionary<Spell, bool>(); //зачем здесь bool
        protected HashSet<Spell> spells = new HashSet<Spell>();

        public int Max_MP
        {
            get { return Max_mp; }
            set
            {
                if (value < 0)
                    throw new System.ArgumentException
                        ("Max MP < 0");
                Max_mp = value
                ;
            }
        }
        public int MP
        {
            get { return mp; }
            set { mp = value; if (mp < 0) mp = 0; if (mp > Max_mp) mp = Max_mp; }
        }

        public MagicCharacter(string _name, Race_all _race, Gender_all _gender, int _age) : base(_name, _race, _gender, _age)
        {
            Max_mp = 200;
            mp = Max_mp;
        }

        public override string ToString()
        {
            return base.ToString() + "\n"
                + $"Mana: {MP}/{Max_MP}";
        }
        public void CheckSpell(Spell spell)
        {
            if (!CheckS(spell))
                throw new NullReferenceException("The spell is not learned.");
        }
        public bool CheckS(Spell spell)//true-содержит false-нет
        {
            foreach (var s in spells)
            {
                if (s.GetType() == spell.GetType())
                    return true;
            }
            return false;
        }
        public void LearnSpell(Spell spell)
        {
            if(CheckS(spell))
                throw new GameException("Вы уже знаете данное заклинание");
            spells.Add(spell);
        }
        public void ForgetSpell(Spell spell)
        {
            CheckSpell(spell);
            spells.Remove(spell);
        }
        public void UseSpell(Spell spell, Character character)
        {
            CheckSpell(spell);
            if (this.MP < spell.min_mana)
                throw new GameException("Недостаточно маны для выполнения заклинания!");
            if (!ExceptionSpell(spell))
                throw new GameException("Вы не можете произнести данное заклинание!");
            spell.Perform_a_magic_effect(character);
            this.MP -= spell.lost_mana;
            AddXP(character);
        }
        public void UseSpell(Spell spell, Character character, int power)
        {
            CheckSpell(spell);
            if (this.mp < spell.min_mana | !CanUseSpell(spell, power))
                throw new GameException("Недостаточно маны для выполнения заклинания!");
            if (!ExceptionSpell(spell))
                throw new GameException("Вы не можете произнести данное заклинание!");
            spell.Perform_a_magic_effect(character, power);
            this.MP -= spell.lost_mana;
            AddXP(character);
        }

        protected bool CanUseSpell(Spell spell, int power)//Проверка маны
        {
            if ((spell is Armor) & (power * 50) > this.MP)
                return false;
            if ((spell is Add_HP) & (power * 2) > this.MP)
                return false;
            return true;
        }
        protected bool ExceptionSpell(Spell spell)//Исключения к заклинаниям
        {
            if((spell is Heal)&& !Talk_ability)
                return false;
            if ((spell is Antidote) && !Talk_ability && !Move_ability)
                return false;
            if ((spell is Animate) && !Talk_ability)
                return false;
            if ((spell is Armor) && !Talk_ability && !Move_ability)
                return false;
            if ((spell is Die_off) && !Talk_ability)
                return false;
            if ((spell is Add_HP) && !Talk_ability)
                return false;
            return true;

        }
        public void OutSpellInv()//Вывод заклинаний
        {
            int count = 1;
            string item = "Добавить здоровье";
            foreach (var i in spells)
            {
                if (i is Heal)
                {
                    item = "Вылечить";
                }
                if (i is Antidote)
                    item = "Противоядие";
                if (i is Animate)
                {
                    item = "Оживить";
                }
                if (i is Armor)
                {
                    item = "Броня";
                }
                if (i is Die_off)
                {
                    item = "Отомри!";
                }
                Console.WriteLine("{0}: {1}", count, item);
                ++count;
            }
        }
        public Spell ChooseSpellinv(int i)
        {
            return spells.ElementAt(i);
        }

        public int SizeSpells()
        {
            return spells.Count();
        }

    }
}
