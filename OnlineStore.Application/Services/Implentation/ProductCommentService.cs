using OnlineStore.Application.Generators;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.CommentReaction;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.ViewModel.Admin.Comment;
using OnlineStore.Domain.ViewModel.Client.Comment;
using OnlineStore.Domain.ViewModel.Client.ProductComment;

namespace OnlineStore.Application.Services.Implentation
{
    public class ProductCommentService(
        IProductCommentRepository commentRepository,
        IProductRepository productRepository) : IProductCommentService 
    {

        public async Task<ConfirmOrRejectCommentResult> ConfirmCommentAsync(ConfirmOrRejectCommentViewModel model)
        {
            if (model.CommentId == null || model.CommentId == 0)
            {
                return ConfirmOrRejectCommentResult.NotFound;
            }

            await commentRepository.ConfirmCommentAsync(model.CommentId);
            return ConfirmOrRejectCommentResult.Success;
        }

        public async Task<CreateProductCommentResult> CreateProductCommentAsync(CreateProductCommentViewModel model)
        {
            ProductComment comment = new()
            {
                Title = model.Title,
                Advantages = model.Advantages,
                CreateDate = DateTime.Now,
                Disadvantages = model.Disadvantages,
                ProductId = model.ProductId,
                Status = ProductCommentStatus.Pendding,
                Text = model.Text,
                UserId = model.UserId,

            };
            await commentRepository.InsertProductCommentAsync(comment);
            await commentRepository.SaveAsync();
            return CreateProductCommentResult.Success;
        }

        public async Task<FilterProductCommentViewModel> FilterProductCommentAsync(FilterProductCommentViewModel filter)
      => await commentRepository.FilterProductCommentAsync(filter);

        public async Task<int> GetAllDislikeReactionAsync(int productId, int commentId, ProductCommentReactionType type = ProductCommentReactionType.DisLike)
   => await commentRepository.GetAllDislikeReactionAsync(productId, commentId, type);

        public async Task<int> GetAllLikeReactionAsync(int productId, int commentId, ProductCommentReactionType type = ProductCommentReactionType.Like)
     => await commentRepository.GetAllLikeReactionAsync(productId, commentId, type);

        public async Task<List<ProductComment>> GetCommentIsPenddingAsync()
      =>await commentRepository.GetCommentIsPenddingAsync();

        public async Task<List<ClientCommentViewModel>> GetProductCommentsAsync(int productId)
     => (await commentRepository.GetCommentByProductIdAsync(productId)).Select(
         pc => new ClientCommentViewModel()
         {
             Advantages = pc.Advantages.SplitWords(),
             Disadvantages = pc.Disadvantages.SplitWords(),
             Status = pc.Status,
             ProductId = pc.ProductId,
             CreateDate = pc.CreateDate,
             commentId = pc.CommentId,
             Text = pc.Text,
             FirstName = pc.User.FirstName,
             LastName = pc.User.LastName,
             Slug = pc.Product.Slug
         }).ToList();

        public async Task<List<ClientCommentViewModel>> GetProductCommentsByUserIdAsync(int userId)
          => (await commentRepository.GetCommentByUserIdAsync(userId)).Select(
           pc => new ClientCommentViewModel()
           {
               Advantages = pc.Advantages.SplitWords(),
               Disadvantages = pc.Disadvantages.SplitWords(),
               Status = pc.Status,
               ProductId = pc.ProductId,
               CreateDate = pc.CreateDate,
               commentId = pc.CommentId,
               Text = pc.Text,
               Product = pc.Product,
           }).ToList();

        public async Task<CreateProductCommentViewModel> GetProductForCommentAsync(int productId)
        {
            Product product = await productRepository.GetProductByIdAsync(productId);
            if (product == null)
            {
                return null;
            }
            CreateProductCommentViewModel productComment = new()
            {
                ProductId = product.ProductId,
                ProductTitle = product.ProductTitle,
                ProductEnTitle = product.TitleForEnglish,
                ProductImage = product.Image,
                Slug = product.Slug,
            };
            return productComment;
        }

        public async Task ProductCommentReactionAsync(ProductReactionCommentViewModel model)
        {
            ProductCommentReaction reaction = await commentRepository
                .GetCommentReactionByCommentIdAsync(model.CommentId, model.UserId);
            if (reaction == null)
            {
                reaction = new()
                {
                    CommentId = model.CommentId,
                    CreateDate = DateTime.Now,
                    ProductId = model.ProductId,
                    Type = model.Type,
                    UserId = model.UserId,
                };
                await commentRepository.InsertCommentReactionAsync(reaction);
                await commentRepository.SaveAsync();
            }
            else if (model.Type == reaction.Type)
            {
                commentRepository.DeleteCommentReaction(reaction);
                await commentRepository.SaveAsync();
            }
            else
            {
                reaction.Type = model.Type;
                commentRepository.UpdateConmmentReaction(reaction);
                await commentRepository.SaveAsync();
            }

        }

        public async Task<ConfirmOrRejectCommentResult> RejectCommentAsync(ConfirmOrRejectCommentViewModel model)
        {
            if (model.CommentId == null || model.CommentId == 0)
            {
                return ConfirmOrRejectCommentResult.NotFound;
            }

            await commentRepository.RejectCommentAsync(model.CommentId);
            return ConfirmOrRejectCommentResult.Success;
        }
    }
}
