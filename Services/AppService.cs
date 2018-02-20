using Lookups;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AppService
    {
        private IQueueService _queueService;
        private IDungeonFinderService _dungeonFinderService;

        public AppService(IQueueService queueService, IDungeonFinderService dungeonFinderService)
        {
            this._queueService = queueService;
            this._dungeonFinderService = dungeonFinderService;
        }

        public void Start()
        {
            CharacterGenerator generator = new CharacterGenerator();
            string input = "";

            Console.WriteLine("Add players to the queue. W for Warrior, P for Priest");
            while ((input = Console.ReadLine()) != "x")
            {
                Character c = null;

                switch (input.ToLower())
                {
                    case "w": c = generator.Warrior(); break;
                    case "p": c = generator.Priest(); break;
                }
                
                if (c != null)
                {
                    QueueCharacter q = new QueueCharacter
                    {
                        Character = c
                    };

                    _queueService.AddCharacter(q);
                    Console.WriteLine("Added new character: " + c.Name);
                }
            }
        }
    }
}
