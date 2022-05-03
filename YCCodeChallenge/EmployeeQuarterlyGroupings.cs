namespace YCCodeChallenge
{
    public class EmployeeQuarterlyGroupings
    {
        private readonly Dictionary<DateTime, PaymentTotals> quarterlyGroupings;

        public EmployeeQuarterlyGroupings(int employeeId)
        {
            EmployeeId = employeeId;
            quarterlyGroupings = new();
        }

        public int EmployeeId { get; }
        public IReadOnlyDictionary<DateTime, PaymentTotals> QuarterlyGroupings => quarterlyGroupings;

        public void AddOTEAmount(DateTime periodEnd, decimal oteAmount)
        {
            PaymentTotalByPeriodEnd(periodEnd).OTEAmount += oteAmount;
        }

        public void AddNonOTEAmount(DateTime periodEnd, decimal nonOTEAmount)
        {
            PaymentTotalByPeriodEnd(periodEnd).NotOTEAmount += nonOTEAmount;
        }

        public void AddSuperGuaranteeChargeAmount(DateTime paymentMade, decimal superGuaranteeChargeAmount)
        {
            DateTime disbursementQuarter = paymentMade.AddDays(-28);
            PaymentTotalByPeriodEnd(disbursementQuarter).SuperGuaranteeChargePaid += superGuaranteeChargeAmount;
        }

        private PaymentTotals PaymentTotalByPeriodEnd(DateTime periodEnd)
        {
            DateTime quarterStart = StartOfQuarter(periodEnd);

            if (!quarterlyGroupings.ContainsKey(quarterStart))
            {
                quarterlyGroupings.Add(quarterStart, new PaymentTotals());
            }

            return quarterlyGroupings[quarterStart];
        }

        private DateTime StartOfQuarter(DateTime date)
        {
            int startMonth = ((date.Month - 1) / 3) * 3 + 1;

            return new DateTime(date.Year, startMonth, 1);
        }
    }
}
