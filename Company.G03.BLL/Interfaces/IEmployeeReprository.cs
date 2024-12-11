using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IEmployeeReprository : IGenericRepository<Employee>
    {
        //IEnumerable<Employee> GetAll();
        //Employee Get(int Id);
        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity);
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
    }
}
