using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace игра
{

    class MagicCharacter : Character
    {
        protected int mp;
        protected int Max_mp;
        //public List<Spells> spells = new List<Spells>;  
        // или enum

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
        //public MagicCharacter(string _name, int _race, int _gender, int _age) : base(_name, _race, _gender, _age)
        //{
        //    Max_mp = 200;
        //    mp = Max_mp;
        //}
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
        //туса со спеллами
    }
}
