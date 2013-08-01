using System;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Hql.Ast;
using NHibernate.Linq.Functions;
using NUnit.Framework;
using Environment = NHibernate.Cfg.Environment;

namespace NHibernate.Test.Linq
{
	[TestFixture]
	public class LinqToHqlTreeNodeVisitorTests : LinqTestCase
	{
		[ThreadStatic] private static bool testNodeVisitorHasBeenCalled;

		private class TestNodeVisitor : ILinqToHqlTreeNodeVisitor
		{
			public void Visit(HqlTreeNode hqlTree)
			{
				testNodeVisitorHasBeenCalled = true;
			}
		}

		protected override void Configure(Configuration configuration)
		{
			configuration.Properties[Environment.LinqToHqlTreeNodeVisitor] =
				typeof (TestNodeVisitor).AssemblyQualifiedName;
			base.Configure(configuration);
		}

		[Test]
		public void LinqToHqlTreeNodeVisitorIsCalledDuringQuery()
		{
			try
			{
				db.Orders.Where(x => x.OrderId >= 1).ToList();
				Assert.IsTrue(testNodeVisitorHasBeenCalled, "Test node visitor was not called.");
			}
			finally
			{
				testNodeVisitorHasBeenCalled = false;
			}
		}
	}
}