using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.CommentReaction;
using OnlineStore.Domain.ViewModel.Admin.Comment;
using OnlineStore.Domain.ViewModel.Client.Comment;
using OnlineStore.Domain.ViewModel.Client.ProductComment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Interface
{
    public interface IProductCommentService
    {
        Task<CreateProductCommentViewModel> GetProductForCommentAsync(int productId); 
        Task<CreateProductCommentResult> CreateProductCommentAsync(CreateProductCommentViewModel model);
        Task<FilterProductCommentViewModel> FilterProductCommentAsync(FilterProductCommentViewModel filter);
        Task<ConfirmOrRejectCommentResult> ConfirmCommentAsync(ConfirmOrRejectCommentViewModel model);
        Task<ConfirmOrRejectCommentResult> RejectCommentAsync(ConfirmOrRejectCommentViewModel model);
        Task<List<ClientCommentViewModel>> GetProductCommentsAsync(int productId);
        Task<List<ClientCommentViewModel>> GetProductCommentsByUserIdAsync(int userId);
        Task ProductCommentReactionAsync(ProductReactionCommentViewModel model);
        Task<int> GetAllLikeReactionAsync(int productId,int commentId, ProductCommentReactionType type = ProductCommentReactionType.Like);
        Task<int> GetAllDislikeReactionAsync(int productId,int commentId, ProductCommentReactionType type = ProductCommentReactionType.DisLike);
        Task<List<ProductComment>> GetCommentIsPenddingAsync();
    }
}
