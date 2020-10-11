using System.Collections.Generic;

namespace Creatioanl.FactoryMethod.FlowSnacks
{
    // Product Interface
    public interface IState
    {
        string GetStateName();
        List<string> Actions {get;}
    }
}