using Company.G03.BLL.Interfaces;
using Company.G03.DAL.Data.Contexts;
using Company.G03.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.G03.BLL.Reprositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentReprository
    {
        private readonly AppDbContext _context;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
