namespace MinimalApiBoilerplate.Api.Authentication;

[AttributeUsage(AttributeTargets.Method)]
public class SkipApiKeyCheckAttribute : Attribute { }