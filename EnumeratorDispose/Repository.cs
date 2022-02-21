using System.Collections.Generic;

namespace AboutCleanCode
{
    public class Repository
    {
        private int myTransactionsCount = 0;

        public int TransactionsCount => myTransactionsCount;

        public IEnumerable<string> FindAll()
        {
            try
            {
                myTransactionsCount++;

                yield return "a";
                yield return "b";
                yield return "c";
                yield return "d";
            }
            finally
            {
                myTransactionsCount--;
            }
        }
    }
}
