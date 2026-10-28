
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class BillingService
{
    private readonly BillingRepository _billingRepository;
    private readonly AppointmentRepository _appointmentRepository;

    public BillingService()
    {
        _billingRepository = new BillingRepository();
        _appointmentRepository = new AppointmentRepository();
    }

    public List<Bill> GetAllBills()
    {
        return _billingRepository.GetAll();
    }

    public Bill? GetBillById(int billId)
    {
        if (billId <= 0)
            throw new ArgumentException(
                "Bill ID must be greater than zero.");

        return _billingRepository.GetById(billId);
    }

    public int CreateBill(int appointmentId, decimal totalAmount)
    {
        if (appointmentId <= 0)
            throw new ArgumentException("Select a valid appointment.");

        if (totalAmount < 0)
            throw new ArgumentException(
                "Bill amount cannot be negative.");

        Appointment? appointment =
            _appointmentRepository.GetById(appointmentId);

        if (appointment == null)
            throw new ArgumentException("Appointment does not exist.");

        if (appointment.Status != "Completed")
            throw new InvalidOperationException(
                "A bill can only be created for a completed appointment.");

        Bill bill = new Bill
        {
            AppointmentId = appointmentId,
            TotalAmount = totalAmount,
            Status = "Unpaid"
        };

        return _billingRepository.Add(bill);
    }

    public bool CancelBill(int billId)
    {
        if (billId <= 0)
            throw new ArgumentException(
                "Bill ID must be greater than zero.");

        Bill? bill = _billingRepository.GetById(billId);

        if (bill == null)
            return false;

        if (bill.Status != "Unpaid")
            throw new InvalidOperationException(
                "Only unpaid bills can be cancelled.");

        return _billingRepository.UpdateStatus(billId, "Cancelled");
    }
}
