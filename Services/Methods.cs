using CodeFirstTask.Data;
using CodeFirstTask.Entitys;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace CodeFirstTask.Services
{
  
    public class Methods
    {

        public readonly AppDbcontext _context;

        public Methods(AppDbcontext context)
        {
            _context = context;
        }
        //Add Employee, Department, and Project
        public async Task AddEmployeeAsync()
        {
            Console.WriteLine("Enter Employess data");
            Console.Write("Name:");
            string name = Console.ReadLine();
            Console.Write("DepartmentId:");
            int DepartmentID= int.Parse(Console.ReadLine());

            var emp = new Employee()
            {
                DepartmentId = DepartmentID,
                FullName = name
            };
            await _context.AddAsync(emp);
            await _context.SaveChangesAsync();
        }

        public async Task AddDepartment()
        {
            Console.WriteLine("Enter Department Data");

            Console.Write("Department Name: ");
            string DepartmentName = Console.ReadLine();
            var department = new Department()
            {
               DepartmentName =DepartmentName
            };
            await _context.AddAsync(department);
            await _context.SaveChangesAsync();
        }


         public async Task AddProjectAsync()
        {
            Console.WriteLine("Enter Project Data");
            Console.Write("Project Name: ");
            string projectName = Console.ReadLine();
            var project = new Project()
            {
                ProjectName = projectName
            };
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();
        }

        // edit Employee, and remove employee,
        public async Task EditEmployessData( )
        {
            var emp = await _context.Employees.Include(e=>e.Department).ToListAsync();

            if (emp.Count == 0)
            {
                Console.WriteLine("No Employees found.");
                return;
            }
            foreach (var item in emp)
            {
                Console.WriteLine($"Id: {item.EmployeeId}, Name: {item.FullName}, DepartmentName: {item.Department.DepartmentName}");
            }
            Console.WriteLine("Enter Employess id To edit");
            int id = int.Parse(Console.ReadLine());
            var EditEmployes = _context.Employees.Find( id);
            if (EditEmployes == null)
            {
                Console.WriteLine("unValid Number");
            }

            Console.Write("Enter New Employess Name:");
            string newName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newName))
            {
                EditEmployes.FullName = newName;

            }
          
            Console.Write("Enter deparmentID :");
            if (int.TryParse(Console.ReadLine(), out int newDepartmentId))
            {
                EditEmployes.DepartmentId = newDepartmentId;
            }
            else
            {
                Console.WriteLine("Invalid Department ID.");
            }


            _context.Employees.Update(EditEmployes);
            await _context.SaveChangesAsync();
            Console.WriteLine("Employee updated successfully.");

        }

        public async Task assignEmploessToDepartment()
        {
            var dep= await _context.Departments.ToListAsync();
            if (dep.Count ==0)
            {
                Console.WriteLine(" Depatment Not Found");
                return;
            }
            foreach (var item in dep)
            {
                Console.WriteLine($" DepartmetID : {item.DepartmentId }, department Name :{ item.DepartmentName}");
            }
            Console.WriteLine("Enter Department To assgin Employess");

            int departmentId = int.Parse(Console.ReadLine());
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
            {
                Console.WriteLine("Department not found.");
                return;
            }
            var employees = await _context.Employees.ToListAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id: {employee.EmployeeId}, Name: {employee.FullName}");
            }
            Console.WriteLine("Enter Employee ID to assign to the department:");
            int employeeId = int.Parse(Console.ReadLine());

            var employeeToAssign = await _context.Employees.FindAsync(employeeId);

            if (employeeToAssign == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }
            employeeToAssign.DepartmentId = departmentId;
            _context.Employees.Update(employeeToAssign);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Employee {employeeToAssign.FullName} assigned to department {department.DepartmentName} successfully.");

        }
        public async Task AssignMultipleEmployeesToDepartmentAsync()
        {
            
            var departments = await _context.Departments.ToListAsync();
            if (!departments.Any())
            {
                Console.WriteLine("No departments found.");
                return;
            }

            foreach (var dep in departments)
            {
                Console.WriteLine($"Id: {dep.DepartmentId}, Name: {dep.DepartmentName}");
            }

            Console.Write("Enter Department ID to assign employees to: ");
            if (!int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
            {
                Console.WriteLine("Department not found.");
                return;
            }

           
            var employees = await _context.Employees.ToListAsync();
            if (!employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            foreach (var emp in employees)
            {
                Console.WriteLine($"Id: {emp.EmployeeId}, Name: {emp.FullName}, Current Dept: {emp.DepartmentId}");
            }

            Console.WriteLine("Enter Employee IDs to assign (separated by comma, e.g. 1,2,3): ");
            string input = Console.ReadLine();

            var ids = input.Split(',')
                           .Select(id => int.TryParse(id, out int empId) ? empId : -1)
                           .Where(id => id > 0)
                           .ToList();

            var employeesToAssign = employees.Where(e => ids.Contains(e.EmployeeId)).ToList();

            if (!employeesToAssign.Any())
            {
                Console.WriteLine("No valid employees selected.");
                return;
            }

            foreach (var emp in employeesToAssign)
            {
                emp.DepartmentId = departmentId;
            }

            _context.Employees.UpdateRange(employeesToAssign);
            await _context.SaveChangesAsync();

            Console.WriteLine($"{employeesToAssign.Count} employees assigned to {department.DepartmentName} successfully.");
        }

        public async Task AssignEmplyesstoProject()
        {
            var projects = await _context.Projects.ToListAsync();
            if (!projects.Any())
            {
                Console.WriteLine("No projects found.");
                return;
            }

            foreach (var proj in projects)
            {
                Console.WriteLine($"ProjectID: {proj.ProjectId}, Name: {proj.ProjectName}");
            }

            Console.Write("Enter Project ID: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid Project ID.");
                return;
            }

            var project = await _context.Projects
                .Include(p => p.Employess)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null)
            {
                Console.WriteLine("Project not found.");
                return;
            }

            var employees = await _context.Employees.ToListAsync();
            if (!employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            foreach (var emp in employees)
            {
                Console.WriteLine($"EmployeeID: {emp.EmployeeId}, Name: {emp.FullName}");
            }

            Console.Write("Enter Employee ID: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Invalid Employee ID.");
                return;
            }

            var employee = await _context.  Employees.FindAsync(employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            project.Employess.Add(employee);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Employee {employee.FullName} assigned to project {project.ProjectName} successfully.");
        }


        public async Task RemoveEmployesFormProject()
        {
            Console.Write("Enter Project ID: ");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid Project ID.");
                return;
            }

            var project = await _context.Projects
                .Include(p => p.Employess)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);

            if (project == null)
            {
                Console.WriteLine("Project not found.");
                return;
            }

            if (!project.Employess.Any())
            {
                Console.WriteLine("No employees assigned to this project.");
                return;
            }

            foreach (var emp in project.Employess)
            {
                Console.WriteLine($"EmployeeID: {emp.EmployeeId}, Name: {emp.FullName}");
            }

            Console.Write("Enter Employee ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Invalid Employee ID.");
                return;
            }

            var employee = project.Employess.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not assigned to this project.");
                return;
            }

            project.Employess.Remove(employee);
            await _context.SaveChangesAsync();

            Console.WriteLine($"Employee {employee.FullName} removed from project {project.ProjectName}.");
        }


        // Edit Department data
        public async Task EditDepartmentData()
        {
            var departments = await _context.Departments.ToListAsync();
            if (!departments.Any())
            {
                Console.WriteLine(" No deparment");
                return;
            }
            foreach (var dep in departments)
            {
                Console.WriteLine($"Id: {dep.DepartmentId}, Name: {dep.DepartmentName}");
            }
            Console.WriteLine("Enter Department ID to edit:");
            if (!int.TryParse(Console.ReadLine(), out int departmentId))
            {
                Console.WriteLine("Invalid Department ID.");
                return;
            }
            var department = await _context.Departments.FindAsync(departmentId);
            if (department == null)
            {
                Console.WriteLine("Department not found.");
                return;
            }
            Console.Write("Enter New Department Name : ");
            string newDepartmentName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newDepartmentName))
            {
                department.DepartmentName = newDepartmentName;
            }
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();

            Console.WriteLine("Department updated successfully.");

        }
        // edit Project data
        public async Task EditProjectData()
        {
            var projects = await _context.Projects.ToListAsync();
            if (!projects.Any())
            {
                Console.WriteLine("No projects found.");
                return;
            }
            foreach (var project in projects)
            {
                Console.WriteLine($"Id: {project.ProjectId}, Name: {project.ProjectName}");
            }
            Console.WriteLine("Enter Project ID to edit:");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid Project ID.");
                return;
            }
            var proj = await _context.Projects.FindAsync(projectId);
            if (proj == null)
            {
                Console.WriteLine("Project not found.");
                return;
            }
            Console.Write("Enter New Project Name : ");
            string newProjectName = Console.ReadLine();
            if (!string.IsNullOrEmpty(newProjectName))
            {
                proj.ProjectName = newProjectName;
            }
            _context.Projects.Update(proj);
            await _context.SaveChangesAsync();
            Console.WriteLine("Project updated successfully.");
        }

        // Remove Employee, Department, and Project

        public async Task RemoveEmployee()
        {
            var employes= await _context.Employees.ToListAsync();

            if(!employes.Any())
            {
                Console.WriteLine("No Employees found.");
                return;
            }
            foreach (var emp in employes)
            {
                Console.WriteLine($"Id: {emp.EmployeeId}, Name: {emp.FullName}");
            }
            Console.WriteLine("Enter Employess ID to Remove");
            if (!int.TryParse(Console.ReadLine(), out int employeeId))
            {
                Console.WriteLine("Invalid Employee ID.");
                return;
            }
            var employee = await _context.Employees.FindAsync(employeeId);
            if (employee == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }
            _context.Employees.Remove(employee);

            await _context.SaveChangesAsync();
            Console.WriteLine($"Employee{employee.FullName} removed successfully.");
        }

        public async Task RemoveDeparmtent() 
        {
            var departments = await _context.Departments.ToListAsync();
            if (!departments.Any())
            {
                Console.WriteLine("Department Not Found");
            }
            foreach (var department in departments)
            {
                Console.WriteLine($"DepartmentID: {department.DepartmentId} ,department Name :{department.DepartmentName} ");
            }
            Console.WriteLine("Enter DepartmentID");
            int depid= int.Parse(Console.ReadLine());
            var DepartmentRemoved = await _context.Departments.FindAsync(depid);

            if (DepartmentRemoved == null)
            {
                Console.WriteLine("invaild id");
                return;
            }
            _context.Remove(DepartmentRemoved);
            await _context.SaveChangesAsync();
            Console.WriteLine($"departemnt removed {DepartmentRemoved.DepartmentName} removed succssfully");

        }
        public async Task RemoveProject()
        {

            var projects = await _context.Projects.ToListAsync();
            if (!projects.Any())
            {
                Console.WriteLine("No projects found.");
                return;


            }
            foreach (var project in projects)
            {
                Console.WriteLine($"ProjectID: {project.ProjectId}, Project Name: {project.ProjectName}");
            }
            Console.WriteLine("Enter Project ID to remove:");
            if (!int.TryParse(Console.ReadLine(), out int projectId))
            {
                Console.WriteLine("Invalid Project ID.");
                return;
            }
            var projectToRemove = await _context.Projects.FindAsync(projectId);
            if (projectToRemove == null)
            {
                Console.WriteLine("Project not found.");
                return;
            }
            _context.Projects.Remove(projectToRemove);
            await _context.SaveChangesAsync();
            Console.WriteLine($"Project {projectToRemove.ProjectName} removed successfully.");
        }

        // display all Employees, Departments, and Projects

        public async Task DisplayEmployees()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Projects)
                .Select(e => new
                {
                    e.EmployeeId,
                    e.FullName,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : "No Department",
                    Projects = e.Projects.Select(p => p.ProjectName).ToList()
                })
                .ToListAsync();

            if (!employees.Any())
            {
                Console.WriteLine("No employees found.");
                return;
            }

            foreach (var emp in employees)
            {
                Console.WriteLine($"Id: {emp.EmployeeId}, Name: {emp.FullName}, Department: {emp.DepartmentName}");
                Console.WriteLine(emp.Projects.Any()
                    ? $"Projects: {string.Join(", ", emp.Projects)}"
                    : "Projects: No Projects");
                Console.WriteLine(new string('-', 50));
            }
        }


        public async Task DisplayDepartments()
        {
            var departments = await _context.Departments
                .Include(d => d.Employess)
                .Select(d => new
                {
                    d.DepartmentId,
                    d.DepartmentName,
                    Employees = d.Employess.Select(e => e.FullName).ToList()
                })
                .ToListAsync();

            if (!departments.Any())
            {
                Console.WriteLine("No departments found.");
                return;
            }

            foreach (var dep in departments)
            {
                Console.WriteLine($"Id: {dep.DepartmentId}, Name: {dep.DepartmentName}");
                Console.WriteLine(dep.Employees.Any()
                    ? $"Employees: {string.Join(", ", dep.Employees)}"
                    : "Employees: No Employees");
                Console.WriteLine(new string('-', 50));
            }
        }


        public async Task DisplayProjects()
        {
            var projects = await _context.Projects
                .Include(p => p.Employess)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    Employees = p.Employess.Select(e => e.FullName).ToList()
                })
                .ToListAsync();

            if (!projects.Any())
            {
                Console.WriteLine("No projects found.");
                return;
            }

            foreach (var proj in projects)
            {
                Console.WriteLine($"Id: {proj.ProjectId}, Name: {proj.ProjectName}");
                Console.WriteLine(proj.Employees.Any()
                    ? $"Employees: {string.Join(", ", proj.Employees)}"
                    : "Employees: No Employees");
                Console.WriteLine(new string('-', 50));
            }
        }




        }

    }

