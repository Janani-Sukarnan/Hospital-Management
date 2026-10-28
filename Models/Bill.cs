
namespace HospitalManagementSystem.Models;

public class Bill
{
    public int BillId { get; set; }

    public int AppointmentId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime BillDate { get; set; }

    public string Status { get; set; } = "Unpaid";
}

