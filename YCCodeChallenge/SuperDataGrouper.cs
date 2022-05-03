using YCCodeChallenge.SuperData;

namespace YCCodeChallenge
{
    internal class SuperDataGrouper
    {
        public Dictionary<int, EmployeeQuarterlyGroupings> GroupSuperData(IEnumerable<Disbursement> disbursements, IEnumerable<PaySlip> paySlips, IEnumerable<PayCode> payCodes)
        {
            Dictionary<int, EmployeeQuarterlyGroupings> employeeQuarterlyGroupings = new();

            Dictionary<string, OTETreatment> payCodeOTETreatments = GroupPayCodes(payCodes);

            GroupQuarterlyPaySlips(paySlips, payCodeOTETreatments, employeeQuarterlyGroupings);

            GroupQuarterlyDisbursements(disbursements, employeeQuarterlyGroupings);

            return employeeQuarterlyGroupings;
        }

        private Dictionary<string, OTETreatment> GroupPayCodes(IEnumerable<PayCode> payCodes)
        {
            return payCodes.ToDictionary(p => p.Code, q => q.OteTreatment);
        }

        private void GroupQuarterlyPaySlips(IEnumerable<PaySlip> paySlips, Dictionary<string, OTETreatment> payCodeOTETreatments, 
                                            Dictionary<int, EmployeeQuarterlyGroupings> employeeQuarterlyGroupings)
        {
            foreach (var paySlipGrouping in paySlips.GroupBy(p => p.EmployeeId))
            {
                int employeeId = paySlipGrouping.Key;

                EmployeeQuarterlyGroupings quarterlyGroupings;
                if (employeeQuarterlyGroupings.ContainsKey(employeeId))
                {
                    quarterlyGroupings = employeeQuarterlyGroupings[employeeId];
                }
                else
                {
                    quarterlyGroupings = new(employeeId);
                    employeeQuarterlyGroupings.Add(employeeId, quarterlyGroupings);
                }

                foreach (var paySlip in paySlipGrouping)
                {
                    if (payCodeOTETreatments[paySlip.PayCode] == OTETreatment.OTE)
                    {
                        quarterlyGroupings.AddOTEAmount(paySlip.End, paySlip.Amount);
                    }
                    else
                    {
                        quarterlyGroupings.AddNonOTEAmount(paySlip.End, paySlip.Amount);
                    }
                }
            }
        }

        private void GroupQuarterlyDisbursements(IEnumerable<Disbursement> disbursements, Dictionary<int, EmployeeQuarterlyGroupings> employeeQuarterlyGroupings)
        {
            foreach (var disbursement in disbursements)
            {
                if (!employeeQuarterlyGroupings.ContainsKey(disbursement.EmployeeCode))
                {
                    continue;
                }

                employeeQuarterlyGroupings[disbursement.EmployeeCode].AddSuperGuaranteeChargeAmount(disbursement.PaymentMade, disbursement.SuperGuaranteeCharge);
            }
        }
    }
}
