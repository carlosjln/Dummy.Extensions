using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle( "Dummy.Extensions" )]
[assembly: AssemblyProduct("Dummy.Extensions")]
[assembly: AssemblyDescription( "Extensions for fun coding :)" )]

[assembly: AssemblyCompany("Carlos J. López")]
[assembly: AssemblyCopyright("Copyright © 2013 Carlos J. López")]
[assembly: AssemblyTrademark("")]

[assembly: AssemblyVersion( "1.0.0.*" )]
// [assembly: AssemblyFileVersion( "1.0.0" )]
[assembly: AssemblyInformationalVersion("1.0.2")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

[assembly: ComVisible( false )]
[assembly: AssemblyCulture( "" )]
[assembly: Guid( "c4abbc87-be9e-4c68-b175-5431abbbce5e" )]