
using HospitalManagementSystem.Models;
using HospitalManagementSystem.Repositories;

namespace HospitalManagementSystem.Services;

public class PaymentService
{
    private readonly PaymentRepository _paymentRepository;
    private readonly BillingRepository _billingRepository;

    public PaymentService()
    {
        _paymentRepository = new PaymentRepository();
        _billingRepository = new BillingRepository();
    }

    public List<Payment> GetAllPayments()
    {
        return _paymentRepository.GetAll();
    }

    public Payment? GetPaymentById(int paymentId)
    {
        if (paymentId <= 0)
            throw new ArgumentException(
                "Payment ID must be greater than zero.");

        return _paymentRepository.GetById(paymentId);
    }

    public List<Payment> GetPaymentsByBillId(int billId)
    {
        if (billId <= 0)
            throw new ArgumentException(
                "Bill ID must be greater than zero.");

        if (_billingRepository.GetById(billId) == null)
            throw new ArgumentException("Bill does not exist.");

        return _paymentRepository.GetByBillId(billId);
    }

    public int RecordPayment(
        int billId,
        decimal amount,
        string paymentMethod,
        string? referenceNumber = null)
    {
        if (billId <= 0)
            throw new ArgumentException("Select a valid bill.");

        if (amount <= 0)
            throw new ArgumentException(
                "Payment amount must be greater than zero.");

        string[] allowedMethods =
        {
            "Cash", "Card", "UPI", "BankTransfer"
        };

        string? validMethod = allowedMethods.FirstOrDefault(
            method => method.Equals(
                paymentMethod?.Trim(),
                StringComparison.OrdinalIgnoreCase));

        if (validMethod == null)
            throw new ArgumentException(
                "Payment method must be Cash, Card, UPI, or BankTransfer.");

        Bill? bill = _billingRepository.GetById(billId);

        if (bill == null)
            throw new ArgumentException("Bill does not exist.");

        if (bill.Status == "Paid" || bill.Status == "Cancelled")
            throw new InvalidOperationException(
                "Payment cannot be recorded for a paid or cancelled bill.");

        List<Payment> existingPayments =
            _paymentRepository.GetByBillId(billId);

        decimal alreadyPaid = existingPayments.Sum(p => p.Amount);
        decimal remainingAmount = bill.TotalAmount - alreadyPaid;

        if (amount > remainingAmount)
            throw new ArgumentException(
                $"Payment exceeds the remaining balance of {remainingAmount:C}.");

        Payment payment = new Payment
        {
            BillId = billId,
            Amount = amount,
            PaymentMethod = validMethod,
            ReferenceNumber = string.IsNullOrWhiteSpace(referenceNumber)
                ? null
                : referenceNumber.Trim()
        };

        decimal totalPaid = alreadyPaid + amount;

        string newStatus = totalPaid >= bill.TotalAmount
            ? "Paid"
            : "PartiallyPaid";

        int paymentId = _paymentRepository.AddAndUpdateBillStatus(
            payment,
            newStatus);

        return paymentId;


        //_billingRepository.UpdateStatus(billId, newStatus);

        return paymentId;
    }
}

