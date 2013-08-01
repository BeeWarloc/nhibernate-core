using NHibernate.Hql.Ast;

namespace NHibernate.Linq.Functions
{
	public interface ILinqToHqlTreeNodeVisitor
	{
		void Visit(HqlTreeNode hqlTree);
	}
}
