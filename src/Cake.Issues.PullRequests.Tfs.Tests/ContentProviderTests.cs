﻿namespace Cake.Issues.PullRequests.Tfs.Tests
{
    using System;
    using Shouldly;
    using Xunit;

    public sealed class ContentProviderTests
    {
        public sealed class TheGetContentMethod
        {
            [Theory]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "foo",
                null,
                "foo: Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "",
                null,
                "Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                " ",
                null,
                "Some message")]
            [InlineData(
                @"foo.cs",
                123,
                "Some message",
                1,
                "foo",
                "http://google.com",
                "[foo](http://google.com/): Some message")]
            public void Should_Return_Correct_Value(
                string filePath,
                int? line,
                string message,
                int priority,
                string rule,
                string ruleUrl,
                string expectedResult)
            {
                // Given
                Uri ruleUri = null;
                if (!string.IsNullOrWhiteSpace(ruleUrl))
                {
                    ruleUri = new Uri(ruleUrl);
                }

                var issue = new Issue(filePath, line, message, priority, rule, ruleUri, "Foo");

                // When
                var result = ContentProvider.GetContent(issue);

                // Then
                result.ShouldBe(expectedResult);
            }
        }
    }
}
