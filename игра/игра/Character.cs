using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Заменить namespace
namespace игра
{
    public enum Status_all
    {
        Healthy,
        Weak,
        Ill,
        Poisoned,
        Paralyzed,
        Dead
    }

    public  enum Race_all
    {
        Human,
        Gnome,
        Elf,
        Orc,
        Goblin
    }

    public enum Gender_all
    {
        Male,
        Female
    }
    interface IComparable
    {
        int CompareTo(Object obj);
    }
    public class Character: IComparable
    {
        // Это неизменяемые списки (только для чтения)
        private readonly IReadOnlyList<string> status_all = new List<string> { "нормальное", "ослаблен", "болен", "парализован", "мёртв" };
        private readonly IReadOnlyList<string> race_all = new List<string> { "человек", "гном", "эльф", "орк", "гоблин" };
        private readonly IReadOnlyList<string> gender_all = new List<string> { "мужской", "женский" };
        // ПОЧЕМУ НЕ Enum?????????  Каждый раз когда нужно будет менять статус или что либо еще придется смотреть код и смотреть соответсвие индексов к состоянию????????*
        //вверх я не могу достучаться до них =)))))))))
        //вверх
        //вверх
        //вверх
        //
        //

        private static int ID_next= 0;

        protected int hp;
        protected int Max_hp;
        protected int age;
        protected int xp; 
        protected Status_all status;
        //set'ы для них
 
        public int ID
        {
            get; 
        }
        public int Age
        { 
            get { return age; }
            set
            {
                if (age < 0)
                    throw new System.ArgumentException
                        ("Age < 0");
                age = value;
            }
        }
        public int HP
        {
            get { return hp; }
            set
            {
                if (value < 0) hp = 0;
                if (value > Max_hp) hp = Max_HP;
                else hp = value;
                CheckHP();
            }
        }
        public int Max_HP
        {
            get { return Max_hp; }
            set
            {
                if (value < 0)
                    throw new System.ArgumentException
                        ("Max hp < 0");
                Max_hp = value
                ;
            }
        } 
        public int XP
        {
            get { return xp; }
            set
            {
                if (value < 0)
                    throw new System.ArgumentException
                        ("xp < 0");
                xp = value;
            }

        }
        public string Name
        {
            get ; 
        }
        //public string Status
        //{
        //    get; set;
        //}
        //public string Race
        //{
        //    get; private set;
        //}
        //public string Gender
        //{
        //    get; private set;
        //}
        public Status_all Status
        {
            get { return status; } set { status = value; if (Status == Status_all.Dead || Status == Status_all.Paralyzed) {Move_ability = Talk_ability = false; } }
        }
        public Race_all Race
        {
            get; 
        }
        public Gender_all Gender
        {
            get;
        }

        public bool Talk_ability
        {
            get; set;
        }
        public bool Move_ability
        {
            get; set;
        }
        //public Character(string _name, int _race, int _gender, int _age)
        //{
        //    // Заменить(?) идентификатор
        //    ID = ID_next;
        //    ID_next += 1;
        //    Name = _name;
        //    Status = status_all[0];
        //    Talk_ability = true;
        //    Move_ability = true;
        //    Race = race_all[_race];
        //    Gender = gender_all[_gender];
        //    Age = _age;
        //    Max_HP = 100;
        //    HP = Max_HP;
        //    XP = 0;
        //}
        public Character(string _name, Race_all _race, Gender_all _gender, int _age)
        {
            // Заменить(?) идентификатор
            ID = ID_next;
            ID_next += 1;
            if (String.IsNullOrEmpty(_name) || _name.Length < 1) throw new System.ArgumentException ("Wtf the Name");
            else Name = _name;
            Status = Status_all.Healthy;
            Talk_ability = true;
            Move_ability = true;
            Race = _race;
            Gender = _gender;
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
        //public virtual void CheckHP()
        //{
        //    decimal HP_percent = 100 / (Max_HP / HP);
        //    if (HP_percent < 10 && Status == status_all[0])
        //        Status = status_all[1];
        //    if (HP_percent > 10 && Status == status_all[1])
        //        Status = status_all[0];
        //    if (HP_percent == 0)
        //        Status = status_all[4];
        //}
        public virtual void CheckHP()
        {
            decimal HP_percent = 100 / (Max_HP / HP);
            if (HP_percent < 10 && Status == Status_all.Healthy)
                Status = Status_all.Weak;
            if (HP_percent > 10 && Status == Status_all.Weak)
                Status = Status_all.Healthy;
            if (HP_percent == 0)
                Status = Status_all.Dead;
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
            return $"ID: {ID}\nИмя: {Name}\nСостояние: {Status}\nВозможность разговаривать: {talk_ans}\n" +
                $"Возможность двигаться: {move_ans}\nРаса: {Race}\nПол: {Gender}\nВозраст: {Age}\nЗдоровье: {HP}\n" +
                $"Максимальное здоровье: {Max_HP}\nОпыт: {XP}";
        }
       // туса с артефактами
        static void Main(string[] args)
        {
            var abc = new MagicCharacter("Xa", Race_all.Elf, Gender_all.Female, 15);
            abc.XP = 150;
            Console.WriteLine(abc.ToString());
            Console.ReadKey();
        }
    }

}
