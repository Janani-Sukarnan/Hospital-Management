
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem;

internal class Program
{
    private static readonly DepartmentService DepartmentService = new();
    private static readonly PatientService PatientService = new();
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
                        PatientMenu();
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

    private static void PatientMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== PATIENT MANAGEMENT =====");
            Console.WriteLine("1. Display All Patients");
            Console.WriteLine("2. Search Patient by ID");
            Console.WriteLine("3. Register New Patient");
            Console.WriteLine("4. Update Patient");
            Console.WriteLine("5. Deactivate Patient");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayPatients();
                        break;

                    case "2":
                        SearchPatient();
                        break;

                    case "3":
                        RegisterPatient();
                        break;

                    case "4":
                        UpdatePatient();
                        break;

                    case "5":
                        DeactivatePatient();
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

    private static void DisplayPatients()
    {
        List<Patient> patients = PatientService.GetAllPatients();

        Console.WriteLine("\nID | Name | DOB | Gender | Phone | Active");
        Console.WriteLine("-------------------------------------------");

        foreach (Patient patient in patients)
        {
            Console.WriteLine(
                $"{patient.PatientId} | {patient.FullName} | " +
                $"{patient.DateOfBirth:yyyy-MM-dd} | {patient.Gender} | " +
                $"{patient.Phone} | {patient.IsActive}");
        }

        Pause();
    }

    private static void SearchPatient()
    {
        int id = ReadInt("Enter patient ID: ");

        Patient? patient = PatientService.GetPatientById(id);

        if (patient == null)
        {
            Console.WriteLine("Patient not found.");
        }
        else
        {
            Console.WriteLine($"ID: {patient.PatientId}");
            Console.WriteLine($"Name: {patient.FullName}");
            Console.WriteLine($"Date of Birth: {patient.DateOfBirth:yyyy-MM-dd}");
            Console.WriteLine($"Gender: {patient.Gender}");
            Console.WriteLine($"Phone: {patient.Phone}");
            Console.WriteLine($"Email: {patient.Email ?? "-"}");
            Console.WriteLine($"Address: {patient.Address ?? "-"}");
            Console.WriteLine($"Active: {patient.IsActive}");
        }

        Pause();
    }

    private static void RegisterPatient()
    {
        Console.Write("Enter full name: ");
        string name = Console.ReadLine() ?? "";

        DateTime dob;
        while (true)
        {
            Console.Write("Enter date of birth (yyyy-MM-dd): ");

            if (DateTime.TryParseExact(
                Console.ReadLine(),
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dob))
                break;

            Console.WriteLine("Invalid date. Use yyyy-MM-dd.");
        }

        Console.Write("Enter gender (Male/Female/Other/Prefer not to say): ");
        string gender = Console.ReadLine() ?? "";

        Console.Write("Enter phone: ");
        string phone = Console.ReadLine() ?? "";

        Console.Write("Enter email (optional): ");
        string? email = Console.ReadLine();

        Console.Write("Enter address (optional): ");
        string? address = Console.ReadLine();

        Patient patient = new Patient
        {
            FullName = name,
            DateOfBirth = dob,
            Gender = gender,
            Phone = phone,
            Email = email,
            Address = address
        };

        int id = PatientService.RegisterPatient(patient);

        Console.WriteLine($"Patient registered successfully. ID: {id}");
        Pause();
    }

    private static void UpdatePatient()
    {
        int id = ReadInt("Enter patient ID to update: ");

        Patient? existing = PatientService.GetPatientById(id);

        if (existing == null)
        {
            Console.WriteLine("Patient not found.");
            Pause();
            return;
        }

        Console.Write($"Enter full name ({existing.FullName}): ");
        string? name = Console.ReadLine();

        Console.Write($"Enter date of birth yyyy-MM-dd ({existing.DateOfBirth:yyyy-MM-dd}): ");
        string? dobInput = Console.ReadLine();

        DateTime dob = existing.DateOfBirth;

        if (!string.IsNullOrWhiteSpace(dobInput) &&
            !DateTime.TryParseExact(
                dobInput,
                "yyyy-MM-dd",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out dob))
        {
            Console.WriteLine("Invalid date format.");
            Pause();
            return;
        }

        Console.Write($"Enter gender ({existing.Gender}): ");
        string? gender = Console.ReadLine();

        Console.Write($"Enter phone ({existing.Phone}): ");
        string? phone = Console.ReadLine();

        Console.Write($"Enter email ({existing.Email ?? "-"}): ");
        string? email = Console.ReadLine();

        Console.Write($"Enter address ({existing.Address ?? "-"}): ");
        string? address = Console.ReadLine();

        Patient updated = new Patient
        {
            PatientId = id,
            FullName = string.IsNullOrWhiteSpace(name) ? existing.FullName : name,
            DateOfBirth = dob,
            Gender = string.IsNullOrWhiteSpace(gender) ? existing.Gender : gender,
            Phone = string.IsNullOrWhiteSpace(phone) ? existing.Phone : phone,
            Email = string.IsNullOrWhiteSpace(email) ? existing.Email : email,
            Address = string.IsNullOrWhiteSpace(address) ? existing.Address : address,
            IsActive = existing.IsActive,
            RegisteredAt = existing.RegisteredAt
        };

        bool success = PatientService.UpdatePatient(updated);

        Console.WriteLine(success
            ? "Patient updated successfully."
            : "Patient could not be updated.");

        Pause();
    }

    private static void DeactivatePatient()
    {
        int id = ReadInt("Enter patient ID to deactivate: ");

        Console.Write("Are you sure? Enter Y to confirm: ");
        string? confirmation = Console.ReadLine();

        if (!string.Equals(
            confirmation, "Y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Deactivation cancelled.");
            Pause();
            return;
        }

        bool success = PatientService.DeactivatePatient(id);

        Console.WriteLine(success
            ? "Patient deactivated successfully."
            : "Patient not found or already inactive.");

        Pause();
    }


}
