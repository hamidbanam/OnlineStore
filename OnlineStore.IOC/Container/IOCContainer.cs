using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Application.Sender.Implentation;
using OnlineStore.Application.Sender.Interface;
using OnlineStore.Application.Services.Implentation;
using OnlineStore.Application.Services.Interface;
using OnlineStore.Data.ImplentationRepository;
using OnlineStore.Domain.InterfaceRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Ioc.Container
{
    public static class IOCContainer
    {
        public static void RegisterService(this IServiceCollection services)
        {
            #region Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IFeatureRepository, FeatureRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductGallaryRepository, ProductGallaryRepository>();
            services.AddScoped<IProductFeatureRepository, ProductFeatureRepository>();
            services.AddScoped<IProductColorRepository, ProductColorRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
            services.AddScoped<IWalletRepository, WalletRepository>();
            services.AddScoped<IContactUsRepository, ContactUsRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            #endregion

            #region Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductGallaryService, ProductGallaryService>();
            services.AddScoped<IProductFeatureService, ProductFeatureService>();
            services.AddScoped<IProductColorService, ProductColorService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductCommentService, ProductCommentService>();
            services.AddScoped<IWalletService, WalletService>();
            services.AddSingleton<ISmsSender, SmsSender>();
            services.AddScoped<INovinoService, NovinoService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IContactUsService, ContactUsService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IClientService, ClientService>();

            #endregion
        }
    }
}
