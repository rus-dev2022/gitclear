using GitVersion.Configuration;
using GitVersion.Core.Tests.Helpers;
using GitVersion.Model.Configuration;
using NUnit.Framework;
using Shouldly;
using YamlDotNet.Core;

namespace GitVersion.Core.Tests.Configuration;

[TestFixture]
public class IgnoreConfigTests : TestBase
{
    [Test]
    public void CanDeserialize()
    {
        var yaml = @"
ignore:
    sha: [b6c0c9fda88830ebcd563e500a5a7da5a1658e98]
    commits-before: 2015-10-23T12:23:15
";

        using var reader = new StringReader(yaml);
        var config = ConfigSerializer.Read(reader);

        config.Ignore.ShouldNotBeNull();
        config.Ignore.ShAs.ShouldNotBeEmpty();
        config.Ignore.ShAs.ShouldBe(new[] { "b6c0c9fda88830ebcd563e500a5a7da5a1658e98" });
        config.Ignore.Before.ShouldBe(DateTimeOffset.Parse("2015-10-23T12:23:15"));
    }

    [Test]
    public void ShouldSupportsOtherSequenceFormat()
    {
        var yaml = @"
ignore:
    sha:
        - b6c0c9fda88830ebcd563e500a5a7da5a1658e98
        - 6c19c7c219ecf8dbc468042baefa73a1b213e8b1
";

        using var reader = new StringReader(yaml);
        var config = ConfigSerializer.Read(reader);

        config.Ignore.ShouldNotBeNull();
        config.Ignore.ShAs.ShouldNotBeEmpty();
        config.Ignore.ShAs.ShouldBe(new[] { "b6c0c9fda88830ebcd563e500a5a7da5a1658e98", "6c19c7c219ecf8dbc468042baefa73a1b213e8b1" });
    }

    [Test]
    public void WhenNotInConfigShouldHaveDefaults()
    {
        var yaml = @"
next-version: 1.0
";

        using var reader = new StringReader(yaml);
        var config = ConfigSerializer.Read(reader);

        config.Ignore.ShouldNotBeNull();
        config.Ignore.ShAs.ShouldBeEmpty();
        config.Ignore.Before.ShouldBe(null);
    }

    [Test]
    public void WhenBadDateFormatShouldFail()
    {
        var yaml = @"
ignore:
    commits-before: bad format date
";

        using var reader = new StringReader(yaml);
        Should.Throw<YamlException>(() => ConfigSerializer.Read(reader));
    }

    [Test]
    public void NewInstanceShouldBeEmpty()
    {
        var ignoreConfig = new IgnoreConfig();

        ignoreConfig.IsEmpty.ShouldBeTrue();
    }
}
