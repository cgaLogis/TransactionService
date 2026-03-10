using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Application.Excceptions
{
    public class TransactionAlreadyExistException : ApplicationException
    {
        public TransactionAlreadyExistException(Guid id) : base($"Транзакция с id : {id} уже существует.")
        {
        }
    }
}
