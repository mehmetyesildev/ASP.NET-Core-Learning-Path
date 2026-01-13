using razorpagesExample.Models;

namespace razorpagesExample.Repository;

public class MockEmployeeRepository : IEmployeeRepository
{
    private List<Employee> _employeeList;
    public MockEmployeeRepository()
    {
        _employeeList=new List<Employee>()
        {
            new(){Id=1,Name="Ahmet Yılmaz",Email="ahmetyılmaz@gmail.com",photo="1.jpg",Department="Muhasebe"},
            new(){Id=2,Name="Hasan can",Email="hasancengiz@gmail.com",photo="2.jpg",Department="Muhasebe"},
            new(){Id=3,Name="ali Yılmaz",Email="aliyılmaz@gmail.com",photo="3.jpg",Department="Muhasebe"},
            new(){Id=4,Name="mehmet gül",Email="mehmetgul@gmail.com",photo="4.jpg",Department="Muhasebe"},
        };
    }
    public IEnumerable<Employee> GetAll()
    {
        return _employeeList;
    }

    public Employee GetById(int id)
    {
        return _employeeList.FirstOrDefault(e=>e.Id==id);
    }

    public Employee Update(Employee entity)
    {
        Employee employee=_employeeList.FirstOrDefault(e=>e.Id==entity.Id);
        if(employee !=null)
        {
            employee.Name=entity.Name;
            employee.Email=entity.Email;
            employee.photo=entity.photo;
            employee.Department = entity.Department;
        }
        return employee;
    }    
    
}