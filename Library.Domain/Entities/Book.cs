using Library.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string ISBN { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; private set; }
        public int AvailableCopies { get; private set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }

        public Author Author { get; set; }
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
        public List<Borrowing>? Borrowings { get; set; }


        public void Borrow()
        {
            if (AvailableCopies <= 0)
                throw new NoAvailableCopiesException();

            AvailableCopies--;
        }

        public void Return()
        {
            if (AvailableCopies >= TotalCopies)
                throw new InvalidBookReturnException();

            AvailableCopies++;
        }

        public void SetTotalAndAvailableCopies(int totalCopies)
        {
            if (TotalCopies <= 0)
                throw new InvalidBookCopiesException();

            TotalCopies = totalCopies;
            AvailableCopies = totalCopies;

        }

        public void ChangeTotalCopies(int newTotalCopies)
        {
            if (newTotalCopies <= 0)
                throw new InvalidBookCopiesException();

            var borrowedCopies = TotalCopies - AvailableCopies;

            if (newTotalCopies < borrowedCopies)
            {
                throw new InvalidBookCopiesException(
                    $"Total copies cannot be less than borrowed copies ({borrowedCopies}).");
            }

            TotalCopies = newTotalCopies;
            AvailableCopies = newTotalCopies - borrowedCopies;
        }
    }
}
