using System;
using Xunit;
using FluentAssertions;
using CrmTest.Entities;

namespace CrmTest.Tests.Notes
{
    public class NoteEntityTests
    {
        [Fact]
        public void Note_ShouldBeCreatable()
        {
            // Act
            var entity = new Note();

            // Assert
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Note_ShouldHaveDefaultValues()
        {
            // Act
            var entity = new Note();

            // Assert
            entity.Id.Should().Be(default(long));

        }

        [Fact]
        public void Note_Text_ShouldAcceptValue()
        {
            var entity = new Note { Text = "Test Value" };
            entity.Text.Should().Be("Test Value");
        }

    }
}
