using Models;
using System;
using Services;
using Lookups;

namespace GroupFinder
{
    class Program
    {
        static void Main(string[] args)
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
