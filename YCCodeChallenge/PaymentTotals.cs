namespace YCCodeChallenge
{
    public class PaymentTotals
    {
        public decimal OTEAmount { get; set; }
        public decimal NotOTEAmount { get; set; }
        public decimal SuperGuaranteeChargePaid { get; set; }
        public decimal SuperPayable => Math.Round(OTEAmount * 0.095m, 2);
        public decimal Variance => SuperPayable - SuperGuaranteeChargePaid;
    }
}
