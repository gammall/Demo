using ConsoleApp.Models;
using ConsoleApp.Services;
using System.Net;
using System.Reflection.Metadata;


namespace ConsoleApp.Tests;

public class CustomerService_Test
{

    ICustomerService customerService = new CustomerService();

    [Fact]
    public void AddToList_Should_AddCustomerToList_ReturnTrue()
    {
        // Arrange
        User customer = new User // vi skapar en kund för att kunna testa att metoden nedan funkar
        {
            Id = 1,
            FirstName = "Felix",
            LastName = "Felix",
            PhoneNumber = "Felix",
            Email = "Felix",
            Address = "Felix"
        };
        ICustomerService customerService = new CustomerService(); 

        // Act
        bool result = customerService.AddCustomerToList(customer); // Detta ar vad som ska testas, laggs det till en anvandare nar jag kallar funktionen AddCustomerToList()?

        // Assert
        Assert.True(result); // om det laggs till en anvandare så får vi ett true-varde
    }
}
