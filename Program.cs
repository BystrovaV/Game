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
        static string play_hero;

        static void Art(int art, Character character)//рандом артефакта для найти артефакт
        {
            Artifact artifact=new Decoction();//чтобы не писать в каждом case
            var rnd = new Random();
            var vals = new int[] { 10, 25, 50 };//размер артефакта
            switch (art)
            {
                case 0:
                    artifact=new HpWater((BottleVolume)vals[rnd.Next(vals.Length)]);
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
                case 5:
                    artifact = new BasiliskEye();
                    break;
            }
            character.GetArtifact(artifact);
        }

        static Spell ChooseSpell(int n)// выбор заклинания
        {
            Spell spell=new Heal();
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
       
        static bool FindPerson(string name)//проверка есть ли такой перс
        {
            foreach (string n in character.Keys)
                if (n == name)
                    return true;
            return false;
        }
        static void Action(Character hero)//Выполнение действия
        {
            //string n;
            int n;
            Console.WriteLine("Выполнить действие(1-найти артефакт, 2-выбросить артефакт, 3-передать артефакт, 4-использовать артефакт");
            if (hero is MagicCharacter)
            {
                Console.WriteLine("5-выучить заклинание, 6-забыть заклинание, 7-произнести заклинание): ");

            }
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
                        Console.WriteLine("Вы не нашли артефакта");
                    else
                    {
                        int art = rnd.Next(0, 5);
                        Art(art, hero);
                    }
                    break;
                    //!!!!!!!!!
                case 2:
                    int N_throw;
                    Console.WriteLine("Какой артефакт вы хотите выбросить? (перечисление артефактов): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_throw) | N_throw < 1 | N_throw > 6)
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    //hero.ThrowArtifact(/*указать параметры*/);
                    //метод выбросить артефакт
                    break;
                    //!!!!!!!!
                case 3:
                    string s;
                    int N_art;
                    Console.WriteLine("Какой артефакт вы хотите передать? (перечисление артефактов): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_art) | N_art < 1 | N_art > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    Console.WriteLine("Введите имя игрока, которому хотите передать артефакт: ");
                    s = Console.ReadLine();
                    while (!FindPerson(s))
                    {
                        Console.WriteLine("Введенного вами имя не существует. Повторите ввод еще раз: ");
                        s = Console.ReadLine();
                    }

                    //hero.GiveArtifact(/*указать параметры*/);

                    //метод передать артефакт
                    break;
                    //!!!!!!!!
                case 4:
                    Console.WriteLine("Какой артефакт вы хотите использовать? (перечисление артефактов): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_art) | N_art < 1 | N_art > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер артефакта. Повторите ввод еще раз: ");
                    }
                    break;
                case 5:

                    int N_spell;
                    Console.WriteLine("Какое заклинание хотите выучить?(перечисление заклинаний): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_spell) | N_spell < 1 | N_spell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    Spell spell;
                    spell=ChooseSpell(N_spell);
                    if (hero is MagicCharacter magic)
                        magic.LearnSpell(spell);
                    break;
                case 6:
                    int N_fspell;

                    Console.WriteLine("Какое заклинание хотите забыть?(перечисление заклинаний): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_fspell) | N_fspell < 1 | N_fspell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    Spell fspell;
                    fspell = ChooseSpell(N_fspell);
                    if (hero is MagicCharacter mag)
                        mag.ForgetSpell(fspell);
                    Console.WriteLine("Вы забыли заклинание");
                    break;
                    //!!!!!!!!
                case 7:
                    int N_Sspell;
                    Console.WriteLine("Какое заклинание хотите произнести?(перечисление заклинаний): ");
                    while (!Int32.TryParse(Console.ReadLine(), out N_Sspell) | N_Sspell < 1 | N_Sspell > 6)//не точно
                    {
                        Console.WriteLine("Вы неправильно ввели номер заклинания. Повторите ввод еще раз: ");
                    }
                    //Выбрать персонажа и мощность
                    //Метод произнести заклинание
                    break;
            }

        }
        static bool Alive()// Проверка сколько персонажей живы 
        {
            int n = 0;
            foreach(Character p in character.Values)
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
            Console.WriteLine("Добро пожаловать в игру!");
            Console.Write("Сколько хотите создать персонажей? (Введите целое число): ");

            while (!Int32.TryParse(Console.ReadLine(), out N_of_person))
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
                name = Console.ReadLine();
                Console.Write("Выберите расу персонажа (0-человек, 1-гном, 2-эльф, 3-орк, 4-гоблин): ");
                while (!Int32.TryParse(Console.ReadLine(), out race) | race > 4 | race < 0)
                {
                    Console.Write("Вы неправильно ввели номер расы. Повторите ввод: ");
                }
                Console.Write("Введите пол персонажа (0-мужской, 1-женский): ");
                while (!Int32.TryParse(Console.ReadLine(), out gen) | gen > 1 | gen < 0)
                {
                    Console.Write("Вы неправильно ввели номер расы. Повторите ввод: ");
                }
                Console.Write("Введите возраст персонажа: ");
                while (!Int32.TryParse(Console.ReadLine(), out age) | age < 1)
                {
                    Console.Write("Вы неправильно ввели номер расы. Повторите ввод: ");
                }
                if (race != 1 & race != 2)
                    character.Add(name, new Character(name, (Race_all)race, (Gender_all)gen, age));
                else
                    character.Add(name, new MagicCharacter(name, (Race_all)race, (Gender_all)gen, age));
                Console.WriteLine();
            }
            while (Alive())
            {
                foreach (Character p in character.Values)
                {
                    Action(p);
                }
            }
        
            Spell spell = new Add_HP();
            Console.ReadKey();

        }
    }
}