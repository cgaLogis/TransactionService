using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionService.Application.Excceptions
{
    public class NotFoundTransactionException : ApplicationException
    {
        public NotFoundTransactionException(Guid id) : base($"Transaction with id: {id} not found")
        {
        }
    }
}
