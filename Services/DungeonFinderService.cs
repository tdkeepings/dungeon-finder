using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class DungeonFinderService : IDungeonFinderService
    {
        private int _groupSize;
        private IQueueService _queueService;


        public DungeonFinderService(IQueueService queueService)
        {
            this._queueService = queueService;
            _groupSize = 5;

            // register event handlers
            _queueService.QueueTick += OnQueueTick;
        }

        public void OnQueueTick(object sender, EventArgs e)
        {
            IEnumerable<QueueCharacter> group = FindValidGroup();
            if (group.Count() >= 5)
            {
                QueuePop(group);
            }
        }

        public void QueuePop(IEnumerable<QueueCharacter> group)
        {
            DungeonGroup dungeonGroup = new DungeonGroup();
            dungeonGroup.Members = group;

            Console.WriteLine("Found valid group with characters and roles:");

            foreach (QueueCharacter groupMember in group)
            {
                Console.WriteLine(groupMember.Character.Name + " as " + groupMember.SelectedRole.ToString());
            }
        }

        private IEnumerable<QueueCharacter> FindValidGroup()
        {
            IEnumerable<QueueCharacter> candidates = _queueService.QueuedCharacters;

            bool tankFound = false;
            bool healerFound = false;
            int dpsFound = 0;

            List<QueueCharacter> group = new List<QueueCharacter>();

            // a valid group has to have at least 5 people, pointless checking if 
            // the queue doesn't have enough people
            if (candidates.Count() >= _groupSize)
            {
                foreach (QueueCharacter queueMember in candidates)
                {
                    // for now we can just assume that tanks are the least likely to
                    // queue up, then healers, then dps
                    if (queueMember.Character.Class.CanPerformRole(CharacterRole.Tank) && !tankFound)
                    {
                        queueMember.SelectedRole = CharacterRole.Tank;
                        group.Add(queueMember);
                        tankFound = true;
                    }
                    else if (queueMember.Character.Class.CanPerformRole((CharacterRole.Healer)) && !healerFound)
                    {
                        queueMember.SelectedRole = CharacterRole.Healer;
                        group.Add(queueMember);
                        healerFound = true;
                    }
                    else if (queueMember.Character.Class.CanPerformRole((CharacterRole.Dps)))
                    {
                        if (dpsFound < 3)
                        {
                            queueMember.SelectedRole = CharacterRole.Dps;
                            group.Add(queueMember);
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
