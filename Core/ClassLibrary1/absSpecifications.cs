using System.Linq.Expressions;

namespace Domain
{
    public abstract class absSpecifications<T> where T : class
    {
        //property for Each Evrey Specification

        //When Criteria is null ==> return all data
        public Expression<Func<T, bool>>? Criteria { get; set; } //where
        public List<Expression<Func<T, Object>>> IncludeExpression { get; set; } = new();
        public Expression<Func<T, Object>>? OrderBy { get; private set; }
        public Expression<Func<T, Object>>? OrderByDescending { get; private set; }


        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPaginated { get; set; }




        protected absSpecifications(Expression<Func<T, bool>>? criteria)
        {
            Criteria = criteria;
            //IncludeExpression = new List<Expression<Func<T, Object>>>();
        }
        protected absSpecifications()
        {

        }
        protected void AddInclude(Expression<Func<T, Object>> expression) => IncludeExpression.Add(expression);
        protected void SetOrderByasce(Expression<Func<T, Object>> oexpression) => OrderBy = oexpression;
        protected void SetOrderByDesce(Expression<Func<T, Object>> expression) => OrderByDescending = expression;

        //totalproducts=20
        //pagesize =5
        //pageindex = 2

        //Skip = (pageindex - 1) * pagesize

        protected void ApplyPagination(int pageIndex , int pageSize)
        {
            Skip = (pageIndex - 1) * pageSize;
            Take = pageSize;
            IsPaginated = true;
        }

        //AddOrderBy
        //AddOrderByDescending



    }
    // _dbcontext.Set<product>() 
    // where<Expression<Func<T, bool>> expression> //Criteria
    //.Include(Expression<Func<T, object>> expression) == Include(P.P.ProductBrand) ==>list
    //.Include(Expression<Func<T, object>> expression) // Include
    //=====================
    //Filtering , sorting 
    //Filter ==> BrandId , Typeld
    //Sort ==> Price[Asc,desc] , Name[Asc,desc]
    //orderBy,orderByDescending
    //OrderBy ==> Expression<Func<T, object>>  
    //OrderByDescending ==> Expression<Func<T, object>>  
    //Skip 
    //Take

}

