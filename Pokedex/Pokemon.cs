using System;
using System.Collections.Generic;
using System.Text;

namespace Pokedex
{
    public class Pokemon 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type1 { get; set; }
        public string Type2 { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public int Speed { get; set; }
        public bool Legendary { get; set; }

        public Pokemon()
        {
            Name = "";
            Type1 = "";
            Type2 = "";
            HP = 10;
            Attack = 10;
            Defense = 10;
            SpAtk = 10;
            SpDef = 10;
            Speed = 10;
            Legendary = false;
        }
        public Pokemon(int id) : base()
        {
            ID = id;
        }

    }
}
