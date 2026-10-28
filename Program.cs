
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Services;

namespace HospitalManagementSystem;

internal class Program
{
    private static readonly DepartmentService DepartmentService = new();
    private static readonly PatientService PatientService = new();
    private static readonly DoctorService DoctorService = new();
    private static readonly AppointmentService AppointmentService = new();
    private static readonly MedicalRecordService MedicalRecordService = new();
    private static readonly BillingService BillingService = new();
    private static readonly PaymentService PaymentService = new();
    private static readonly AuditLogService AuditLogService = new();
    private static readonly ReportService ReportService = new();
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
                        DoctorMenu();
                        break;

                    case "4":
                        AppointmentMenu();
                        break;

                    case "5":
                        MedicalRecordMenu();
                        break;

                    case "6":
                        BillingMenu();
                        break;

                    case "7":
                        PaymentMenu();
                        break;

                    case "8":
                        AuditLogMenu();
                        break;

                    case "9":
                        ReportsMenu();
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

    private static void DoctorMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== DOCTOR MANAGEMENT =====");
            Console.WriteLine("1. Display All Doctors");
            Console.WriteLine("2. Search Doctor by ID");
            Console.WriteLine("3. Add Doctor");
            Console.WriteLine("4. Update Doctor");
            Console.WriteLine("5. Deactivate Doctor");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayDoctors();
                        break;
                    case "2":
                        SearchDoctor();
                        break;
                    case "3":
                        AddDoctor();
                        break;
                    case "4":
                        UpdateDoctor();
                        break;
                    case "5":
                        DeactivateDoctor();
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

    private static void DisplayDoctors()
    {
        List<Doctor> doctors = DoctorService.GetAllDoctors();

        Console.WriteLine("\nID | Name | Specialization | Department ID | Fee | Active");
        Console.WriteLine("----------------------------------------------------------");

        foreach (Doctor doctor in doctors)
        {
            Console.WriteLine(
                $"{doctor.DoctorId} | {doctor.FullName} | " +
                $"{doctor.Specialization} | {doctor.DepartmentId} | " +
                $"{doctor.ConsultationFee:C} | {doctor.IsActive}");
        }

        Pause();
    }

    private static void SearchDoctor()
    {
        int id = ReadInt("Enter doctor ID: ");
        Doctor? doctor = DoctorService.GetDoctorById(id);

        if (doctor == null)
        {
            Console.WriteLine("Doctor not found.");
        }
        else
        {
            Console.WriteLine($"ID: {doctor.DoctorId}");
            Console.WriteLine($"Name: {doctor.FullName}");
            Console.WriteLine($"Specialization: {doctor.Specialization}");
            Console.WriteLine($"Phone: {doctor.Phone}");
            Console.WriteLine($"Email: {doctor.Email ?? "-"}");
            Console.WriteLine($"Department ID: {doctor.DepartmentId}");
            Console.WriteLine($"Consultation Fee: {doctor.ConsultationFee:C}");
            Console.WriteLine($"Active: {doctor.IsActive}");
        }

        Pause();
    }

    private static void AddDoctor()
    {
        Console.Write("Enter doctor name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Enter specialization: ");
        string specialization = Console.ReadLine() ?? "";

        Console.Write("Enter phone: ");
        string phone = Console.ReadLine() ?? "";

        Console.Write("Enter email (optional): ");
        string? email = Console.ReadLine();

        int departmentId = ReadInt("Enter department ID: ");

        decimal fee;
        while (true)
        {
            Console.Write("Enter consultation fee: ");
            if (decimal.TryParse(Console.ReadLine(), out fee) && fee >= 0)
                break;

            Console.WriteLine("Enter a valid non-negative fee.");
        }

        Doctor doctor = new Doctor
        {
            FullName = name,
            Specialization = specialization,
            Phone = phone,
            Email = email,
            DepartmentId = departmentId,
            ConsultationFee = fee
        };

        int id = DoctorService.AddDoctor(doctor);
        Console.WriteLine($"Doctor added successfully. ID: {id}");

        Pause();
    }

    private static void UpdateDoctor()
    {
        int id = ReadInt("Enter doctor ID to update: ");

        Doctor? existing = DoctorService.GetDoctorById(id);

        if (existing == null)
        {
            Console.WriteLine("Doctor not found.");
            Pause();
            return;
        }

        Console.Write($"Name ({existing.FullName}): ");
        string? name = Console.ReadLine();

        Console.Write($"Specialization ({existing.Specialization}): ");
        string? specialization = Console.ReadLine();

        Console.Write($"Phone ({existing.Phone}): ");
        string? phone = Console.ReadLine();

        Console.Write($"Email ({existing.Email ?? "-"}): ");
        string? email = Console.ReadLine();

        Console.Write($"Department ID ({existing.DepartmentId}): ");
        string? departmentInput = Console.ReadLine();

        int departmentId = existing.DepartmentId;

        if (!string.IsNullOrWhiteSpace(departmentInput) &&
            !int.TryParse(departmentInput, out departmentId))
        {
            Console.WriteLine("Invalid department ID.");
            Pause();
            return;
        }

        Console.Write($"Consultation fee ({existing.ConsultationFee}): ");
        string? feeInput = Console.ReadLine();

        decimal fee = existing.ConsultationFee;

        if (!string.IsNullOrWhiteSpace(feeInput) &&
            (!decimal.TryParse(feeInput, out fee) || fee < 0))
        {
            Console.WriteLine("Invalid consultation fee.");
            Pause();
            return;
        }

        Doctor updated = new Doctor
        {
            DoctorId = id,
            FullName = string.IsNullOrWhiteSpace(name) ? existing.FullName : name,
            Specialization = string.IsNullOrWhiteSpace(specialization)
                ? existing.Specialization : specialization,
            Phone = string.IsNullOrWhiteSpace(phone) ? existing.Phone : phone,
            Email = string.IsNullOrWhiteSpace(email) ? existing.Email : email,
            DepartmentId = departmentId,
            ConsultationFee = fee,
            IsActive = existing.IsActive
        };

        bool success = DoctorService.UpdateDoctor(updated);

        Console.WriteLine(success
            ? "Doctor updated successfully."
            : "Doctor could not be updated.");

        Pause();
    }

    private static void DeactivateDoctor()
    {
        int id = ReadInt("Enter doctor ID to deactivate: ");

        Console.Write("Confirm deactivation? Enter Y: ");
        string? confirmation = Console.ReadLine();

        if (!string.Equals(confirmation, "Y",
            StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Deactivation cancelled.");
            Pause();
            return;
        }

        bool success = DoctorService.DeactivateDoctor(id);

        Console.WriteLine(success
            ? "Doctor deactivated successfully."
            : "Doctor not found or already inactive.");

        Pause();
    }

    private static void AppointmentMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== APPOINTMENT MANAGEMENT =====");
            Console.WriteLine("1. Display All Appointments");
            Console.WriteLine("2. Search Appointment by ID");
            Console.WriteLine("3. Schedule Appointment");
            Console.WriteLine("4. Update Appointment Status");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayAppointments();
                        break;
                    case "2":
                        SearchAppointment();
                        break;
                    case "3":
                        ScheduleAppointment();
                        break;
                    case "4":
                        UpdateAppointmentStatus();
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

    private static void DisplayAppointments()
    {
        List<Appointment> appointments =
            AppointmentService.GetAllAppointments();

        Console.WriteLine("\nID | Patient ID | Doctor ID | Date | Status");
        Console.WriteLine("--------------------------------------------");

        foreach (Appointment appointment in appointments)
        {
            Console.WriteLine(
                $"{appointment.AppointmentId} | " +
                $"{appointment.PatientId} | " +
                $"{appointment.DoctorId} | " +
                $"{appointment.AppointmentDate:g} | " +
                $"{appointment.Status}");
        }

        if (appointments.Count == 0)
            Console.WriteLine("No appointments found.");

        Pause();
    }

    private static void SearchAppointment()
    {
        int id = ReadInt("Enter appointment ID: ");

        Appointment? appointment =
            AppointmentService.GetAppointmentById(id);

        if (appointment == null)
        {
            Console.WriteLine("Appointment not found.");
        }
        else
        {
            Console.WriteLine($"Appointment ID: {appointment.AppointmentId}");
            Console.WriteLine($"Patient ID: {appointment.PatientId}");
            Console.WriteLine($"Doctor ID: {appointment.DoctorId}");
            Console.WriteLine($"Date and Time: {appointment.AppointmentDate:g}");
            Console.WriteLine($"Reason: {appointment.Reason ?? "-"}");
            Console.WriteLine($"Status: {appointment.Status}");
        }

        Pause();
    }

    private static void ScheduleAppointment()
    {
        int patientId = ReadInt("Enter patient ID: ");
        int doctorId = ReadInt("Enter doctor ID: ");

        Console.Write("Enter appointment date and time (e.g. 30-10-2026 10:30 AM): ");
        string? dateInput = Console.ReadLine();

        if (!DateTime.TryParse(dateInput, out DateTime appointmentDate))
        {
            Console.WriteLine("Invalid date or time.");
            Pause();
            return;
        }

        Console.Write("Enter appointment reason (optional): ");
        string? reason = Console.ReadLine();

        Appointment appointment = new Appointment
        {
            PatientId = patientId,
            DoctorId = doctorId,
            AppointmentDate = appointmentDate,
            Reason = string.IsNullOrWhiteSpace(reason) ? null : reason.Trim(),
            Status = "Scheduled"
        };

        int id = AppointmentService.ScheduleAppointment(appointment);

        Console.WriteLine(
            $"Appointment scheduled successfully. ID: {id}");

        Pause();
    }

    private static void UpdateAppointmentStatus()
    {
        int id = ReadInt("Enter appointment ID: ");

        Console.WriteLine("\nSelect new status:");
        Console.WriteLine("1. Completed");
        Console.WriteLine("2. Cancelled");
        Console.WriteLine("3. NoShow");
        Console.Write("Enter your choice: ");

        string? choice = Console.ReadLine();

        string? status = choice switch
        {
            "1" => "Completed",
            "2" => "Cancelled",
            "3" => "NoShow",
            _ => null
        };

        if (status == null)
        {
            Console.WriteLine("Invalid status choice.");
            Pause();
            return;
        }

        bool success =
            AppointmentService.UpdateAppointmentStatus(id, status);

        Console.WriteLine(success
            ? "Appointment status updated successfully."
            : "Appointment not found or status could not be updated.");

        Pause();
    }

    private static void MedicalRecordMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== MEDICAL RECORDS MANAGEMENT =====");
            Console.WriteLine("1. Display All Medical Records");
            Console.WriteLine("2. Search Medical Record by ID");
            Console.WriteLine("3. Add Medical Record");
            Console.WriteLine("4. Update Medical Record");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayMedicalRecords();
                        break;
                    case "2":
                        SearchMedicalRecord();
                        break;
                    case "3":
                        AddMedicalRecord();
                        break;
                    case "4":
                        UpdateMedicalRecord();
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
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayMedicalRecords()
    {
        List<MedicalRecord> records =
            MedicalRecordService.GetAllMedicalRecords();

        Console.WriteLine("\nID | Appointment ID | Diagnosis | Follow-up Date");
        Console.WriteLine("------------------------------------------------");

        foreach (MedicalRecord record in records)
        {
            Console.WriteLine(
                $"{record.MedicalRecordId} | " +
                $"{record.AppointmentId} | " +
                $"{record.Diagnosis} | " +
                $"{record.FollowUpDate?.ToShortDateString() ?? "-"}");
        }

        if (records.Count == 0)
            Console.WriteLine("No medical records found.");

        Pause();
    }

    private static void SearchMedicalRecord()
    {
        int id = ReadInt("Enter medical record ID: ");

        MedicalRecord? record =
            MedicalRecordService.GetMedicalRecordById(id);

        if (record == null)
        {
            Console.WriteLine("Medical record not found.");
        }
        else
        {
            Console.WriteLine($"Record ID: {record.MedicalRecordId}");
            Console.WriteLine($"Appointment ID: {record.AppointmentId}");
            Console.WriteLine($"Diagnosis: {record.Diagnosis}");
            Console.WriteLine($"Treatment: {record.TreatmentNotes ?? "-"}");
            Console.WriteLine(
                $"Follow-up: {record.FollowUpDate?.ToShortDateString() ?? "-"}");
        }

        Pause();
    }

    private static void AddMedicalRecord()
    {
        int appointmentId = ReadInt("Enter completed appointment ID: ");

        Console.Write("Enter diagnosis: ");
        string diagnosis = Console.ReadLine() ?? "";

        Console.Write("Enter treatment notes (optional): ");
        string? treatmentNotes = Console.ReadLine();

        Console.Write("Enter follow-up date (yyyy-MM-dd), or leave blank: ");
        string? followUpInput = Console.ReadLine();

        DateTime? followUpDate = null;

        if (!string.IsNullOrWhiteSpace(followUpInput))
        {
            if (!DateTime.TryParse(followUpInput, out DateTime parsedDate))
            {
                Console.WriteLine("Invalid follow-up date.");
                Pause();
                return;
            }

            followUpDate = parsedDate;
        }

        MedicalRecord record = new MedicalRecord
        {
            AppointmentId = appointmentId,
            Diagnosis = diagnosis,
            TreatmentNotes = string.IsNullOrWhiteSpace(treatmentNotes)
                ? null
                : treatmentNotes.Trim(),
            FollowUpDate = followUpDate
        };

        int id = MedicalRecordService.AddMedicalRecord(record);

        Console.WriteLine($"Medical record added successfully. ID: {id}");
        Pause();
    }

    private static void UpdateMedicalRecord()
    {
        int id = ReadInt("Enter medical record ID to update: ");

        MedicalRecord? existing =
            MedicalRecordService.GetMedicalRecordById(id);

        if (existing == null)
        {
            Console.WriteLine("Medical record not found.");
            Pause();
            return;
        }

        Console.Write($"Diagnosis ({existing.Diagnosis}): ");
        string? diagnosis = Console.ReadLine();

        Console.Write($"Treatment notes ({existing.TreatmentNotes ?? "-"}): ");
        string? notes = Console.ReadLine();

        Console.Write(
            $"Follow-up date ({existing.FollowUpDate?.ToString("yyyy-MM-dd") ?? "-"}): ");
        string? followUpInput = Console.ReadLine();

        DateTime? followUpDate = existing.FollowUpDate;

        if (!string.IsNullOrWhiteSpace(followUpInput))
        {
            if (!DateTime.TryParse(followUpInput, out DateTime parsedDate))
            {
                Console.WriteLine("Invalid follow-up date.");
                Pause();
                return;
            }

            followUpDate = parsedDate;
        }

        MedicalRecord updated = new MedicalRecord
        {
            MedicalRecordId = id,
            AppointmentId = existing.AppointmentId,
            Diagnosis = string.IsNullOrWhiteSpace(diagnosis)
                ? existing.Diagnosis
                : diagnosis.Trim(),
            TreatmentNotes = string.IsNullOrWhiteSpace(notes)
                ? existing.TreatmentNotes
                : notes.Trim(),
            FollowUpDate = followUpDate,
            CreatedAt = existing.CreatedAt
        };

        bool success = MedicalRecordService.UpdateMedicalRecord(updated);

        Console.WriteLine(success
            ? "Medical record updated successfully."
            : "Medical record could not be updated.");

        Pause();
    }

    private static void BillingMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== BILLING MANAGEMENT =====");
            Console.WriteLine("1. Display All Bills");
            Console.WriteLine("2. Search Bill by ID");
            Console.WriteLine("3. Create Bill");
            Console.WriteLine("4. Cancel Bill");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayBills();
                        break;
                    case "2":
                        SearchBill();
                        break;
                    case "3":
                        CreateBill();
                        break;
                    case "4":
                        CancelBill();
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
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayBills()
    {
        List<Bill> bills = BillingService.GetAllBills();

        Console.WriteLine("\nBill ID | Appointment ID | Amount | Date | Status");
        Console.WriteLine("--------------------------------------------------");

        foreach (Bill bill in bills)
        {
            Console.WriteLine(
                $"{bill.BillId} | {bill.AppointmentId} | " +
                $"{bill.TotalAmount:C} | {bill.BillDate:d} | {bill.Status}");
        }

        if (bills.Count == 0)
            Console.WriteLine("No bills found.");

        Pause();
    }

    private static void SearchBill()
    {
        int id = ReadInt("Enter bill ID: ");

        Bill? bill = BillingService.GetBillById(id);

        if (bill == null)
        {
            Console.WriteLine("Bill not found.");
        }
        else
        {
            Console.WriteLine($"Bill ID: {bill.BillId}");
            Console.WriteLine($"Appointment ID: {bill.AppointmentId}");
            Console.WriteLine($"Total amount: {bill.TotalAmount:C}");
            Console.WriteLine($"Bill date: {bill.BillDate:d}");
            Console.WriteLine($"Status: {bill.Status}");
        }

        Pause();
    }

    private static void CreateBill()
    {
        int appointmentId = ReadInt("Enter completed appointment ID: ");

        decimal amount;

        while (true)
        {
            Console.Write("Enter total amount: ");

            if (decimal.TryParse(Console.ReadLine(), out amount) && amount >= 0)
                break;

            Console.WriteLine("Enter a valid non-negative amount.");
        }

        int id = BillingService.CreateBill(appointmentId, amount);

        Console.WriteLine($"Bill created successfully. Bill ID: {id}");
        Pause();
    }

    private static void CancelBill()
    {
        int id = ReadInt("Enter bill ID to cancel: ");

        Console.Write("Confirm cancellation? Enter Y: ");
        string? confirmation = Console.ReadLine();

        if (!string.Equals(
            confirmation, "Y", StringComparison.OrdinalIgnoreCase))
        {
            Console.WriteLine("Cancellation aborted.");
            Pause();
            return;
        }

        bool success = BillingService.CancelBill(id);

        Console.WriteLine(success
            ? "Bill cancelled successfully."
            : "Bill could not be cancelled. Check that it exists and is unpaid.");

        Pause();
    }

    private static void PaymentMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== PAYMENT MANAGEMENT =====");
            Console.WriteLine("1. Display All Payments");
            Console.WriteLine("2. Search Payment by ID");
            Console.WriteLine("3. Display Payments by Bill ID");
            Console.WriteLine("4. Record Payment");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayPayments();
                        break;
                    case "2":
                        SearchPayment();
                        break;
                    case "3":
                        DisplayPaymentsByBill();
                        break;
                    case "4":
                        RecordPayment();
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
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayPayments()
    {
        List<Payment> payments = PaymentService.GetAllPayments();

        Console.WriteLine("\nPayment ID | Bill ID | Amount | Method | Date");
        Console.WriteLine("------------------------------------------------");

        foreach (Payment payment in payments)
        {
            Console.WriteLine(
                $"{payment.PaymentId} | {payment.BillId} | " +
                $"{payment.Amount:C} | {payment.PaymentMethod} | " +
                $"{payment.PaymentDate:g}");
        }

        if (payments.Count == 0)
            Console.WriteLine("No payments found.");

        Pause();
    }

    private static void SearchPayment()
    {
        int id = ReadInt("Enter payment ID: ");

        Payment? payment = PaymentService.GetPaymentById(id);

        if (payment == null)
        {
            Console.WriteLine("Payment not found.");
        }
        else
        {
            Console.WriteLine($"Payment ID: {payment.PaymentId}");
            Console.WriteLine($"Bill ID: {payment.BillId}");
            Console.WriteLine($"Amount: {payment.Amount:C}");
            Console.WriteLine($"Method: {payment.PaymentMethod}");
            Console.WriteLine($"Payment Date: {payment.PaymentDate:g}");
            Console.WriteLine(
                $"Reference Number: {payment.ReferenceNumber ?? "-"}");
        }

        Pause();
    }

    private static void DisplayPaymentsByBill()
    {
        int billId = ReadInt("Enter bill ID: ");

        List<Payment> payments =
            PaymentService.GetPaymentsByBillId(billId);

        Console.WriteLine($"\nPayments for Bill ID {billId}");

        foreach (Payment payment in payments)
        {
            Console.WriteLine(
                $"Payment ID: {payment.PaymentId} | " +
                $"Amount: {payment.Amount:C} | " +
                $"Method: {payment.PaymentMethod}");
        }

        if (payments.Count == 0)
            Console.WriteLine("No payments found for this bill.");

        Pause();
    }

    private static void RecordPayment()
    {
        int billId = ReadInt("Enter bill ID: ");

        decimal amount;

        while (true)
        {
            Console.Write("Enter payment amount: ");

            if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                break;

            Console.WriteLine("Enter an amount greater than zero.");
        }

        Console.WriteLine("\nPayment Method");
        Console.WriteLine("1. Cash");
        Console.WriteLine("2. Card");
        Console.WriteLine("3. UPI");
        Console.WriteLine("4. Bank Transfer");
        Console.Write("Choose method: ");

        string? method = Console.ReadLine() switch
        {
            "1" => "Cash",
            "2" => "Card",
            "3" => "UPI",
            "4" => "BankTransfer",
            _ => null
        };

        if (method == null)
        {
            Console.WriteLine("Invalid payment method.");
            Pause();
            return;
        }

        Console.Write("Enter reference number (optional): ");
        string? reference = Console.ReadLine();

        int paymentId = PaymentService.RecordPayment(
            billId,
            amount,
            method,
            string.IsNullOrWhiteSpace(reference) ? null : reference.Trim());

        Console.WriteLine(
            $"Payment recorded successfully. Payment ID: {paymentId}");

        Pause();
    }

    private static void AuditLogMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== AUDIT LOG MANAGEMENT =====");
            Console.WriteLine("1. Display All Logs");
            Console.WriteLine("2. Search Logs by Entity");
            Console.WriteLine("3. Add Audit Log");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        DisplayAuditLogs();
                        break;
                    case "2":
                        SearchAuditLogs();
                        break;
                    case "3":
                        AddAuditLog();
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
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayAuditLogs()
    {
        List<AuditLog> logs = AuditLogService.GetAllLogs();

        Console.WriteLine("\nAudit ID | Entity | Entity ID | Action | Date");
        Console.WriteLine("------------------------------------------------");

        foreach (AuditLog log in logs)
        {
            Console.WriteLine(
                $"{log.AuditId} | {log.EntityName} | " +
                $"{log.EntityId} | {log.Action} | {log.CreatedAt:g}");
        }

        if (logs.Count == 0)
            Console.WriteLine("No audit logs found.");

        Pause();
    }

    private static void SearchAuditLogs()
    {
        Console.Write("Enter entity name (e.g. Patient): ");
        string entityName = Console.ReadLine() ?? "";

        int entityId = ReadInt("Enter entity ID: ");

        List<AuditLog> logs =
            AuditLogService.GetLogsByEntity(entityName, entityId);

        foreach (AuditLog log in logs)
        {
            Console.WriteLine(
                $"Audit ID: {log.AuditId} | " +
                $"Action: {log.Action} | " +
                $"Details: {log.Details ?? "-"} | " +
                $"Date: {log.CreatedAt:g}");
        }

        if (logs.Count == 0)
            Console.WriteLine("No matching logs found.");

        Pause();
    }

    private static void AddAuditLog()
    {
        Console.Write("Enter entity name: ");
        string entityName = Console.ReadLine() ?? "";

        int entityId = ReadInt("Enter entity ID: ");

        Console.Write("Enter action (Created/Updated/Deactivated): ");
        string action = Console.ReadLine() ?? "";

        Console.Write("Enter details (optional): ");
        string? details = Console.ReadLine();

        AuditLogService.LogAction(
            entityName,
            entityId,
            action,
            string.IsNullOrWhiteSpace(details) ? null : details.Trim());

        Console.WriteLine("Audit log recorded successfully.");
        Pause();
    }

    private static void ReportsMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== HOSPITAL REPORTS =====");
            Console.WriteLine("1. Total Patients");
            Console.WriteLine("2. Total Doctors");
            Console.WriteLine("3. Total Appointments");
            Console.WriteLine("4. Scheduled Appointments");
            Console.WriteLine("5. Completed Appointments");
            Console.WriteLine("6. Total Revenue");
            Console.WriteLine("7. Outstanding Balance");
            Console.WriteLine("8. Display All Reports");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("\nEnter your choice: ");

            string? choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Console.WriteLine(
                            $"Total Patients: {ReportService.GetTotalPatients()}");
                        Pause();
                        break;

                    case "2":
                        Console.WriteLine(
                            $"Total Doctors: {ReportService.GetTotalDoctors()}");
                        Pause();
                        break;

                    case "3":
                        Console.WriteLine(
                            $"Total Appointments: {ReportService.GetTotalAppointments()}");
                        Pause();
                        break;

                    case "4":
                        Console.WriteLine(
                            $"Scheduled Appointments: {ReportService.GetScheduledAppointments()}");
                        Pause();
                        break;

                    case "5":
                        Console.WriteLine(
                            $"Completed Appointments: {ReportService.GetCompletedAppointments()}");
                        Pause();
                        break;

                    case "6":
                        Console.WriteLine(
                            $"Total Revenue: {ReportService.GetTotalRevenue():C}");
                        Pause();
                        break;

                    case "7":
                        Console.WriteLine(
                            $"Outstanding Balance: {ReportService.GetOutstandingBalance():C}");
                        Pause();
                        break;

                    case "8":
                        DisplayAllReports();
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
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void DisplayAllReports()
    {
        Console.WriteLine("\n===== HOSPITAL SUMMARY REPORT =====");
        Console.WriteLine("----------------------------------");

        Console.WriteLine(
            $"Total Patients: {ReportService.GetTotalPatients()}");

        Console.WriteLine(
            $"Total Doctors: {ReportService.GetTotalDoctors()}");

        Console.WriteLine(
            $"Total Appointments: {ReportService.GetTotalAppointments()}");

        Console.WriteLine(
            $"Scheduled Appointments: {ReportService.GetScheduledAppointments()}");

        Console.WriteLine(
            $"Completed Appointments: {ReportService.GetCompletedAppointments()}");

        Console.WriteLine(
            $"Total Revenue: {ReportService.GetTotalRevenue():C}");

        Console.WriteLine(
            $"Outstanding Balance: {ReportService.GetOutstandingBalance():C}");

        Pause();
    }



    //CREATE DATABASE HospitalDB;
    //GO
}
