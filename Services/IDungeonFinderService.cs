using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public interface IDungeonFinderService
    {
        void OnQueueTick(object sender, EventArgs e);
        void QueuePop(IEnumerable<QueueCharacter> group);
    }
}
