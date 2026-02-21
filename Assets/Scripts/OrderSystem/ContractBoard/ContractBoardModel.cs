using ContractLine;
using System.Collections.Generic;

namespace ContractBoard
{
    public class ContractBoardModel
    {
        public List<ContractLinePresenter> Contracts { get; private set; }

        public ContractBoardModel()
        {
            Contracts = new List<ContractLinePresenter>();
        }
    }
}
