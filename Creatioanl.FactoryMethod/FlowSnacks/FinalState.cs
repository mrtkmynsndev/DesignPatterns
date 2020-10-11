using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    public class FinalState : IState
    {
        private List<string> _actions;

        public FinalState()
        {
            _actions = new List<string>() { "Close Flow", "Notify all users" };
        }

        public List<string> Actions => _actions;

        public string GetStateName()
        {
            return this.GetType().Name;
        }
    }
}