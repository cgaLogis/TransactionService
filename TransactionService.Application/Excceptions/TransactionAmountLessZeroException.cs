namespace TransactionService.Application.Excceptions
{
    public class TransactionAmountLessZeroException : ApplicationException
    {
        public TransactionAmountLessZeroException(Guid id, decimal amount) : base($"Transaction id: {id} has amount {amount} less then zero.")
        {
        }
    }
}
