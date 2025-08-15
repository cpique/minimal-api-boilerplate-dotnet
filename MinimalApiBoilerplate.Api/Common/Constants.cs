namespace MinimalApiBoilerplate.Api.Common;

public static class Constants
{
    public static class TickerQ
    {
        public const string DashboardBasePath = "/tickerq-dashboard";
        public const int MaxConcurrency = 4;
    }

    public static class Auth
    {
        public const string ApiKeyHeaderName = "X-Api-Key";
    }
}
