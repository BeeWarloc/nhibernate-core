using System.Linq;
using System.Linq.Expressions;
using NHibernate.Linq.Visitors;
using Remotion.Linq;
using Remotion.Linq.Clauses;

namespace NHibernate.Linq.ReWriters
{
	public class UnwrapOrderByExpressionFromAnonClassRewriter : NhExpressionTreeVisitor
	{
		public static void ReWrite(QueryModel queryModel)
		{
			var visitor = new UnwrapOrderByExpressionFromAnonClassRewriter();
			queryModel.TransformExpressions(visitor.VisitExpression);
		}

		private static bool IsAnonymous(System.Type type)
		{
			return type.Name.StartsWith("<>f__AnonymousType");
		}

		protected override Expression VisitMemberExpression(MemberExpression expression)
		{
			if (IsAnonymous(expression.Member.DeclaringType) && expression.Expression.NodeType == ExpressionType.New)
			{
				// Find matching constructor arg
				var newExpr = (NewExpression) expression.Expression;
				var ctor = newExpr.Constructor;
				var ctorParam =
					ctor.GetParameters()
					.Select((x, i) => new {Index = i, x.Name})
					.FirstOrDefault(x => x.Name == expression.Member.Name);

				if (ctorParam != null)
				{
					return newExpr.Arguments[ctorParam.Index];
				}
			}
			return base.VisitMemberExpression(expression);
		}
	}
}