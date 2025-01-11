using OnlineStore.Application.Services.Interface;
using OnlineStore.Domain.Enum.Wallet;
using OnlineStore.Domain.InterfaceRepository;
using OnlineStore.Domain.Model.Order;
using OnlineStore.Domain.Model.Product.Color;
using OnlineStore.Domain.Model.Product.Product;
using OnlineStore.Domain.Model.User.User;
using OnlineStore.Domain.Model.Wallet;
using OnlineStore.Domain.Shared;
using OnlineStore.Domain.ViewModel.Client.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.Application.Services.Implentation
{
    public class OrderService(IOrderRepository orderRepository,
       IProductColorRepository colorRepository,
       IProductRepository productRepository,
       IWalletRepository walletRepository,
       IWalletService walletService) : IOrderService
    {
        public async Task<AddToOrderResult> CreateOrderAsync(AddToOrderViewModel model)
        {
            Product? product;
            ProductColor? productColor;
            Order order = await orderRepository.GetOrderByUserIdAsync(model.UserId);
            if (order != null)
            {
                OrderDetail? orderDetail = await orderRepository.GetOrderDetailByOrderIdAsync(order.OrderId, model.ProductId, model.ColorId);
                if (orderDetail != null)
                {
                    if (model.ColorId.HasValue)
                    {
                        productColor = await colorRepository.GetProductColorByIdAsync(model.ColorId ?? 0);
                        if (orderDetail.Quantity >= productColor.Quantity)
                        {
                            return AddToOrderResult.QuantityInavlid;
                        }
                    }
                    else
                    {
                        product = await productRepository.GetProductByIdAsync(model.ProductId);
                        if (orderDetail.Quantity >= product.Quantity)
                        {
                            return AddToOrderResult.QuantityInavlid;
                        }
                    }
                    orderDetail.Quantity++;
                    orderRepository.UpdateOrderDetail(orderDetail);
                    await orderRepository.SaveAsync();

                    productColor = await colorRepository.GetProductColorByIdAsync(model.ColorId ?? 0);
                    productColor.Quantity--;
                    colorRepository.UpdateProductColor(productColor);

                    product = await productRepository.GetProductByIdAsync(model.ProductId);
                    product.Quantity--;
                    productRepository.EditProduct(product);

                    await colorRepository.SaveAsync();

                }
                else
                {
                    int price = default;

                    if (model.ColorId.HasValue)
                    {
                        productColor = await colorRepository.GetProductColorByIdAsync(model.ColorId ?? 0);
                        price = productColor.Price;
                    }
                    else
                    {
                        product = await productRepository.GetProductByIdAsync(model.ProductId);
                        price = product.Price;
                    }
                    OrderDetail newDetail = new()
                    {
                        OrderId = order.OrderId,
                        Price = price,
                        ProductColorId = model.ColorId,
                        Quantity = 1,
                        CreateDate = DateTime.Now,
                        ProductId = model.ProductId,
                    };
                    await orderRepository.InsertOrderDetailAsync(newDetail);
                    await orderRepository.SaveAsync();

                    productColor = await colorRepository.GetProductColorByIdAsync(newDetail.ProductColorId ?? 0);
                    productColor.Quantity--;
                    colorRepository.UpdateProductColor(productColor);

                    product = await productRepository.GetProductByIdAsync(newDetail.ProductId);
                    product.Quantity--;
                    productRepository.EditProduct(product);

                    await colorRepository.SaveAsync();
                }
            }
            else
            {
                Order newOrder = new()
                {
                    CreateDate = DateTime.Now,
                    IsFinally = false,
                    UserId = model.UserId,
                    LocationId = 0

                };
                await orderRepository.InsertOrderAsync(newOrder);
                await orderRepository.SaveAsync();

                int price = default;

                if (model.ColorId.HasValue)
                {
                    productColor = await colorRepository.GetProductColorByIdAsync(model.ColorId ?? 0);
                    price = productColor.Price;
                }
                else
                {
                    product = await productRepository.GetProductByIdAsync(model.ProductId);
                    price = product.Price;
                }
                OrderDetail newDetail = new()
                {
                    OrderId = newOrder.OrderId,
                    Price = price,
                    ProductColorId = model.ColorId,
                    Quantity = 1,
                    CreateDate = DateTime.Now,
                    ProductId = model.ProductId,
                };
                await orderRepository.InsertOrderDetailAsync(newDetail);
                await orderRepository.SaveAsync();

                productColor = await colorRepository.GetProductColorByIdAsync(newDetail.ProductColorId ?? 0);
                productColor.Quantity--;
                colorRepository.UpdateProductColor(productColor);

                product = await productRepository.GetProductByIdAsync(newDetail.ProductId);
                product.Quantity--;
                productRepository.EditProduct(product);

                await orderRepository.SaveAsync();
            }

            return AddToOrderResult.Success;
        }

        public async Task<ProductQuantityResult> DecreaseProductQuantityAsync(ProductQuantityViewModel model)
        {
            OrderDetail orderDetail = await GetOrderDetailByIdAsync(model.Id);
            if (orderDetail == null)
            {
                return ProductQuantityResult.NotFoundDetail;
            }

            orderDetail.Quantity--;
            orderRepository.UpdateOrderDetail(orderDetail);

            Product product = await productRepository.GetProductByIdAsync(orderDetail.ProductId);
            product.Quantity++;
            productRepository.EditProduct(product);

            ProductColor productColor = await colorRepository.GetProductColorByIdAsync(orderDetail.ProductColorId ?? 0);
            productColor.Quantity++;
            colorRepository.UpdateProductColor(productColor);

            await orderRepository.SaveAsync();
            if (orderDetail.Quantity <= 0)
            {
                await orderRepository.DeleteOrderDetailAsync(model.Id);

            }

            return ProductQuantityResult.Success;
        }

        public async Task<ProductQuantityResult> DeleteOrderDetailAsync(ProductQuantityViewModel model)
        {
            OrderDetail orderDetail = await GetOrderDetailByIdAsync(model.Id);
            if (orderDetail == null)
            {
                return ProductQuantityResult.NotFoundDetail;
            }

            Product product = await productRepository.GetProductByIdAsync(orderDetail.ProductId);
            product.Quantity = orderDetail.Quantity + product.Quantity;
            productRepository.EditProduct(product);

            ProductColor productColor = await colorRepository.GetProductColorByIdAsync(orderDetail.ProductColorId ?? 0);
            productColor.Quantity = orderDetail.Quantity + productColor.Quantity;
            colorRepository.UpdateProductColor(productColor);

            await orderRepository.SaveAsync();

            await orderRepository.DeleteOrderDetailAsync(model.Id);
            return ProductQuantityResult.Success;
        }

        public async Task<OrderViewModel> GetOrderByUserIdAsync(int userId)
        {
            Order order = await orderRepository.GetOrderByUserIdAsync(userId);
            if (order == null)
            {
                return null;
            }
            OrderViewModel model = new()
            {
                CreateDate = order.CreateDate,
                IsFinally = order.IsFinally,
                orderDetails = order.OrderDetails,
                OrderId = order.OrderId,
                UserId = userId,

            };
            return model;

        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int detailId)
   => await orderRepository.GetOrderDetailByIdAsync(detailId);

        public async Task<List<OrderViewModel>> OrderListIsFinalyAsync(int userId)
        {

            return (await orderRepository.OrderListIsFinalydAsync(userId)).Select(
                order => new OrderViewModel()
                {
                    CreateDate = order.CreateDate,
                    IsFinally = order.IsFinally,
                    orderDetails = order.OrderDetails,
                    OrderId = order.OrderId,
                    UserId = userId,
                    Price = order.OrderDetails.FirstOrDefault().Price,
                    RefId = walletRepository.GetWalletByOrderIdAsync(order.OrderId).Result.RefId,
                }).ToList();


        }

        public async Task<ProductQuantityResult> IncreaseProductQuantity(ProductQuantityViewModel model)
        {
            OrderDetail orderDetail = await GetOrderDetailByIdAsync(model.Id);
            if (orderDetail == null)
            {
                return ProductQuantityResult.NotFoundDetail;
            }
            int quntity = orderDetail.ProductColor != null && orderDetail.ProductColor.Quantity.HasValue ?
            orderDetail.ProductColor.Quantity.Value :
             orderDetail.Product.Quantity;

            orderDetail.Quantity++;
            if (orderDetail.Quantity > quntity)
            {
                return ProductQuantityResult.NotFoundDetail;
            }
            orderRepository.UpdateOrderDetail(orderDetail);

            Product product = await productRepository.GetProductByIdAsync(orderDetail.ProductId);
            product.Quantity--;
            productRepository.EditProduct(product);

            ProductColor productColor = await colorRepository.GetProductColorByIdAsync(orderDetail.ProductColorId ?? 0);
            productColor.Quantity--;
            colorRepository.UpdateProductColor(productColor);

            await orderRepository.SaveAsync();
            return ProductQuantityResult.Success;
        }

        public async Task InsertAddressToOrderAsync(int addressId, int userId)
     => await orderRepository.InsertAddressToOrderAsync(addressId, userId);
        public async Task<PayOrderResult> PayOrderAsync(PayOrderViewModel model)
        {
            int walletPrice = await walletService.BalanceAmountAsync(model.UserId ?? 0);
            Order order = await orderRepository.GetOrderByIdAsync(model.OrderId);
            if (order == null)
            {
                return PayOrderResult.NotFoundOrder;
            }
            int price = order.OrderDetails.Sum(od => od.Price * od.Quantity);
            if (price > walletPrice)
            {
                Wallet wallet = new()
                {
                    Case = TransactionCase.ChargeWallet,
                    CreateDate = DateTime.Now,
                    Description = "شارژ کیف پول جهت خرید کالا",
                    OrderId = order.OrderId,
                    Payed = false,
                    IP = model.Ip,
                    UserId = model.UserId ?? 0,
                    Price = price,
                    RefId = null,
                    Type = TransactionType.Deposit,
                };
                await walletRepository.ChargeWalletAsync(wallet);
                await walletRepository.SaveAsync();
            }

            return PayOrderResult.Success;
        }

        public async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            if (orderDetail != null)
            {
                orderRepository.UpdateOrderDetail(orderDetail);
                await orderRepository.SaveAsync();
            }
        }

        public async Task<OrderViewModel> GetOrderIsFinalyAsync(int userId, int orderId)
        {
            Order order = await orderRepository.GetOrderIsFinalydAsync(userId, orderId);
            if (order == null)
            {
                return null;
            }
            OrderViewModel model = new()
            {
                CreateDate = order.CreateDate,
                IsFinally = order.IsFinally,
                orderDetails = order.OrderDetails,
                OrderId = order.OrderId,
                UserId = userId,
                RefId = walletRepository.GetWalletByOrderIdAsync(order.OrderId).Result.RefId,

            };
            return model;
        }

        public async Task<int> OrderCountForOneYearAsync()
        {
            List<Order> orders = await orderRepository.OrderListIsFinalyAsync();

            return orders.Count(o => o.CreateDate >= DateTime.Now.AddYears(-1));
        }

        public async Task<int> OrderSumPriceForOneYearAsync()
        {
            List<Order> orders = await orderRepository.OrderListIsFinalyAsync();
            orders = orders.Where(o => o.CreateDate >= DateTime.Now.AddYears(-1)).ToList();
            return orders.Sum(o => o.OrderDetails.FirstOrDefault().Price);
        }

        public async Task<int> OrderCountForOneMonthAsync()
        {
            List<Order> orders = await orderRepository.OrderListIsFinalyAsync();

            return orders.Count(o => o.CreateDate >= DateTime.Now.AddMonths(-1));
        }

        public async Task<int> OrderSumPriceForOneMonthAsync()
        {
            List<Order> orders = await orderRepository.OrderListIsFinalyAsync();
            orders = orders.Where(o => o.CreateDate >= DateTime.Now.AddMonths(-1)).ToList();
            return orders.Sum(o => o.OrderDetails.FirstOrDefault().Price);
        }

        public async Task<List<OrderDetail>> GetAllOrderDetailForJobAsync()
   => await orderRepository.GetAllOrderDetailForJobAsync();

        public async Task DeleteOrderDetailAsync(int detailId)
    => await orderRepository.DeleteOrderDetailAsync(detailId);
    }
}

