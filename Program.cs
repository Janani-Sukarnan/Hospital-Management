
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem;

internal class Program
{
    private static readonly DepartmentService DepartmentService = new();

    private static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("====================================");
            Console.WriteLine("   HOSPITAL MANAGEMENT SYSTEM");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Department Management");
            Console.WriteLine("2. Patient Management");
            Console.WriteLine("3. Doctor Management");
            Console.WriteLine("4. Appointment Management");
            Console.WriteLine("5. Medical Records");
            Console.WriteLine("6. Billing Management");
            Console.WriteLine("7. Payment Management");
            Console.WriteLine("8. Audit Logs");
            Console.WriteLine("9. Reports");
            Console.WriteLine("0. Exit");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DepartmentMenu();
                        break;

                    case "2":
                        Console.WriteLine(
                            "Patient menu will be connected next.");
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine(
                            "Doctor menu will be connected next.");
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine(
                            "Appointment menu will be connected next.");
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine(
                            "Medical records menu will be connected next.");
                        Pause();
                        break;

                    case "6":
                        Console.WriteLine(
                            "Billing menu will be connected next.");
                        Pause();
                        break;

                    case "7":
                        Console.WriteLine(
                            "Payment menu will be connected next.");
                        Pause();
                        break;

                    case "8":
                        Console.WriteLine(
                            "Audit log menu will be connected next.");
                        Pause();
                        break;

                    case "9":
                        Console.WriteLine(
                            "Reports menu will be connected next.");
                        Pause();
                        break;

                    case "0":
                        Console.WriteLine("Exiting Hospital Management System...");
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        Pause();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DepartmentMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== DEPARTMENT MANAGEMENT =====");
            Console.WriteLine("1. Display All Departments");
            Console.WriteLine("2. Search Department by ID");
            Console.WriteLine("3. Add Department");
            Console.WriteLine("4. Update Department");
            Console.WriteLine("5. Deactivate Department");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayDepartments();
                        break;

                    case "2":
                        SearchDepartment();
                        break;

                    case "3":
                        AddDepartment();
                        break;

                    case "4":
                        UpdateDepartment();
                        break;

                    case "5":
                        DeactivateDepartment();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Invalid choice.");
                        Pause();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayDepartments()
    {
        List<Department> departments =
            DepartmentService.GetAllDepartments();

        Console.WriteLine("\nID | Department Name | Location | Active");
        Console.WriteLine("------------------------------------------");

        foreach (Department department in departments)
        {
            Console.WriteLine(
                $"{department.DepartmentId} | " +
                $"{department.DepartmentName} | " +
                $"{department.Location ?? "-"} | " +
                $"{department.IsActive}");
        }

        Pause();
    }

    private static void SearchDepartment()
    {
        int id = ReadInt("Enter department ID: ");

        Department? department =
            DepartmentService.GetDepartmentById(id);

        if (department == null)
        {
            Console.WriteLine("Department not found.");
        }
        else
        {
            Console.WriteLine($"ID: {department.DepartmentId}");
            Console.WriteLine($"Name: {department.DepartmentName}");
            Console.WriteLine($"Location: {department.Location ?? "-"}");
            Console.WriteLine($"Active: {department.IsActive}");
        }

        Pause();
    }

    private static void AddDepartment()
    {
        Console.Write("Enter department name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter location (optional): ");
        string? location = Console.ReadLine();

        int id = DepartmentService.AddDepartment(name, location);

        Console.WriteLine($"Department added successfully. ID: {id}");
        Pause();
    }

    private static void UpdateDepartment()
    {
        int id = ReadInt("Enter department ID: ");

        Console.Write("Enter new department name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter new location (optional): ");
        string? location = Console.ReadLine();

        bool updated =
            DepartmentService.UpdateDepartment(id, name, location);

        Console.WriteLine(
            updated ? "Department updated successfully."
                    : "Department not found.");

        Pause();
    }

    private static void DeactivateDepartment()
    {
        int id = ReadInt("Enter department ID: ");

        bool deactivated = DepartmentService.DeactivateDepartment(id);

        Console.WriteLine(
            deactivated ? "Department deactivated successfully."
                       : "Department not found or already inactive.");

        Pause();
    }

    private static int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);

            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}
