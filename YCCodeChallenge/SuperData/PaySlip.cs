namespace YCCodeChallenge.SuperData
{
    public record PaySlip
    {
        public Guid PayslipId { get; init; }
        public DateTime End { get; init; }
        public int EmployeeId { get; init; }
        public string PayCode { get; init; } = string.Empty;
        public decimal Amount { get; init; }
    }
}
