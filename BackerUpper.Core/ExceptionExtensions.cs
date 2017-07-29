using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackerUpper.Core
{
    public static class ExceptionExtensions
    {
        public static Exception UnwindFirstException(this Exception exception)
        {
            if (exception == null)
                return null;
            AggregateException aggregateException = exception as AggregateException;
            if (aggregateException == null)
                return exception;
            Exception innerException;
            do
            {
                innerException = aggregateException.InnerExceptions.FirstOrDefault();
                aggregateException = innerException as AggregateException;
            } while (aggregateException != null);
            return innerException;
        }
    }
}
