using System;
using System.Collections.Generic;
using Models;

namespace Lookups
{
    public class CharacterGenerator
    {
        private string[] names = new string[]{
            "Louise",
            "Cherie",
            "Dawna",
            "Astrid",
            "Porfirio",
            "Chara",
            "Kenia",
            "Julee",
            "Fe",
            "Rocky",
            "Shani",
            "Karan",
            "Tammi",
            "Delcie",
            "Brady",
            "Muriel",
            "Wilson",
            "Tarra",
            "Marjorie",
            "Sherman"
        };

        private string RandomName()
        {
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, names.Length);
            return names[randomIndex];
        }

        public Character Warrior()
        {
            return new Character()
            {
                Name = RandomName(),
                Class = ClassLookup.Instance.Warrior
            };
        }

        public Character Priest()
        {
            return new Character()
            {
                Name = RandomName(),
                Class = ClassLookup.Instance.Priest
            };
        }
    }
}
