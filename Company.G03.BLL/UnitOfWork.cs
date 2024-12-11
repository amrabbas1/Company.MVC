using Company.G03.BLL.Interfaces;
using Company.G03.BLL.Reprositories;
using Company.G03.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDepartmentReprository _departmentreprository;
        private IEmployeeReprository _employeereprository;
        private protected readonly AppDbContext _context;
        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _departmentreprository = new DepartmentRepository(context);
            _employeereprository = new EmployeeRepository(context);
        }
        public IEmployeeReprository EmployeeReprository => _employeereprository;

        public IDepartmentReprository DepartmentReprository => _departmentreprository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
