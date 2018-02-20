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
        private List<QueueCharacter> queuedCharacters { get; set; }

        public event EventHandler QueueTick;
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
            queuedCharacters = new List<QueueCharacter>();
            id = Guid.NewGuid().ToString();

            // kick off the queue timer
            Timer t = new Timer(1000);
            t.AutoReset = true;
            t.Elapsed += new ElapsedEventHandler(OnQueueTick);
            t.Start();
            
            // all done
            Console.WriteLine("Started service with ID: " + Id);
        }

        #region Queue events
        protected virtual void OnQueueTick(object sender, EventArgs e)
        {
            QueueTick?.Invoke(this, e);
        }
        
        #endregion
        
        #region Character manipulation
        
        /// <summary>
        /// Read only QueuedCharacters
        /// </summary>
        /// <value>The queued characters.</value>
        public IEnumerable<QueueCharacter> QueuedCharacters
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
        public void AddCharacter(QueueCharacter character)
        {
            if (!CharacterIsInQueue(character))
            {
                queuedCharacters.Add(character);
            }
        }

        /// <summary>
        /// Remove the given character from the queue
        /// </summary>
        /// <param name="character">Character.</param>
        public void RemoveCharacter(QueueCharacter character)
        {
            if (CharacterIsInQueue(character))
            {
                queuedCharacters.Remove(character);
            }
        }

        /// <summary>
        /// Returns the given character if it is present in the queue
        /// </summary>
        /// <returns>The character in the queue, or null</returns>
        /// <param name="character">Character.</param>
        private bool CharacterIsInQueue(QueueCharacter character)
        {
            QueueCharacter characterInList = queuedCharacters.SingleOrDefault<QueueCharacter>(c => c.Id.Equals(character.Id));
            return characterInList != null;
        }
        #endregion
    }
}
