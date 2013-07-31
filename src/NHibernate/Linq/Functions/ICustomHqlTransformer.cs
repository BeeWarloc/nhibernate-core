using NHibernate.Hql.Ast;

namespace NHibernate.Linq.Functions
{
	public interface ICustomHqlTransformer
	{
		void Transform(HqlTreeNode hqlTree);
	}
}
