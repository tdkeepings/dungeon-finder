using System;
namespace Models
{
    public class QueueCharacter
    {
        public Guid Id { get; }
        public Character Character { get; set; }
        public CharacterRole SelectedRole { get; set; }

        public QueueCharacter()
        {
            Id = Guid.NewGuid();
        }
    }
}
