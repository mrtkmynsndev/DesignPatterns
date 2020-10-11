using System;
using System.Collections.Generic;

namespace Behavioral.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Command Pattern!");
            var tvReceiver = new TvReceiver();

            List<IButtonCommand> buttons = new List<IButtonCommand>()
            {
                new ForwardButtonCommand(tvReceiver),
                new BackButtonCommand(tvReceiver),
                new SoundOffButtonCommand(tvReceiver)
             };

            foreach (var item in buttons)
            {
                RemoteControl firstRemote = new RemoteControl(item);
                firstRemote.Press();
            }
        }
    }

    /// <summary>
    /// The 'Command' interface
    /// </summary>
    internal interface IButtonCommand
    {
        void Execute();
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    internal class ForwardButtonCommand : IButtonCommand
    {
        ITvReciever _tvReceiver;

        public ForwardButtonCommand(ITvReciever tvReceiver)
        {
            _tvReceiver = tvReceiver;
        }

        public void Execute()
        {
            _tvReceiver.Forward();
        }
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    internal class BackButtonCommand : IButtonCommand
    {
        ITvReciever _tvReceiver;

        public BackButtonCommand(ITvReciever tvReceiver)
        {
            _tvReceiver = tvReceiver;
        }

        public void Execute()
        {
            this._tvReceiver.Back();
        }
    }

    /// <summary>
    /// The 'ConcreteCommand' class
    /// </summary>
    internal class SoundOffButtonCommand : IButtonCommand
    {
        ITvReciever _tvReceiver;

        public SoundOffButtonCommand(ITvReciever tvReceiver)
        {
            _tvReceiver = tvReceiver;
        }

        public void Execute()
        {
            _tvReceiver.SoundOff();
        }
    }

    internal interface ITvReciever
    {
        void Detect();
        void SoundOff();
        void Back();
        void Forward();
    }

    /// <summary>
    /// The 'Receiver' class
    /// </summary>
    internal class TvReceiver : ITvReciever
    {
        public void Back()
        {
            Console.WriteLine($"Back button pressed: {this.GetType().Name}");
        }

        public void Detect()
        {
            Console.WriteLine($"{this.GetType().Name}");
        }

        public void Forward()
        {
            Console.WriteLine($"Forward button pressed: {this.GetType().Name}");
        }

        public void SoundOff()
        {
            Console.WriteLine($"Sound off button pressed: {this.GetType().Name}");
        }
    }

    /// <summary>
    /// The 'Invoker' class
    /// </summary>
    internal class RemoteControl
    {
        IButtonCommand buttonCommand;

        public RemoteControl(IButtonCommand buttonCommand)
        {
            this.buttonCommand = buttonCommand;
        }

        public void Press()
        {
            this.buttonCommand.Execute();
        }
    }
}
