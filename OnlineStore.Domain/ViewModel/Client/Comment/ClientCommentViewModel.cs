using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.Model.Product.Product;

namespace OnlineStore.Domain.ViewModel.Client.Comment
{
    public class ClientCommentViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ProductId { get; set; }

        public string? Slug { get; set; }

        public int commentId { get; set; }

        public string Text { get; set; }

        public List<string> Disadvantages { get; set; }

        public List<string> Advantages { get; set; }

        public Model.Product.Product.Product? Product { get; set; }

        public DateTime CreateDate { get; set; }
        public ProductCommentStatus Status { get; set; }
    }
}
