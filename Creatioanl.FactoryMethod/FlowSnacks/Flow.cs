using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    // Concrete Creator class
    public class Flow : IFlow
    {
        private List<IState> _states = new List<IState>();

        public Flow()
        {
            CreateStates();
        }

        public void CreateStates()
        {
            _states = new List<IState>() {
                new StartState(),
                new ApproveState(),
                new RejectState(),
                new FinalState()
            };
        }

        public void ShowFlowAbility()
        {
            foreach (var state in _states)
            {
                System.Console.WriteLine(state.GetStateName());
                System.Console.Write("    Actions: ");
                foreach (var action in state.Actions)
                {
                    System.Console.Write(action + ", " );
                }

                System.Console.WriteLine("");
            }
        }
    }
}