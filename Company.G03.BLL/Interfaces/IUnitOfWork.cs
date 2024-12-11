using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeReprository EmployeeReprository { get; }
        public IDepartmentReprository DepartmentReprository { get; }
        Task<int> CompleteAsync();

    }
}
