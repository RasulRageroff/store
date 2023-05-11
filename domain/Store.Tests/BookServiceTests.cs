using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Tests
{
    public class BookServiceTests
    {
        [Fact]
        public void GetAllByQuery_WithIsbn_CallsGetAllByIsbn()
        {
            var bookRepositoryStub = new Mock<IBookRepository>();

            bookRepositoryStub.Setup(x => x.GetAllByIsnb(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "") });

            bookRepositoryStub.Setup(x => x.GetByAllTitleOrAuthor(It.IsAny<string>()))
                .Returns(new[] { new Book(1, "", "", "") });

            var bookService = new BookService(bookRepositoryStub.Object);
            var valid = "ISBN 12345-67890";
            var actual = bookService.GetAllByQuery("ISBN 12345-67890");
            Assert.Collection(actual, book => Assert.Equal(1, book.Id));
        }
            [Fact]
            public void GetAllByQuery_WithIsbn_CallsGetAllByTitleOrAuthor()
            {
                var bookRepositoryStub = new Mock<IBookRepository>();

                bookRepositoryStub.Setup(x => x.GetAllByIsnb(It.IsAny<string>()))
                    .Returns(new[] { new Book(1, "", "", "") });

                bookRepositoryStub.Setup(x => x.GetByAllTitleOrAuthor(It.IsAny<string>()))
                    .Returns(new[] { new Book(2, "", "", "") });

                var bookService = new BookService(bookRepositoryStub.Object);
                var invalidIsbn = "12345-67890";
                var actual = bookService.GetAllByQuery(invalidIsbn);
                Assert.Collection(actual, book => Assert.Equal(1, book.Id));
            }
    }
}
