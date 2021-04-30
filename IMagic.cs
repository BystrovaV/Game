using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Не забудьте сменить namespace!!!!
namespace RPG
{
    interface IMagic
    {
        void Perform_a_magic_effect();
        void Perform_a_magic_effect(Character character);
        void Perform_a_magic_effect(int power);
        void Perform_a_magic_effect(Character character, int power);
    }
}
