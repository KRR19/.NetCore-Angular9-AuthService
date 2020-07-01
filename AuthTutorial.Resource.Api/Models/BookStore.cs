using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTutorial.Resource.Api.Models
{
    public class BookStore
    {
        public List<Book> Books => new List<Book>
        {
            new Book { Id = 1, Author = "J. K. Rowling", Title ="Harry Potter", Price = 10.45M},
            new Book { Id = 2, Author = "Герман Мелвилл", Title = "Моби Дик", Price = 8.52M},
            new Book { Id = 3, Author = "Jules Verne", Title = "The Mysterious Island", Price = 7.13M},
            new Book { Id = 4, Author="Carlo Collodi", Title = "The Adventures of Pinocchio", Price = 6.42M}
        };

        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            { Guid.Parse("42E4F8AF-FCCD-4BE9-BC12-ADB0F050DBD1"), new int[]{ 1, 3} },
            { Guid.Parse("7db9e12c-00a9-4ba1-9bcf-83a8529f1a4d"), new int[]{ 2, 3, 4} }
        };
    }
}
