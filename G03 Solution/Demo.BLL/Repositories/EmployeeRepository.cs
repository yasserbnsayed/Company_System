using Demo.BLL.Interfaces;
using Demo.DAL.Contexts;
using Demo.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository//[ctrl+.]
    {
        public MVCAppG03DbContext Context { get; }
        public EmployeeRepository(MVCAppG03DbContext context) : base(context)
        {
            Context = context;
        }
        public IEnumerable<Employee> GetEmployeesByDepartmentName(string departmentName)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Employee>> SearchEmployee(string value)
            => await Context.Employees.Where(E => E.Name.Contains(value)).ToListAsync();
    }

    //    public class EmployeeRepository : IEmployeeRepository
    //{
    //    private readonly MVCAppG03DbContext context;

    //    public EmployeeRepository(MVCAppG03DbContext context) // ctrl+.
    //    {
    //        this.context = context;
    //    }

    //    public int Add(Employee Employee)
    //    {
    //        context.Employees.Add(Employee);
    //        return context.SaveChanges();

    //    }

    //    public int Delete(Employee Employee)
    //    {
    //        context.Employees.Remove(Employee);
    //        return context.SaveChanges();
    //    }

    //    public Employee Get(int? id)
    //        => context.Employees.FirstOrDefault(D => D.Id == id);

    //    public IEnumerable<Employee> GetAll()
    //        => context.Employees.ToList();

    //    public int Update(Employee Employee)
    //    {
    //        context.Employees.Update(Employee);
    //        return context.SaveChanges();
    //    }



    //}
}
