using CodeFirstTask.Entitys;
using Microsoft.EntityFrameworkCore;
using System;

namespace CodeFirstTask.Data
{
    public class AppDbcontext : DbContext
    {
        public AppDbcontext() { }

        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=D:\\c .net\\ITI LAB\\menuAPP\\CodeFirstTask\\CampanyCodeFirst.db");
            }
        }

        public DbSet<Employee> Employees { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Project> Projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Keys
            modelBuilder.Entity<Employee>().HasKey(e => e.EmployeeId);
            modelBuilder.Entity<Department>().HasKey(d => d.DepartmentId);
            modelBuilder.Entity<Project>().HasKey(p => p.ProjectId);

            // Relationships
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(d => d.Employess)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Many-to-Many Employee <-> Project
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Projects)
                .WithMany(p => p.Employess)
                .UsingEntity(j => j.ToTable("EmployeeProjects"));

          

            // Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1, DepartmentName = "HR" },
                new Department { DepartmentId = 2, DepartmentName = "IT" },
                new Department { DepartmentId = 3, DepartmentName = "Finance" },
                new Department { DepartmentId = 4, DepartmentName = "Marketing" },
                new Department { DepartmentId = 5, DepartmentName = "Sales" }
            );

            // Projects (ضيف StartDate Default عشان ما يكسرش)
            modelBuilder.Entity<Project>().HasData(
                new Project { ProjectId = 1, ProjectName = "ERP System", StartDate = DateTime.Now },
                new Project { ProjectId = 2, ProjectName = "Website Redesign", StartDate = DateTime.Now },
                new Project { ProjectId = 3, ProjectName = "Mobile App", StartDate = DateTime.Now },
                new Project { ProjectId = 4, ProjectName = "CRM Integration", StartDate = DateTime.Now },
                new Project { ProjectId = 5, ProjectName = "AI Chatbot", StartDate = DateTime.Now }
            );

            // Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FullName = "Ahmed Ali", DepartmentId = 1 },
                new Employee { EmployeeId = 2, FullName = "Sara Mohamed", DepartmentId = 2 },
                new Employee { EmployeeId = 3, FullName = "Omar Hassan", DepartmentId = 3 },
                new Employee { EmployeeId = 4, FullName = "Mona Ibrahim", DepartmentId = 4 },
                new Employee { EmployeeId = 5, FullName = "Khaled Mahmoud", DepartmentId = 5 }
            );
        }
    }
}

