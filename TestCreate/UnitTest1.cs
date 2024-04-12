

namespace YourNamespace.Tests
{
    public class IndexPageTests
    {
        [Fact]
        public void IndexPage_ShouldDisplayColumns()
        {
            // Arrange
            var indexModel = new Webs1te.Pages.Clients.IndexModel();
            var expectedColumns = new List<string> { "ID", "Name", "Course", "Phone", "Address", "Create At" };

            // Act
            var actualColumns = new List<string>();
            foreach (var column in indexModel.listClients)
            {
                actualColumns.Add((string)column);
            }

            // Assert
            Assert.Equal(expectedColumns.Count, actualColumns.Count);

            foreach (var expectedColumn in expectedColumns)
            {
                Assert.Contains(expectedColumn, actualColumns);
            }
        }
    }
}
