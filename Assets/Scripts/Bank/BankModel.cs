namespace Bank
{
    public class BankModel
    {
        private BankConfig _config;

        public int MoneyAmount {  get; private set; }

        public BankModel(BankConfig config)
        {
            _config = config;
            SetDefault();
        }

        private void SetDefault()
        {
            MoneyAmount = _config.MoneyAmount;
        }

        public void SetMoneyAmount(int amount)
        {
            MoneyAmount = amount;
        }
    }
}