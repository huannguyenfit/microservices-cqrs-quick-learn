using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Test.Core.Data
{
	public static class ExpressionExtensions
	{
		public static Expression ReplaceParameter(this Expression expression, ParameterExpression source, Expression target)
		{
			return new ParameterReplacer { Source = source, Target = target }.Visit(expression);
		}

		class ParameterReplacer : ExpressionVisitor
		{
			public ParameterExpression Source;
			public Expression Target;
			protected override Expression VisitParameter(ParameterExpression node)
				=> node == Source ? Target : base.VisitParameter(node);
		}
	}
	public static class QueryableExtensions
    {
		public static IQueryable<T> ApplyLike<T>(this IQueryable<T> dataQueryable, string searchValue, params Expression<Func<T, string>>[] columnExpressions)
		{
			if (string.IsNullOrEmpty(searchValue) || columnExpressions == null || columnExpressions.Length == 0)
				return dataQueryable;
			var entityParam = Expression.Parameter(typeof(T), "entity");
			Expression[] valueExpressions = new Expression[columnExpressions.Length];

			for (int i = 0; i < columnExpressions.Length; i++)
			{
				valueExpressions[i] = columnExpressions[i].Body.ReplaceParameter(columnExpressions[i].Parameters[0], entityParam);
			}
			Expression<Func<T, bool>> lastCondition = x => false;
			foreach (Expression valueExpression in valueExpressions)
			{
				Expression<Func<string, bool>> likeExpression = d => EF.Functions.Like(d, $"%{searchValue}%");
				var likeValueExpression = likeExpression.Body.ReplaceParameter(likeExpression.Parameters[0], valueExpression);
				var condition = Expression.Lambda<Func<T, bool>>(likeValueExpression, entityParam);
				lastCondition = Or(lastCondition, condition);
			}
			return dataQueryable.Where(lastCondition);
		}

		private static Expression<Func<T, Boolean>> Or<T>(
			Expression<Func<T, Boolean>> expressionOne,
			Expression<Func<T, Boolean>> expressionTwo
		)
		{
			var invokedSecond = Expression.Invoke(expressionTwo, expressionOne.Parameters.Cast<Expression>());

			return Expression.Lambda<Func<T, Boolean>>(
				Expression.Or(expressionOne.Body, invokedSecond), expressionOne.Parameters
			);

		}
    }
}
