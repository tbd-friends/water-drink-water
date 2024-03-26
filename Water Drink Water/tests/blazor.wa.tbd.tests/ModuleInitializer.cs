using System.Runtime.CompilerServices;

namespace blazor.wa.tbd.tests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize() => VerifyBunit.Initialize();
}