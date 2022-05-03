using ExcelDataReader;

namespace YCCodeChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
         
            SuperDataReader superDataReader = new SuperDataReader();
            using (var fileStream = File.Open(@"Sample Super Data.xlsx", FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(fileStream))
                {
                   superDataReader.ReadSuperData(reader);
                }
            }

            SuperDataGrouper formatter = new SuperDataGrouper();
            Dictionary<int, EmployeeQuarterlyGroupings> employeeGroupings 
               = formatter.GroupSuperData(superDataReader.Disbursements, superDataReader.PaySlips, superDataReader.PayCodes);

            SuperDisbursementPrinter printer = new SuperDisbursementPrinter();
            printer.PrintSuperData(employeeGroupings);
        }
    }
}