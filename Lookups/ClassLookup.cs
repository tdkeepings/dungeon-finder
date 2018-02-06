using System;
using System.Collections.Generic;
using Models;

namespace Lookups
{
    public class ClassLookup
    {
        #region Singleton instance
        private static ClassLookup instance;

        public static ClassLookup Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ClassLookup();
                }

                return instance;
            }
        }

        private CharacterClass warrior;
        private CharacterClass priest;

        private ClassLookup()
        {
            warrior = new CharacterClass()
            {
                Name = "Warrior",
                Roles = new List<CharacterRole>(){
                    CharacterRole.Tank,
                    CharacterRole.Dps
                }
            };

            priest = new CharacterClass()
            {
                Name = "Priest",
                Roles = new List<CharacterRole>(){
                    CharacterRole.Dps,
                    CharacterRole.Healer
                }
            };
        }
        #endregion

        public CharacterClass Warrior
        {
            get
            {
                return warrior;
            }
        }

        public CharacterClass Priest
        {
            get
            {
                return priest;
            }
        }
    }
}
