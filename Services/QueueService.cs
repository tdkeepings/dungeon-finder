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
        public string Id
        {
            get
            {
                return id;
            }
        }

        public QueueService()
        {
            queuedCharacters = new List<Character>();

            id = Guid.NewGuid().ToString();

            Timer t = new Timer(1000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnQueueTick);
            t.Start();

            Console.WriteLine("Started service with ID: " + Id);
        }

        #region Queue events

        public event EventHandler QueueTick;

        protected virtual void OnQueueTick(object sender, EventArgs e)
        {
            EventHandler handler = QueueTick;
            if (handler != null)
            {
                handler(this, e);
            }
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
