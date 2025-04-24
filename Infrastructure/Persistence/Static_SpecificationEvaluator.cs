using Domain;

namespace Presistence
{
    static class Static_SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, absSpecifications<T> specification) where T : class
        {
            var query = inputQuery; // dbcontext.Set<Product>();

            // Application des critères de filtrage
            if (specification.Criteria is not null) 
                query = query.Where(specification.Criteria);
           

            // Application des includes pour les propriétés de navigation
            query = specification.IncludeExpression.Aggregate(query,
                (current, includeExpression) => current.Include(includeExpression)
            );
            // _dbContext.Set<Product>().wWhere(P => P.Id 
            // _dbContext.Set<Product>().Where(P => P.Id ==1). Include(ProductBrand).Include(ProductBrand).Include(ProductType)
           
            if (specification.OrderBy is not null) 
                query = query.OrderBy(specification.OrderBy);
            
            else if (specification.OrderByDescending is not null) 
                query = query.OrderByDescending(specification.OrderByDescending);

            if (specification.IsPaginated)
                query = query.Skip(specification.Skip).Take(specification.Take);
            // _dbContext.Set<Product>().Where(P => P.Id ==1). Include(ProductBrand).Include(ProductBrand).Include(ProductType).Skip(10).take(5)



            return query;


            #region Explaine Aggregate() 
            //string[] strings = { "Ahmed", "Shalaan", "MAi" };

            //string  message =   "Hallo"  ;

            //message = strings.Aggregate(message, (str01, str02) => $"{ str01}{str02}"); 
            #endregion
        }
    }
     
}
// where<Expression<Func<T, bool>> expression> //Criteria
//.Include(Expression<Func<T, object>> expression) == Include(P.P.ProductBrand)
//.Include(Expression<Func<T, object>> expression) // Include