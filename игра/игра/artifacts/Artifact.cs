using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace игра.artifacts
{
    public enum BottleVolume
    {
        Small = 10,
        Medium = 25,
        Large = 50
    }
    public abstract class Artifact: IMagic
    {

        protected int power;
        public virtual int Power
        {
            get { return power; }
            set
            {
                if (value < 0) throw new System.ArgumentException("Artifact Power < 0");
                power = value;
            }
        }
        public bool CanUse
        {
            get;
            protected set;
        }
        public Artifact(int _power)
        {
            Power = _power;
            CanUse = true;
        }
        public virtual void Perform_a_magic_effect(Character character, int power){ throw new NotSupportedException(); }

    
        public virtual void Perform_a_magic_effect(Character character) { throw new NotSupportedException(); }

        public virtual void Perform_a_magic_effect(int power) { throw new NotSupportedException(); }
        public virtual void Perform_a_magic_effect() { throw new NotSupportedException(); }
    
    }
}
