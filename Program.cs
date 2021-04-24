using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPG
{
    class Program
    {
        static IDictionary<string, Character> character;
        static int N_of_person;
        //static string play_hero;

        static void Art(int art, Character character)//рандом артефакта для найти артефакт
        {
            Artifact artifact;
            var rnd = new Random();
            var vals = new int[] { 10, 25, 50 };//размер артефакта
            switch (art)
            {
                case 0:
                    artifact = new HpWater((BottleVolume)vals[rnd.Next(vals.Length)]);
                    break;
                case 1:
                    artifact = new MpWater((BottleVolume)vals[rnd.Next(vals.Length)]);
                    break;
                case 2:
                    artifact = new StaffOfLightning(rnd.Next(5, 50));
                    break;
                case 3:
                    artifact = new Decoction();
                    break;
                case 4:
                    artifact = new PoisonousSpit(rnd.Next(5, 50));
                    break;
                default:
                    artifact = new BasiliskEye();
                    break;
            }
            character.GetArtifact(artifact);
            Console.WriteLine(character.Name + " обнаружил{0} артефакт!", character.CheckGender());////////////
            character.InventoryInfo(artifact);
            Console.WriteLine();
        }

        static Spell ChooseSpell(int n)// выбор заклинания
        {
            Spell spell = new Heal();
            switch (n)
            {
                case 1:
                    spell = new Heal();
                    break;
                case 2:
                    spell = new Antidote();
                    break;
                case 3:
                    spell = new Animate();
                    break;
                case 4:
                    spell = new Armor();
                    break;
                case 5:
                    spell = new Die_off();
                    break;
                case 6:
                    spell = new Add_HP();
                    break;
            }
            return spell;
        }
        static void OutSpells()
        {
            Console.WriteLine("1: \"Вылечить\"\n2: \"Противоядие\"\n3: \"Оживить\"\n4: \"Броня\"\n5: \"Отомри!\"\n6: \"Добавить здоровье\"");
        }
        static bool FindPerson(string name)//проверка есть ли такой перс
        {
            foreach (string n in character.Keys)
                if (n == name)
                    return true;
            return false;
        }
        static Character GetPerson(string name)
        {
            return character[name];
        }
        static void OutNames()
        {
            int count = 1;
            foreach (string n in character.Keys)
            {
                Console.WriteLine("Персонаж {0}: {1}", count, n);
                ++count;
            }
        }
        static void SomeoneDied(Character killer, Character victim)
        {
            if (victim.Status == Status_all.Dead)
                Console.WriteLine("{0} убил{1} {2}!", killer.Name, killer.CheckGender(), victim.Name);
        }
        static void Action(Character hero)//Выполнение действия
        {
            string actmag = "): ";
            int n;
            if (hero is MagicCharacter)
            {
                actmag = ", 5-выучить заклинание, 6-забыть заклинание, 7-произнести заклинание): ";
            }
            Console.WriteLine("Выполнить действие (1-найти артефакт, 2-выбросить артефакт, 3-передать артефакт, 4-использовать артефакт" + actmag);
            while (!Int32.TryParse(Console.ReadLine(), out n) | (!(hero is MagicCharacter) & n > 4) | n < 1 | (hero is MagicCharacter & n > 7))
            {
                Console.Write("Вы неправильно ввели номер действия. Повторите ввод: ");
            }
            switch (n)
            {
                case 1:
                    Random rnd = new Random();
                    bool yes = rnd.Next(2) == 1;
                    if (!yes)
                        Console.WriteLine(hero.Name + " не обнаружил{0} артефакт!", hero.CheckGender());/////////////
                    else
                    {
                        int art = rnd.Next(0, 5);
                        Art(art, hero);
                        //Console.WriteLine(hero.Name + " обнаружил{0} артефакт!", hero.CheckGender());
                    }
                    break;
                //!!!!!!!!!
                case 2:
                    if (hero.SizeInventory() == 0)
                    {
                        Console.WriteLine("Инвентарь пуст!");
                        break;
                    }
                    int N_throw;
                    hero.OutInventory();
                    Console.WriteLine("Какой артефакт вы хотите выбросить? Введите номер ячейки инвентаря:");
                    while (!Int32.TryParse(Console.ReadLine(), out N_throw) | N_throw < 1 | N_throw > hero.SizeInventory())//////////
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    hero.ThrowArtifact(hero.ChooseInventory(N_throw-1));////////////
                    Console.WriteLine(hero.Name + " выбросил{0} артефакт!", hero.CheckGender());
                    break;
                //!!!!!!!!
                case 3:
                    if (hero.SizeInventory() == 0)
                    {
                        Console.WriteLine("Инвентарь пуст!");
                        break;
                    }
                    string s;
                    int N_art;
                    hero.OutInventory();
                    Console.WriteLine("Какой артефакт вы хотите передать? Введите номер ячейки инвентаря:");
                    while (!Int32.TryParse(Console.ReadLine(), out N_art) | N_art < 1 | N_art > hero.SizeInventory())///////////
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    OutNames();
                    Console.WriteLine("Введите имя игрока, которому хотите передать артефакт: ");
                    s = Console.ReadLine();
                    while (!FindPerson(s))
                    {
                        Console.WriteLine("Введенного вами имя не существует. Повторите ввод еще раз: ");
                        s = Console.ReadLine();
                    }
                    hero.GiveArtifact(hero.ChooseInventory(N_art-1), GetPerson(s));///////////////
                    Console.WriteLine("{0} передал{1} артефакт {2}!", hero.Name, hero.CheckGender(), GetPerson(s).Name);
                    break;
                //!!!!!!!!
                ////Сбилась нумерация, начинается с 0, а выводиться с 1
                case 4:
                    if (hero.SizeInventory() == 0)
                    {
                        Console.WriteLine("Инвентарь пуст!");
                        break;
                    }
                    hero.OutInventory();
                    Console.WriteLine("Какой артефакт вы хотите использовать? Введите номер ячейки инвентаря:");
                    while (!Int32.TryParse(Console.ReadLine(), out N_art) | N_art < 1 | N_art > hero.SizeInventory())/////////
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    Artifact artifact = hero.ChooseInventory(N_art-1);///////
                    OutNames();
                    Console.WriteLine("Введите имя игрока, на котором хотите использовать артефакт: ");
                    s = Console.ReadLine();
                    while (!FindPerson(s))
                    {
                        Console.WriteLine("Введенного вами имя не существует. Повторите ввод еще раз: ");
                        s = Console.ReadLine();
                    }
                    if (artifact is PoisonousSpit || artifact is StaffOfLightning)
                    {
                        hero.UseArtifact(artifact, GetPerson(s), artifact.Power);
                    }
                    else
                        hero.UseArtifact(artifact, GetPerson(s));
                    Console.WriteLine(hero.Name + " использовал{0} артефакт!", hero.CheckGender());
                    SomeoneDied(hero, GetPerson(s));
                    break;
                case 5:
                    int N_spell;
                    OutSpells();
                    Console.WriteLine("Какое заклинание хотите выучить? Введите номер заклинания: ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_spell) | N_spell < 1 | N_spell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    Spell spell;
                    spell = ChooseSpell(N_spell);
                    if (hero is MagicCharacter magic)
                        magic.LearnSpell(spell);
                    Console.WriteLine(hero.Name + " выучил{0} заклинание!", hero.CheckGender());
                    break;
                case 6:
                    int N_fspell;
                    OutSpells();
                    Console.WriteLine("Какое заклинание хотите забыть? Введите номер заклинания: ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_fspell) | N_fspell < 1 | N_fspell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    Spell fspell;
                    fspell = ChooseSpell(N_fspell);
                    try
                    {
                        if (hero is MagicCharacter mag)
                            mag.ForgetSpell(fspell);
                        Console.WriteLine(hero.Name + " забыл{0} заклинание!", hero.CheckGender());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    break;
                //!!!!!!!!
                case 7:
                    int N_Sspell, n_power;
                    OutSpells();
                    Console.WriteLine("Какое заклинание хотите произнести? Введите номер заклинания: ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_Sspell) | N_Sspell < 1 | N_Sspell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    OutNames();
                    Console.WriteLine("Введите имя игрока, на котором хотите использовать заклинание: ");
                    s = Console.ReadLine();
                    while (!FindPerson(s))
                    {
                        Console.WriteLine("Введенного вами имя не существует. Повторите ввод еще раз: ");
                        s = Console.ReadLine();
                    }
                    Console.WriteLine("Введите мощность заклинания: ");
                    while (!Int32.TryParse(Console.ReadLine(), out n_power))//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели значение мощности. Повторите ввод еще раз: ");
                    }
                    try
                    {
                        if (hero is MagicCharacter magician)
                            magician.UseSpell(ChooseSpell(N_Sspell), GetPerson(s), n_power);
                        Console.WriteLine(hero.Name + " использовал{0} заклинание!", hero.CheckGender());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        break;
                    }
                    SomeoneDied(hero, GetPerson(s));
                    break;
            }

        }
        static bool Alive()// Проверка сколько персонажей живы 
        {
            int n = 0;
            foreach (Character p in character.Values)
            {
                if (p.Status == Status_all.Dead)
                    n++;
            }
            if (n == N_of_person - 1)
            {
                Console.WriteLine("Игра закончена!");
                return false;
            }
            return true;
        }
        static void Main(string[] args)
        {
            //string play_hero;
            Console.WriteLine("Добро пожаловать в игру!");
            Console.Write("Сколько хотите создать персонажей (не менее двух)? Введите натуральное число: ");

            while (!Int32.TryParse(Console.ReadLine(), out N_of_person) | N_of_person < 2)
            {
                Console.Write("Вы неправильно ввели число. Повторите ввод: ");
            }
            character = new Dictionary<string, Character>(N_of_person);
            string name;
            int race, gen, age;
            for (int i = 0; i < N_of_person; i++)
            {
                Console.WriteLine("Создание " + (i + 1) + " персонажа.");
                Console.Write("Введите имя персонажа: ");
                while (true)
                {
                    name = Console.ReadLine();
                    if (name != "")
                        break;
                    Console.Write("Имя не может отсутствовать! Повторите ввод: ");
                }
                Console.Write("Выберите расу персонажа (0-человек, 1-гном, 2-эльф, 3-орк, 4-гоблин): ");
                while (!Int32.TryParse(Console.ReadLine(), out race) | race > 4 | race < 0)
                {
                    Console.Write("Вы неправильно ввели номер расы. Повторите ввод: ");
                }
                Console.Write("Введите пол персонажа (0-мужской, 1-женский): ");
                while (!Int32.TryParse(Console.ReadLine(), out gen) | gen > 1 | gen < 0)
                {
                    Console.Write("Вы неправильно ввели номер пола. Повторите ввод: ");
                }
                Console.Write("Введите возраст персонажа: ");
                while (!Int32.TryParse(Console.ReadLine(), out age) | age < 1)
                {
                    Console.Write("Вы неправильно ввели значение возраста. Повторите ввод: ");
                }
                if (race != 1 & race != 2)
                    character.Add(name, new Character(name, (Race_all)race, (Gender_all)gen, age));
                else
                    character.Add(name, new MagicCharacter(name, (Race_all)race, (Gender_all)gen, age));
                Console.WriteLine();
            }
            /*Console.WriteLine("За кого персонажа вы хотите играть? Введите имя");
            play_hero = Console.ReadLine();*/
            while (/*character[play_hero].Status != Status_all.Dead &*/ Alive())
            {
                foreach (Character p in character.Values)
                {
                    Console.WriteLine("Ход персонажа {0}!", p.Name);
                    Action(p);
                }
            }
            /*
            //Spell spell = new Add_HP();
            //Console.ReadKey();

            MagicCharacter mag = new MagicCharacter("Xa", Race_all.Elf, Gender_all.Female, 15);

            Character character = new Character("An", Race_all.Gnome, Gender_all.Male, 15);
            //Armor armor=new Armor();
            //character.GetArmor();
            character.HP = 20;

            Spell heal = new Add_HP();
            //mag.LearnSpell(heal);
            //mag.UseSpell(heal, character, 150);
            Artifact staff = new StaffOfLightning(170);
            Artifact lol = new PoisonousSpit(10);
            mag.GetArtifact(staff);
            mag.GetArtifact(lol);
            mag.UseArtifact(staff, character, 170);
            //heal.Perform_a_magic_effect(character, 5);

            // character.GetArmor();
            // string s=Console.ReadLine();
            // if (s == "1")
            //{
            //   character.GetArmor();
            // }
            //Console.ReadKey();
            //var abc = new MagicCharacter("Xa", Race_all.Elf, Gender_all.Female, 15);
            //abc.XP = 150;
            Console.WriteLine(character.ToString());
            Console.WriteLine(mag.ToString());
            Console.ReadKey();
            */
        }
    }
}

//static string RandomPerson()// рандомны
//{
//    string s = "";
//    int temp;
//    Random rnd = new Random();
//    temp = rnd.Next(1, N_of_person);
//    //temp = N;
//    foreach (string name in character.Keys)
//    {
//        temp--;
//        if (temp == 0)
//            s = name;
//    }
//    return s;
//}
