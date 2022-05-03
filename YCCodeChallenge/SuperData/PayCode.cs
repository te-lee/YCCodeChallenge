namespace YCCodeChallenge.SuperData
{
    public record PayCode
    {
        public string Code { get; init; } = string.Empty;
        public OTETreatment OteTreatment { get; init; }
    }
}
