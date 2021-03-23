using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Заменить namespace
namespace Gaaame2
{
    interface IComparable
    {
        int CompareTo(Object obj);
    }
    class Character: IComparable
    {
        // Это неизменяемые списки (только для чтения)
        private readonly IReadOnlyList<string> status_all = new List<string> { "нормальное", "ослаблен", "болен", "парализован", "мёртв" };
        private readonly IReadOnlyList<string> race_all = new List<string> { "человек", "гном", "эльф", "орк", "гоблин" };
        private readonly IReadOnlyList<string> gender_all = new List<string> { "мужской", "женский" };
        private static int ID_Count = 0;
        public int ID
        {
            get; private set;
        }
        public int Age
        {
            get; set;
        }
        public int HP
        {
            get; set;
        }
        public int Max_HP
        {
            get; set;
        }
        public int XP
        {
            get; set;
        }
        public string Name
        {
            get; private set;
        }
        public string Status
        {
            get; set;
        }
        public string Race
        {
            get; private set;
        }
        public string Gender
        {
            get; private set;
        }
        public bool Talk_ability
        {
            get; set;
        }
        public bool Move_ability
        {
            get; set;
        }
        public Character(string _name, int _race, int _gender, int _age)
        {
            // Заменить(?) идентификатор
            ID = ID_Count;
            ID_Count += 1;
            Name = _name;
            Status = status_all[0];
            Talk_ability = true;
            Move_ability = true;
            Race = race_all[_race];
            Gender = gender_all[_gender];
            Age = _age;
            Max_HP = 100;
            HP = Max_HP;
            XP = 0;
        }
        public int CompareTo(object obj)
        {
            Character otherCharacter = (Character) obj;
            if (XP < otherCharacter.XP)
                return -1;
            if (XP > otherCharacter.XP)
                return 1;
            return 0;
        }
        public void CheckHP()
        {
            double HP_percent = 100 / (Max_HP / HP);
            if (HP_percent < 10 && Status == status_all[0])
                Status = status_all[1];
            if (HP_percent > 10 && Status == status_all[1])
                Status = status_all[0];
            if (HP_percent == 0)
                Status = status_all[4];
        }
        public override string ToString()
        {
            string talk_ans, move_ans;
            if (Talk_ability)
                talk_ans = "есть";
            else
                talk_ans = "нет";
            if (Move_ability)
                move_ans = "есть";
            else
                move_ans = "нет";
            return $"**********\nID: {ID}\nИмя: {Name}\nСостояние: {Status}\nВозможность разговаривать: {talk_ans}\n" +
                $"Возможность двигаться: {move_ans}\nРаса: {Race}\nПол: {Gender}\nВозраст: {Age}\nЗдоровье: {HP}\n" +
                $"Максимальное здоровье: {Max_HP}\nОпыт: {XP}\n**********";
        }
    }
}
