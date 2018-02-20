using Lookups;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class DungeonFinderService
    {
        private IQueueService _queueService;

        public DungeonFinderService(IQueueService queueService)
        {
            this._queueService = queueService;
        }

        public void Start()
        {
            CharacterGenerator generator = new CharacterGenerator();
            IQueueService service = new QueueService();

            Console.WriteLine("Add players to the queue. W for Warrior, P for Priest");

            string input = "";

            while ((input = Console.ReadLine()) != "x")
            {
                if (input.ToLower() == "w")
                {
                    Character c = generator.Warrior();
                    service.AddCharacter(c);

                    Console.WriteLine("Added new Warrior: " + c.Name);
                }
                if (input.ToLower() == "p")
                {

                    Character c = generator.Priest();
                    service.AddCharacter(c);

                    Console.WriteLine("Added new Priest: " + c.Name);

                    service.AddCharacter(generator.Priest());
                }
            }
        }
    }
}
