namespace YCCodeChallenge
{
    internal class SuperDisbursementPrinter
    {
        public void PrintSuperData(Dictionary<int, EmployeeQuarterlyGroupings> superData)
        {
            foreach (var employeeQuarterlyGrouping in superData.Values)
            {
                Console.WriteLine($"For employee {employeeQuarterlyGrouping.EmployeeId}");
                foreach(var grouping in employeeQuarterlyGrouping.QuarterlyGroupings.OrderBy(g => g.Key))
                {
                    Console.WriteLine($"For Q{(grouping.Key.Month / 3) + 1} {grouping.Key.Year}");
                    Console.WriteLine($"  Total OTE           ${grouping.Value.OTEAmount}");
                    Console.WriteLine($"  Total Super Payable ${grouping.Value.SuperPayable}");
                    Console.WriteLine($"  Total Disbursed     ${grouping.Value.SuperGuaranteeChargePaid}");
                    Console.WriteLine($"  Variance            ${grouping.Value.Variance}");
                }
                Console.WriteLine();
            }
        }
    }
}
