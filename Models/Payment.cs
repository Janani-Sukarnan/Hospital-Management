

namespace HospitalManagementSystem.Models;

public class Payment
{
    public int PaymentId { get; set; }

    public int BillId { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = string.Empty;

    public DateTime PaymentDate { get; set; }

    public string? ReferenceNumber { get; set; }
}

