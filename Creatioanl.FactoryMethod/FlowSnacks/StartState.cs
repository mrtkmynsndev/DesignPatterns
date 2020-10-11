using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    public class StartState : IState
    {
        private List<string> _actions;

        public StartState()
        {
            _actions = new List<string>() { "Notify Managers" };
        }

        public List<string> Actions => _actions;

        public string GetStateName()
        {
            return this.GetType().Name;
        }
    }
}