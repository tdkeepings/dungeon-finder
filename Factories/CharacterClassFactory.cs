using System;
using Models;
using System.Collections.Generic;

namespace Factories
{
    public class CharacterClassFactory
    {

        public CharacterClassFactory() {
            // do nothing, for now
        }

        ///<summary>
        /// Creates the list of all usable character classes
        ///</summary>
        public IEnumerable<CharacterClass> GenerateCharacterClasses()
        {
            // Setup
            List<CharacterClass> classes = new List<CharacterClass>();

            // Work
            CharacterClass warrior = new CharacterClass() 
            {
                Name = "Warrior",
                Roles = new List<CharacterRole>() 
                {
                    CharacterRole.Tank,
                    CharacterRole.Dps
                }
            };
            CharacterClass priest = new CharacterClass() 
            {
                Name = "Priest",
                Roles = new List<CharacterRole>() 
                {
                    CharacterRole.Healer,
                    CharacterRole.Dps
                }
            };

            // Tidy up
            classes.Add(warrior);
            classes.Add(priest);

            // Complete
            return classes;
        }
    }
}
