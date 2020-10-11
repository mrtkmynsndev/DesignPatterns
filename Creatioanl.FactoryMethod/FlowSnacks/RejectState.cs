using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    public class RejectState : IState
    {
        private List<string> _actions;

        public RejectState()
        {
            _actions = new List<string>() { "Notify Manager", "Notify related User" };
        }

        public List<string> Actions => _actions;

        public string GetStateName()
        {
            return this.GetType().Name;
        }
    }
}