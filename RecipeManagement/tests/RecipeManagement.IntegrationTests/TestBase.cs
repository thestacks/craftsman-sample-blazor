namespace RecipeManagement.IntegrationTests;

using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using AutoBogus;
using FluentAssertions.Extensions;
using static TestFixture;

public class TestBase
{
    [SetUp]
    public Task TestSetUp()
    {

        // close to equivalency required to reconcile precision differences between EF and Postgres
        AssertionOptions.AssertEquivalencyUsing(options => 
        {
            options.Using<DateTime>(ctx => ctx.Subject
                .Should()
                .BeCloseTo(ctx.Expectation, 1.Seconds())).WhenTypeIs<DateTime>();
            options.Using<DateTimeOffset>(ctx => ctx.Subject
                .Should()
                .BeCloseTo(ctx.Expectation, 1.Seconds())).WhenTypeIs<DateTimeOffset>();

            return options;
        });
    
        AutoFaker.Configure(builder =>
        {
            // configure global autobogus settings here
            builder.WithRecursiveDepth(1)
                .WithTreeDepth(1)
                .WithRepeatCount(1);
        });
        
        return Task.CompletedTask;
    }
}