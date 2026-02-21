namespace ContractLine
{
    public class ContractLineModel
    {
        public IContract Contract { get; private set; }

        public ContractLineModel(IContract contract)
        {
            Contract = contract;
        }

    }
}