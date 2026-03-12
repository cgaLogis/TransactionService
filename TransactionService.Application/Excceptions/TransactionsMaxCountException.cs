namespace TransactionService.Application.Excceptions
{
    public class TransactionsMaxCountException : ApplicationException
    {
        public TransactionsMaxCountException() : base("Количество транзакций в бд уже 100. Запись невозможна.")
        {

        }
    }
}
