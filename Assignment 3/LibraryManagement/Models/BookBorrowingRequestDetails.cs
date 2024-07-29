namespace LibraryManagement.Models
{
    public class BookBorrowingRequestDetails
    {
        public int BookBorrowingRequestDetailsId { get; set; }
        public int BookBorrowingRequestId { get; set; }
        public BookBorrowingRequest BookBorrowingRequest { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
