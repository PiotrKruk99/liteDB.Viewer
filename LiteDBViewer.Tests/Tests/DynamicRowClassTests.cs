using System;
using Xunit;
using FluentAssertions;
using LiteDBViewer.Models;

namespace LiteDBViewer.Tests;

public class DynamicRowTests
{
    private DynamicRow testObject = new();
    [Fact]
    public void CreateObjectTest()
    {
        testObject.Should().NotBeNull();
    }

    [Fact]
    public void AddingElements()
    {
        testObject.Add("lalala");
        testObject.Add("tralalala");
        testObject.Add("bumparara");
        Console.WriteLine(testObject.Count);
        foreach (var elem in testObject)
        {
            Console.WriteLine(elem);
        }
    }
}