namespace DepsWebApp.Options
{
#pragma warning disable 1591
    public class RatesOptions

    {
        public string BaseCurrency { get; set; }
        public bool IsValid => !string.IsNullOrWhiteSpace(BaseCurrency);
    }
#pragma warning restore 1591
}
