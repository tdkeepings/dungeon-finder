using System;
using System.Collections.Generic;
using Models;

namespace Services
{
    public interface IQueueService
    {
        string Id { get; }
        IEnumerable<Character> QueuedCharacters { get; }
        event EventHandler QueueTick;
        void AddCharacter(Character character);
        void RemoveCharacter(Character character);


    }
}
