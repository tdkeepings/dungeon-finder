using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IQueueService
    {
        string Id { get; }
        IEnumerable<QueueCharacter> QueuedCharacters { get; }

        event EventHandler QueueTick;
        void AddCharacter(QueueCharacter character);
        void RemoveCharacter(QueueCharacter character);
    }
}
