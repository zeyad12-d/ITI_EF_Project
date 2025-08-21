using System;
using CodeFirstTask.Data;
using CodeFirstTask.Services;

class Program
{
    static async Task Main(string[] args)
    {
        var context = new AppDbcontext();
        var methods = new Methods(context);

        string[] mainMenu = { "Add", "Display", "Edit", "Assign", "Remove", "Exit" };
        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("MAIN MENU", mainMenu, ConsoleColor.Cyan, selectedIndex);

            switch (selectedIndex)
            {
                case 0: await AddMenu(methods); break;
                case 1: await DisplayMenu(methods); break;
                case 2: await EditMenu(methods); break;
                case 3: await AssignMenu(methods); break;
                case 4: await RemoveMenu(methods); break;
                case 5: return; // Exit
            }
        }
    }

    static int ShowMenu(string title, string[] options, ConsoleColor color, int selectedIndex = 0)
    {
        ConsoleKey key;
        do
        {
            Console.Clear();
            Console.ForegroundColor = color;
            Console.WriteLine($"\n=== {title} ===\n");
            Console.ResetColor();

            int centerX = Console.WindowWidth / 2;

            for (int i = 0; i < options.Length; i++)
            {
                string text = (i == selectedIndex) ? $"> {options[i]} <" : options[i];
                int left = centerX - text.Length / 2;
                Console.SetCursorPosition(left, Console.CursorTop);

                if (i == selectedIndex)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(text);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(text);
                }
            }

            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.UpArrow)
                selectedIndex = (selectedIndex == 0) ? options.Length - 1 : selectedIndex - 1;
            else if (key == ConsoleKey.DownArrow)
                selectedIndex = (selectedIndex + 1) % options.Length;

        } while (key != ConsoleKey.Enter);

        return selectedIndex;
    }

    // =================== ADD MENU ===================
    static async Task AddMenu(Methods methods)
    {
        string[] addMenu = { "Add Employee", "Add Department", "Add Project", "Back" };
        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("ADD MENU", addMenu, ConsoleColor.Green, selectedIndex);

            if (selectedIndex == addMenu.Length - 1) break;

            switch (selectedIndex)
            {
                case 0:
                    do
                    {
                        await methods.AddEmployeeAsync();
                        Console.Write("Add another employee? (y/n): ");
                    } while (Console.ReadLine()?.ToLower() == "y");
                    break;
                case 1:
                    do
                    {
                        await methods.AddDepartment();
                        Console.Write("Add another department? (y/n): ");
                    } while (Console.ReadLine()?.ToLower() == "y");
                    break;
                case 2:
                    do
                    {
                        await methods.AddProjectAsync();
                        Console.Write("Add another project? (y/n): ");
                    } while (Console.ReadLine()?.ToLower() == "y");
                    break;
            }
        }
    }

    // =================== DISPLAY MENU ===================
    static async Task DisplayMenu(Methods methods)
    {
        string[] displayMenu = { "Display Employees", "Display Departments", "Display Projects", "Back" };
        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("DISPLAY MENU", displayMenu, ConsoleColor.Blue, selectedIndex);

            if (selectedIndex == displayMenu.Length - 1) break;

            switch (selectedIndex)
            {
                case 0: await methods.DisplayEmployees(); break;
                case 1: await methods.DisplayDepartments(); break;
                case 2: await methods.DisplayProjects(); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    // =================== EDIT MENU ===================
    static async Task EditMenu(Methods methods)
    {
        string[] editMenu = {
            "Edit Employee",
            "Edit Department",
            "Edite project"
            ,"Back" };
        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("EDIT MENU", editMenu, ConsoleColor.Magenta, selectedIndex);

            if (selectedIndex == editMenu.Length - 1) break;

            switch (selectedIndex)
            {
                case 0: await methods.EditEmployessData(); break;
                case 1: await methods.EditDepartmentData(); break;
                case 3:await methods.EditProjectData(); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    // =================== ASSIGN MENU ===================
    static async Task AssignMenu(Methods methods)
    {
        string[] assignMenu = {
            "Assign Employee to Dept",
            "Assign Multiple Employees to Dept",
            "Assign to Project",
            "Back"
        };
        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("ASSIGN MENU", assignMenu, ConsoleColor.Yellow, selectedIndex);

            if (selectedIndex == assignMenu.Length - 1) break;

            switch (selectedIndex)
            {
                case 0: await methods.assignEmploessToDepartment(); break;
                case 1: await methods.AssignMultipleEmployeesToDepartmentAsync(); break;
                case 2: await methods.AssignEmplyesstoProject(); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    // =================== REMOVE MENU ===================
    static async Task RemoveMenu(Methods methods)
    {
        string[] removeMenu = {
            "Remove Employee from Project"
            ,"Remove Employee",
            "Remove Department",
            "Remove Project",
            "Back" };


        int selectedIndex = 0;

        while (true)
        {
            selectedIndex = ShowMenu("REMOVE MENU", removeMenu, ConsoleColor.Red, selectedIndex);

            if (selectedIndex == removeMenu.Length - 1) break;

            switch (selectedIndex)
            {
                case 0: await methods.RemoveEmployesFormProject(); break;
                case 1: await methods.RemoveEmployee(); break;
                case 2: await methods.RemoveDeparmtent(); break;
                case 3: await methods.RemoveProject(); break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}
