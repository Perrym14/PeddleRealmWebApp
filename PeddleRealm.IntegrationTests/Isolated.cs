using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Transactions;

namespace PeddleRealm.IntegrationTests
{
    //Any changes made to the database
    //will automatically be rolled back.
    public class Isolated : Attribute, ITestAction
    {
        private TransactionScope _transactionScope;

        public void BeforeTest(ITest test)
        {
            _transactionScope = new TransactionScope();
        }

        public void AfterTest(ITest test)
        {
            _transactionScope.Dispose();
        }

        //Dictates that this attribute can only
        //be applied to test methods.
        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }
        }

    }
}
