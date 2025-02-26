
using System.Linq.Expressions;

namespace Insurance.DataAccess.Repository.IRepository
{

    //Generic Repository/Service Interface
    //When IRepository will be implemented at that time we will know
    //On what class the IRepository is implemented
    public  interface IRepository<T> where T : class
    {
        //T -Any generic model on which we want to perform CRUD operations
        // Or on which we want to interact with DB Context

        //returns All records
        IEnumerable<T> GetAll();

        //when we have to retrieve one item to display

        //We will be getting a link equation
        //and out result will be a boolean
        //So when we pass a lambda expression to this function this is the syntax for that
        //Lambda expression: M.Name == "John"
        T Get(Expression<Func<T,bool>> filter);

        void Add(T entity);
        //void Update(T entity);  This will be added when implementing interface
        void Remove(T entity);

        //delete multiple records in a table
        //we receive IEnumerable or collection of entites
        void RemoveRange(IEnumerable<T> entity);
    }
}
