using System;
using System.Collections.Generic;

namespace Models
{
    public class CharacterClass
    {
        public string Name { get; set; }
        public IEnumerable<CharacterRole> Roles { get; set; }

        public bool CanPerformRole(CharacterRole role)
        {
            foreach (CharacterRole cRole in Roles)
            {
                if (cRole == role)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
