using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MVCAppG03DbContext context) : base(context)
        {
            
        }
    }
    //    public class DepartmentRepository : IDepartmentRepository
    //{
    //    private readonly MVCAppG03DbContext context;

    //    public DepartmentRepository(MVCAppG03DbContext dbCcontext)
    //    {
    //        this.context = dbCcontext;
    //    }
    //    public int Add(Department department)
    //    {
    //        context.Departments.Add(department);
    //        return context.SaveChanges();
            
    //    }

    //    public int Delete(Department department)
    //    {
    //        context.Departments.Remove(department);
    //        return context.SaveChanges();
    //    }

    //    public Department Get(int? id)
    //        => context.Departments.FirstOrDefault(D => D.Id == id);

    //    public IEnumerable<Department> GetAll()
    //        => context.Departments.ToList();

    //    public int Update(Department department)
    //    {
    //        context.Departments.Update(department);
    //        return context.SaveChanges();
    //    }
    //}
}
