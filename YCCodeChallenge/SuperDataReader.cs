using System.Data;
using YCCodeChallenge.SuperData;

namespace YCCodeChallenge
{
    public class SuperDataReader
    {
        public List<Disbursement> Disbursements { get; private set; } = new();
        public List<PaySlip> PaySlips { get; private set; } = new();
        public List<PayCode> PayCodes { get; private set; } = new();

        public void ReadSuperData(IDataReader reader)
        {
            Disbursements = ReadDisbursements(reader);

            reader.NextResult();
            PaySlips = ReadPaySlips(reader);

            reader.NextResult();
            PayCodes = ReadPayCodes(reader);
        }

        private List<Disbursement> ReadDisbursements(IDataReader reader)
        {
            List<Disbursement> disbursements = new();
            reader.Read();
            while (reader.Read())
            {
                double amount = reader.GetDouble(0);
                string paymentMade = reader.GetString(1);
                string payPeriodFrom = reader.GetString(2);
                string payPeriodTo = reader.GetString(3);
                double employeeCode = reader.GetDouble(4);

                disbursements.Add(CreateDisbursement(amount, paymentMade, payPeriodFrom, payPeriodTo, employeeCode));
            }

            return disbursements;
        }

        private Disbursement CreateDisbursement(double amount, string paymentMade, string payPeriodFrom, string payPeriodTo, double employeeCode)
        {
            return new Disbursement
            {
                SuperGuaranteeCharge = (decimal)amount,
                PaymentMade = DateTime.Parse(paymentMade),
                PayPeriodFrom = DateTime.Parse(payPeriodFrom),
                PayPeriodTo = DateTime.Parse(payPeriodTo),
                EmployeeCode = (int)employeeCode
            };
        }

        private List<PaySlip> ReadPaySlips(IDataReader reader)
        {
            List<PaySlip> paySlips = new();
            reader.Read();
            while (reader.Read())
            {
                string payslipId = reader.GetString(0);
                DateTime end = reader.GetDateTime(1);
                double employeeCode = reader.GetDouble(2);
                string payCode = reader.GetString(3);
                double amount = reader.GetDouble(4);

                paySlips.Add(CreatePaySlip(payslipId, end, employeeCode, payCode, amount));
            }

            return paySlips;
        }

        private PaySlip CreatePaySlip(string payslipId, DateTime end, double employeeCode, string payCode, double amount)
        {
            return new PaySlip
            {
                PayslipId = Guid.Parse(payslipId),
                End = end,
                EmployeeId = (int)employeeCode,
                PayCode = payCode,
                Amount = (decimal)amount
            };
        }

        private List<PayCode> ReadPayCodes(IDataReader reader)
        {
            List<PayCode> payCodes = new();
            reader.Read();
            while (reader.Read())
            {
                string code = reader.GetString(0);
                string oteTreatment = reader.GetString(1);

                payCodes.Add(CreatePayCode(code, oteTreatment));
            }

            return payCodes;
        }

        private PayCode CreatePayCode(string code, string oteTreatment)
        {
            return new PayCode
            {
                Code = code,
                OteTreatment = oteTreatment == "OTE" ? OTETreatment.OTE : OTETreatment.NotOTE
            };
        }
    }
}
