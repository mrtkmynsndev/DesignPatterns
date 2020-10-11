using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    // Concrete Product
    public class ApproveState : IState
    {
        private List<string> _actions;

        public ApproveState()
        {
            _actions = new List<string>() { "Email Notification", "Sms Notification" };
        }

        public List<string> Actions => _actions;

        public string GetStateName()
        {
            return $"{this.GetType().Name}";
        }
    }
}