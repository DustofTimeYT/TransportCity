using System.Collections.Generic;

namespace ContractBoard
{
    public class ContractBoardModel
    {
        public List<IContract> Contracts { get; private set; }

        public ContractBoardModel()
        {
            Contracts = new List<IContract>();
        }
    }
}
