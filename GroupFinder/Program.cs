using Models;
using System;
using Services;
using System.Linq;
using Lookups;
using System.Collections.Generic;

namespace GroupFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            CharacterGenerator generator = new CharacterGenerator();
            IQueueService service = new QueueService();
            service.QueueTick += DungeonQueueEventHandler;

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

        /// <summary>
        /// Event handler for dungeon specific queue usage
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        static void DungeonQueueEventHandler(object sender, EventArgs e)
        {
            IQueueService service = (IQueueService)sender;
            DungeonService dungeonService = new DungeonService(service);

            dungeonService.PollQueueForValidGroup();
        }
    }
}
