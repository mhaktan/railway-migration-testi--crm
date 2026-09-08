using System;
using Xunit;
using FluentAssertions;
using CrmTest.Entities;

namespace CrmTest.Tests.Customers
{
    public class CustomerEntityTests
    {
        [Fact]
        public void Customer_ShouldBeCreatable()
        {
            // Act
            var entity = new Customer();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Customer_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Customer();

            // Assert
            entity.Id.Should().Be(default(long));
            entity.IsActive.Should().Be(false);
        }

        [Fact]
        public void Customer_Name_ShouldAcceptValue()
        {
            var entity = new Customer { Name = "Test Value" };
            entity.Name.Should().Be("Test Value");
        }

    }
}
