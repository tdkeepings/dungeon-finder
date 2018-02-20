using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using System.Timers;

namespace Services
{
    public class QueueService : IQueueService
    {
        private string id;
        private int minimumGroupSize;

        public string Id
        {
            get
            {
                return id;
            }
        }

        public QueueService()
        {
            // some initialisation
            queuedCharacters = new List<Character>();
            id = Guid.NewGuid().ToString();
            minimumGroupSize = 5;

            // kick off the queue timer
            Timer t = new Timer(1000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnQueueTick);
            t.Start();
            
            // all done
            Console.WriteLine("Started service with ID: " + Id);
        }

        #region Queue events

        public event EventHandler QueueTick;
        public event EventHandler QueuePop;

        protected virtual void OnQueueTick(object sender, EventArgs e)
        {
            QueueTick?.Invoke(this, e);
            QueryQueue();
        }

        protected virtual void OnQueuePop(object sender, EventArgs e)
        {
            QueuePop?.Invoke(this, e);
        }

        #endregion

        #region Group formation logic
        private void QueryQueue()
        {
            IEnumerable<QueueCharacter> group = FindValidGroup();
            if (group.Count() >= 5)
            {
                Console.WriteLine("Found valid group with characters and roles:");

                foreach (QueueCharacter groupMember in group)
                {
                    Console.WriteLine(groupMember.Character.Name + " as " + groupMember.SelectedRole.ToString());
                }
            }
        }
        private IEnumerable<QueueCharacter> FindValidGroup()
        {
            IEnumerable<Character> candidates = this.QueuedCharacters;

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
        #endregion

        #region Character manipulation

        private List<Character> queuedCharacters { get; set; }

        /// <summary>
        /// Read only QueuedCharacters
        /// </summary>
        /// <value>The queued characters.</value>
        public IEnumerable<Character> QueuedCharacters
        {
            get
            {
                return queuedCharacters;
            }
        }

        /// <summary>
        /// Add the given character to the queue
        /// </summary>
        /// <param name="character">Character.</param>
        public void AddCharacter(Character character)
        {
            if (CharacterInQueue(character) == null)
            {
                queuedCharacters.Add(character);
            }
        }

        /// <summary>
        /// Remove the given character from the queue
        /// </summary>
        /// <param name="character">Character.</param>
        public void RemoveCharacter(Character character)
        {
            Character characterInQueue = CharacterInQueue(character);
            if (characterInQueue != null)
            {
                queuedCharacters.Remove(characterInQueue);
            }
        }

        /// <summary>
        /// Returns the given character if it is present in the queue
        /// </summary>
        /// <returns>The character in the queue, or null</returns>
        /// <param name="character">Character.</param>
        private Character CharacterInQueue(Character character)
        {
            Character characterInList = queuedCharacters.SingleOrDefault<Character>(c => c.Name == character.Name);
            return characterInList;
        }
        #endregion
    }
}
