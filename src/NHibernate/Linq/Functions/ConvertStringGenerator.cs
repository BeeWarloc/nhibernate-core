using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq.Visitors;

namespace NHibernate.Linq.Functions
{
	public class ConvertStringGenerator : BaseHqlGeneratorForMethod
	{
		public ConvertStringGenerator()
		{
			SupportedMethods = new[] {
				ReflectionHelper.GetMethodDefinition<string>(x => Convert.ToInt32(x)),
				ReflectionHelper.GetMethodDefinition<string>(x => int.Parse(x)),
				ReflectionHelper.GetMethodDefinition<string>(x => Convert.ToDecimal(x)),
				ReflectionHelper.GetMethodDefinition<string>(x => decimal.Parse(x)),
				ReflectionHelper.GetMethodDefinition<string>(x => Convert.ToDouble(x)),
				ReflectionHelper.GetMethodDefinition<string>(x => double.Parse(x))
			};
		}
		public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments,
											HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
		{
			return treeBuilder.Cast(visitor.Visit(arguments[0]).AsExpression(), method.ReturnType);
		}
	}
}