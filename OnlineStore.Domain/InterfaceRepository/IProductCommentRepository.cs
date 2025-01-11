using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.CommentReaction;
using OnlineStore.Domain.ViewModel.Admin.Comment;

namespace OnlineStore.Domain.InterfaceRepository
{
    public interface IProductCommentRepository
    {
        Task InsertProductCommentAsync(ProductComment productComment);
        Task SaveAsync();
        Task<FilterProductCommentViewModel> FilterProductCommentAsync(FilterProductCommentViewModel filter);
        Task ConfirmCommentAsync(int commentId);
        Task RejectCommentAsync(int commentId);
        Task<List<ProductComment>> GetCommentByProductIdAsync(int productId);
        Task<List<ProductComment>> GetCommentByUserIdAsync(int userId);
        Task<ProductCommentReaction> GetCommentReactionByCommentIdAsync(int commentId, int userId);
        Task InsertCommentReactionAsync(ProductCommentReaction reaction);
        void DeleteCommentReaction(ProductCommentReaction reaction);
        void UpdateConmmentReaction(ProductCommentReaction reaction);
        Task<int> GetAllLikeReactionAsync(int productId,int commentId,ProductCommentReactionType type = ProductCommentReactionType.Like);
        Task<int> GetAllDislikeReactionAsync(int productId,int commentId,ProductCommentReactionType type=ProductCommentReactionType.DisLike);
        Task<List<ProductComment>> GetCommentIsPenddingAsync();
    }
}
