using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG
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

    public enum Race_all
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
    public class Character : IComparable
    {
        private static int ID_next = 0;

        protected int hp;
        protected int Max_hp;
        protected int age;
        protected int xp;
        protected Status_all status;
        public bool isArmor = false;
        protected Dictionary<Artifact, int> inventory = new Dictionary<Artifact, int>();

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
                if (value < 0)
                    hp = 0;
                else if (value > Max_hp)
                    hp = Max_HP;
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
                Max_hp = value;
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
            get;
        }
        public Status_all Status
        {
            get { return status; }
            set { status = value; if (Status == Status_all.Dead || Status == Status_all.Paralyzed) { Move_ability = Talk_ability = false; } }
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
        public Character(string _name, Race_all _race, Gender_all _gender, int _age)
        {
            ID = ID_next;
            ID_next += 1;
            if (String.IsNullOrEmpty(_name) || _name.Length < 1) throw new System.ArgumentException("Wtf the Name");
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
            Character otherCharacter = (Character)obj;
            if (XP < otherCharacter.XP)
                return -1;
            if (XP > otherCharacter.XP)
                return 1;
            return 0;
        }
        public virtual void CheckHP()
        {
            if (HP == 0)
                Status = Status_all.Dead;
            else
            {
                decimal HP_percent = 100 / (Max_HP / HP);
                if (HP_percent < 10 && Status == Status_all.Healthy)
                    Status = Status_all.Weak;
                if (HP_percent > 10 && Status == Status_all.Weak)
                    Status = Status_all.Healthy;
            }

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
        public bool ArtifactEqual(Artifact artifact, Artifact artifact_in_inv)//равенство артефактов
        {
            bool equalpower = true;
            if (!(artifact is BasiliskEye) & !(artifact is Decoction) & (artifact.Power != artifact_in_inv.Power))
                equalpower = false;
            if ((artifact_in_inv.GetType() == artifact.GetType()) & equalpower)
                return true;
            else
                return false;
        }
        public void ArtifactCheck(Artifact artifact)//проверка на наличие артефакта
        {
            /*if (!inventory.ContainsKey(artifact))
                throw new NullReferenceException("The artifact is not in the inventory.");*/
            bool code = false;
            foreach (var i in inventory.Keys)
            {
                if (ArtifactEqual(artifact, i))
                    code = true;
            }
            if (!code)
                throw new NullReferenceException("The artifact is not in the inventory.");

        }
        public void GetArtifact(Artifact artifact)
        {
            /*if (inventory.ContainsKey(artifact))
                ++inventory[artifact];
            else
                inventory.Add(artifact, 1);*/
            bool code = false;
            foreach (var i in inventory.Keys)
            {
                if (ArtifactEqual(artifact, i))
                {
                    ++inventory[i];
                    code = true;
                    break;
                }
            }
            if (!code)
                inventory.Add(artifact, 1);
        }
        public void ThrowArtifact(Artifact artifact)
        {
            ArtifactCheck(artifact);
            /*--inventory[artifact];
            if (inventory[artifact] == 0)
                inventory.Remove(artifact);*/
            foreach (var i in inventory.Keys)
                if (i.GetType() == artifact.GetType())
                {
                    --inventory[artifact];
                    if (inventory[artifact] == 0)
                        inventory.Remove(artifact);
                    else if (artifact.CanUse == false)
                        artifact.CanUse = true;
                    break;
                }
        }
        public void GiveArtifact(Artifact artifact, Character character)
        {
            ThrowArtifact(artifact);
            character.GetArtifact(artifact);
        }
        public void UseArtifact(Artifact artifact, Character character)
        {
            ArtifactCheck(artifact);
            artifact.Perform_a_magic_effect(character);
            if (!artifact.CanUse)
                ThrowArtifact(artifact);
            
            AddXP(character);
        }
        public void UseArtifact(Artifact artifact, Character character, int power)
        {
            ArtifactCheck(artifact);
            artifact.Perform_a_magic_effect(character, power);
            if (!artifact.CanUse)
                ThrowArtifact(artifact);
            AddXP(character);
        }
        public void OutInventory()//вывод инвентаря
        {
            int count = 1;
            foreach (var i in inventory.Keys)
            {
                Console.Write("{0}: ", count);
                InventoryInfo(i);
                ++count;
            }
        }
        public void InventoryInfo(Artifact i)//информация об артефактах
        {
            string item = "Глаз василиска", info = "";
            if (i is StaffOfLightning)
            {
                item = "Посох \"Молния\"";
                info = ", мощность – " + i.Power.ToString();
            }
            if (i is Decoction)
                item = "Декокт из лягушачьих лапок";
            if (i is HpWater)
            {
                item = "Бутылка с живой водой";
                info = ", объём – " + i.Power.ToString();
            }
            if (i is MpWater)
            {
                item = "Бутылка с мёртвой водой";
                info = ", объём – " + i.Power.ToString();
            }
            if (i is PoisonousSpit)
            {
                item = "Ядовитая слюна";
                info = ", мощность – " + i.Power.ToString();
            }
            Console.WriteLine("{0}{1}", item, info);
        }
        public Artifact ChooseInventory(int i)
        {
            return inventory.ElementAt(i).Key;
        }
        public int SizeInventory()//размер инвентаря
        {
            return inventory.Count;
        }
        public string CheckGender()//определение пола для окончания
        {
            if (Gender == Gender_all.Female)
                return "a";
            else
                return "";
        }
        protected void AddXP(Character character)//добавление опыта
        {
            if (character.Status == Status_all.Dead)
            {
                this.XP += 100;
                foreach (var e in character.inventory.Keys)
                {
                    inventory.Remove(e);
                }
            }
            else
                this.XP += 10;
        }
    }

}