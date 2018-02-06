using System;
using System.Collections.Generic;
using System.Linq;
using Models;

namespace Services
{
    public class DungeonService : IDungeonService
    {

        // Default the group size to a dungeons worth
        private int minimumGroupSize = 5;
        private IQueueService service;

        public DungeonService(IQueueService _service)
        {
            service = _service;
        }

        /// <summary>
        /// Entry point for the service, runs through the process of finding a 
        /// group for a dungeon
        /// </summary>
        public void PollQueueForValidGroup()
        {
            IEnumerable<QueueCharacter> group = FindValidGroup();
            if (group.Count() == 5)
            {
                Console.WriteLine("Found valid group with characters and roles:");

                foreach (QueueCharacter groupMember in group)
                {
                    Console.WriteLine(groupMember.Character.Name + " as " + groupMember.SelectedRole.ToString());
                }
            }
        }

        /// <summary>
        /// Poll the queue to see if there is a valid group available
        /// </summary>
        /// <returns>The valid group.</returns>
        /// <param name="candidates">Candidates.</param>
        private IEnumerable<QueueCharacter> FindValidGroup()
        {
            IEnumerable<Character> candidates = service.QueuedCharacters;

            bool tankFound = false;
            bool healerFound = false;
            int dpsFound = 0;

            List<QueueCharacter> group = new List<QueueCharacter>();
            // a valid group has to have at least 5 people, pointless checking if 
            // the queue doesn't have enough people
            if (candidates.Count() >= minimumGroupSize)
            {
                foreach (Character character in candidates)
                {
                    // for now we can just assume that tanks are the least likely to
                    // queue up, then healers, then dps
                    if (character.Class.CanPerformRole(CharacterRole.Tank) && !tankFound)
                    {
                        group.Add(new QueueCharacter()
                        {
                            Character = character,
                            SelectedRole = CharacterRole.Tank
                        });
                        tankFound = true;
                    }
                    else if (character.Class.CanPerformRole((CharacterRole.Healer)) && !healerFound)
                    {
                        group.Add(new QueueCharacter()
                        {
                            Character = character,
                            SelectedRole = CharacterRole.Healer
                        });
                        healerFound = true;
                    }
                    else if (character.Class.CanPerformRole((CharacterRole.Dps)))
                    {
                        if (dpsFound < 3)
                        {
                            group.Add(new QueueCharacter()
                            {
                                Character = character,
                                SelectedRole = CharacterRole.Dps
                            });
                            dpsFound++;
                        }
                        else
                        {
                            // we dont need more than 3 dps
                        }
                    }
                    else
                    {
                        throw new Exception("CharacterClass for the candidate character does not contain any valid roles");
                    }
                }
            }

            return group;
        }

    }
}
