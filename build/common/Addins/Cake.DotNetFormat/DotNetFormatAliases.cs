namespace Common.Addins.Cake.DotNetFormat;

/// <summary>
/// <para>Contains aliases related to <see href="https://github.com/dotnet/format">dotnet format</see>.</para>
/// <para>
/// In order to use the commands for this addin, you will need to include the following in your build.cake file to download and
/// reference from NuGet.org:
/// <code>
/// #addin Cake.DotNetFormat
/// </code>
/// </para>
/// </summary>
[CakeAliasCategory("DotNetFormat")]
public static class DotNetFormatAliases
{
    /// <summary>
    /// Formats the code using the given settings.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="settings">The settings.</param>
    [CakeMethodAlias]
    public static void DotNetFormat(this ICakeContext context, DotNetFormatSettings settings)
    {
        if (context is null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var runner = new DotNetFormatToolRunner(context.FileSystem, context.Environment, context.ProcessRunner, context.Tools);
        runner.Run(settings);
    }
}
