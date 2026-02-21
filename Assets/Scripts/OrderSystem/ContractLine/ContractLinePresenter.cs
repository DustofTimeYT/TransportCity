using Unity.VisualScripting;

namespace ContractLine
{
    public class ContractLinePresenter
    {
        private ContractLineModel _model;
        private ContractBoardPresenter _contractBoardPresenter;

        public ContractLinePresenter(IContract contract, ContractBoardPresenter contractBoardPresenter)
        {
            _model = new ContractLineModel(contract);
            _contractBoardPresenter = contractBoardPresenter;
        }

        public void SelectContract()
        {
            _contractBoardPresenter.SelectContract(this);
        }

        public IContract GetContract()
        {
            return _model.Contract;
        }

        public string GetConsumerName()
        {
            return _model.Contract.GetConsumer().GetName();
        }

        public string GetProducerName()
        {
            return _model.Contract.GetProducer().GetName();
        }

        public string GetDeliveryItem()
        {
            return _model.Contract.GetDeliveryItem().ToString();
        }

        public string GetDeliveryAmount()
        {
            return _model.Contract.GetDeliveryAmount().ToString();
        }

        public string GetMoneyAmount()
        {
            return $"{_model.Contract.GetMoneyAmount()} rub";
        }

    }
}
