using System;

namespace DepsWebApp.Options
{
#pragma warning disable 1591
    public class NbuClientOptions

    {
        public string BaseAddress { get; set; }

        public bool IsValid => !string.IsNullOrWhiteSpace(BaseAddress) &&
                               Uri.TryCreate(BaseAddress, UriKind.Absolute, out _);
    }
#pragma warning restore 1591
}
