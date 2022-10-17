namespace Common
{
    public static class ExcepcionsMenssages
    {
        public static readonly string MoneyNotNull = "Money can not be null";
        public static readonly string DateNotNull = "Date can not be null";
        public static readonly string MoneyNotLessThan0 = "Money can not be less or equat to 0";
        public static readonly string DatePatron = "Start and End date can not be the same date, more of 60 day between them or bigger that today";
        public static readonly string ProviderError = "Error from the rate provider";
    }
}
