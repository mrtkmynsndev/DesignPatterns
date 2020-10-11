using System;
using System.Collections.Generic;

namespace Behavioral.Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Mediator Pattern!");

            // Participants
            var mert = new Bosch() { Name = "Mert" };
            var munir = new Bosch() { Name = "Munir" };
            var emre = new Siemens() { Name = "Emre" };

            // Register Chatroom
            IChatRoom chatRoom = new Chatroom();
            chatRoom.Register(mert);
            chatRoom.Register(munir);
            chatRoom.Register(emre);

            mert.Send("Emre", "Hello my friend");
            munir.Send("Mert", "Hello my son");
            emre.Send("Mert", "Hi Dear Mert");
            mert.Send("Munir", "Hello father, how re u");

        }
    }

    /// <summary>
    /// The 'Mediator' interface
    /// </summary>
    internal interface IChatRoom
    {
        void Register(Participant participant);
        void Send(string from, string to, string message);
    }

    /// <summary>
    /// The 'Concrete Mediator' class
    /// </summary>
    internal class Chatroom : IChatRoom
    {
        private Dictionary<string, Participant> _participants = new Dictionary<string, Participant>();

        public void Register(Participant participant)
        {
            if (!_participants.ContainsKey(participant.Name))
            {
                _participants.Add(participant.Name, participant);
            }

            participant.RegisterChatroom(this);
        }

        public void Send(string from, string to, string message)
        {
            var toParticipant = _participants[to];

            if (toParticipant != default(Participant))
            {
                toParticipant.Receive(from, message);
            }
        }
    }

    /// <summary>
    /// The 'AbstractColleague' class
    /// </summary>
    internal class Participant
    {
        public string Name { get; set; }
        protected Chatroom Chatroom { get; set; }

        public virtual void Send(string to, string message)
        {
            Chatroom.Send(this.Name, to, message);
        }

        public virtual void Receive(string from, string message)
        {
            Console.WriteLine($"from {from} to {Name} message: '{message}'");
        }

        public virtual void RegisterChatroom(Chatroom chatroom)
        {
            this.Chatroom = chatroom;
        }
    }

    internal class Bosch : Participant
    {
        public override void Receive(string from, string message)
        {
            Console.WriteLine($"To a {this.GetType().Name}");

            base.Receive(from, message);
        }
    }

    internal class Siemens : Participant
    {
        public override void Receive(string from, string message)
        {
            Console.WriteLine($"To a {this.GetType().Name}");

            base.Receive(from, message);
        }
    }
}
