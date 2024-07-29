﻿namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<BookBorrowingRequestDetails> BorrowingRequestDetails { get; set; }
    }
}
