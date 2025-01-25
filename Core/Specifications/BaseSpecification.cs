using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
    protected BaseSpecification(): this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip  { get; private set; }

    public bool IsPagingEnabled  { get; private set; }

    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression) => OrderBy = orderByExpression;
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByExpression) => OrderByDescending = orderByExpression;
    protected void AddDistinct() => IsDistinct = true;
    protected void ApplyPaging(int skip, int take) => (Skip, Take, IsPagingEnabled) = (skip, take, true);

    public IQueryable<T> ApplyCriteria(IQueryable<T> query) => Criteria != null ? query.Where(Criteria) : query;
}

public class BaseSpecification<T, TResult> (Expression<Func<T, bool>>? criteria): BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseSpecification(): this(null) { }

    public Expression<Func<T, TResult>>? Select  { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression) => Select = selectExpression;

    
}
