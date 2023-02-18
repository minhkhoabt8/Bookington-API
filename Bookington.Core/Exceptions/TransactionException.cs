namespace Bookington.Core.Exceptions
{
    public class TransactionException : HandledException
    {
        public TransactionException(string message) : base(404, message)
        {
        }
    }

    public class AccountNotHavingEnoughBalance : TransactionException
    {
        public AccountNotHavingEnoughBalance() : base(
            "Your account's balance is not enough for this transaction")
        {
        }
    }
}
