
using Insurance.DataAccess.Data;
using Insurance.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Insurance.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        //<T> means we are implementing a generic repository
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //setting dbSet to current generic set that we get from the context
            //For e.g : If we create a generic class on category then dbSet will be set to "categories"
            this.dbSet = _db.Set<T>();

        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //Expression<Func<T, bool>> filter: This is a fancy way of saying
        //that the method accepts a condition (like a search query)
        //that will be used to filter the items.
        //For example, it could be a condition like "find a user with a specific email."
        public T Get(Expression<Func<T, bool>> filter)
        {

            /* Method simple explained */
            /*Starting Point: Imagine you have a list of all the items in the database.

            Filtering: You apply a condition to this list to narrow it down (e.g., find all items where the name is "John").

            Fetching Result: You then take the first item from this filtered list. If there's no match, you get nothing.*/

            //dbSet represents the table in the database for the specific type T.
            //The query variable starts with all the items from this table.

            //IQueryable<T>: This is an interface in C# that represents a queryable collection of data.
            //It is designed to work with LINQ (Language Integrated Query) to query data from a data source like a database.

            IQueryable<T> query = dbSet;

            //When you use IQueryable<T>, the query is not executed immediately. Instead,
            //it is executed when you actually iterate over the results
            //(e.g., using ToList(), FirstOrDefault()). This is called deferred execution

            //This line applies the filter condition to the query.
            //So, if the filter is "find users with the first name 'John',"
            //then the query now contains only those users.
            query = query.Where(filter);

            //This line fetches the first item from the filtered results.
            //If no items match the filter condition, it returns null

            return query.FirstOrDefault();

            //the above code is same as Category? categoryfromDb = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();

        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            //removes all the items/records that are passed to the entity
            dbSet.RemoveRange(entity);
        }
    }
}
