using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Context;
using OnlineStore.Domain.Enum.Product;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Product.Comment;
using OnlineStore.Domain.Model.Product.CommentReaction;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.ViewModel.Admin.Comment;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Data.ImplentationRepository
{
    public class ProductCommentRepository(OnlineStoreContext context) : IProductCommentRepository
    {
        public async Task ConfirmCommentAsync(int commentId)
     => await context.ProductComments.Where(pc => pc.CommentId == commentId)
            .ExecuteUpdateAsync(s => s.SetProperty(pc => pc.Status, ProductCommentStatus.Conmfirmed));

        public void DeleteCommentReaction(ProductCommentReaction reaction)
  => context.productCommentReactions.Remove(reaction);

        public async Task<FilterProductCommentViewModel> FilterProductCommentAsync(FilterProductCommentViewModel filter)
        {
            var query = context.ProductComments.AsQueryable();

            switch (filter.Status)
            {

                case FilterProductCommentstatus.All:
                    break;
                case FilterProductCommentstatus.Pendding:
                    query = query.Where(pc => pc.Status == ProductCommentStatus.Pendding);
                    break;
                case FilterProductCommentstatus.Conmfirmed:
                    query = query.Where(pc => pc.Status == ProductCommentStatus.Conmfirmed);
                    break;
                case FilterProductCommentstatus.Rejected:
                    query = query.Where(pc => pc.Status == ProductCommentStatus.Rejected);
                    break;

            }
            if (filter.ProductId.HasValue)
            {
                query = query.Where(pc => pc.ProductId == filter.ProductId.Value);
            }
            query.OrderByDescending(pc => pc.CreateDate);
            await filter.Paging(query.Select(pc => new ProductCommentViewModel()
            {
                CreateDate = pc.CreateDate,
                Status = pc.Status,
                Advantages = pc.Advantages,
                CommentId = pc.CommentId,
                Disadvantages = pc.Disadvantages,
                ProductId = pc.ProductId,
                Text = pc.Text,
                Title = pc.Title
            }));
            return filter;
        }

        public async Task<int> GetAllDislikeReactionAsync(int productId,int commentId, ProductCommentReactionType type=ProductCommentReactionType.DisLike)
       => await context.productCommentReactions.Where(cr => cr.ProductId == productId&&cr.CommentId==commentId && cr.Type == type).CountAsync();

        public async Task<int> GetAllLikeReactionAsync(int productId,int commentId, ProductCommentReactionType type = ProductCommentReactionType.Like)
=> await context.productCommentReactions.Where(cr => cr.ProductId == productId && cr.CommentId == commentId && cr.Type == type).CountAsync();

        public async Task<List<ProductComment>> GetCommentByProductIdAsync(int productId)
      => await context.ProductComments.Include(pc => pc.User).Include(pc=>pc.Product)
            .Where(pc => pc.Status == ProductCommentStatus.Conmfirmed && pc.ProductId == productId)
            .OrderByDescending(pc => pc.CreateDate).ToListAsync();

        public async Task<List<ProductComment>> GetCommentByUserIdAsync(int userId)
       => await context.ProductComments.Include(pc => pc.Product)
            .Where(pc => pc.UserId == userId)
            .OrderByDescending(pc => pc.CreateDate).ToListAsync();

        public async Task<List<ProductComment>> GetCommentIsPenddingAsync()
  => await context.ProductComments.Where(pc => pc.Status == ProductCommentStatus.Pendding).ToListAsync();

        public async Task<ProductCommentReaction?> GetCommentReactionByCommentIdAsync(int commentId, int userId)
     => await context.productCommentReactions.FirstOrDefaultAsync(cr => cr.CommentId == commentId && cr.UserId == userId);

        public async Task InsertCommentReactionAsync(ProductCommentReaction reaction)
      =>await context.productCommentReactions.AddAsync(reaction);

        public async Task InsertProductCommentAsync(ProductComment productComment)
     => await context.ProductComments.AddAsync(productComment);

        public async Task RejectCommentAsync(int commentId)
 => await context.ProductComments.Where(pc => pc.CommentId == commentId)
         .ExecuteUpdateAsync(s => s.SetProperty(pc => pc.Status, ProductCommentStatus.Rejected));

        public async Task SaveAsync()
      => await context.SaveChangesAsync();

        public void UpdateConmmentReaction(ProductCommentReaction reaction)
   => context.productCommentReactions.Update(reaction);
    }
}
