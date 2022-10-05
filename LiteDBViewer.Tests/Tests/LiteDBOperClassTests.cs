using Xunit;
using FluentAssertions;
using LiteDBViewer.Models;

namespace LiteDBViewer.Tests;

public class LiteDBOperClassTests
{
    [Fact]
    public void OpenTest()
    {
        //LiteDBOper.OpenLDB("/home/wiking/Programowanie/VS Apps/cHash/homePage2/AppData/appData.ldb");
        LiteDBOper.OpenLDB("/home/wiking/Programowanie/VSCode/homePage2/AppData/appData.ldb");
        LiteDBOper.GetTablesNames().Should().NotBeNull().And.NotBeEmpty();
    }
}