# YCCodeChallenge

**To Run Application**

The solution in Visual Studio will copy Sample Super Data.xlxs to the output directory. Sample Super Data.xlxs will be read, and the analysis printed to the console, grouping by Employee ID and quarter, such as:

```
For employee 1155
For Q3 2017
  Total OTE           $24396.73
  Total Super Payable $2317.69
  Total Disbursed     $2638.08
  Variance            $-320.39
```

**Assumptions made**
- Each quarter present in the super data should be analysed
- The date a disbursement was paid is the basis for determining which quarter it should be analysed as
- Payments made more than 28 days after the end of a quarter are not to be analysed as the variance for that quarter
- Payments made earlier than 28 days after the start of a quarter are to be analysed as the previous quarter
- Super payable should be rounded to the nearest cent
- Overpayments of super should be negative values
