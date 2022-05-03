namespace YCCodeChallenge.SuperData
{
    public record Disbursement
    {
        public decimal SuperGuaranteeCharge { get; init; }
        public DateTime PaymentMade { get; init; }
        public DateTime PayPeriodFrom { get; init; }
        public DateTime PayPeriodTo { get; init; }
        public int EmployeeCode { get; init; }
    }
}
