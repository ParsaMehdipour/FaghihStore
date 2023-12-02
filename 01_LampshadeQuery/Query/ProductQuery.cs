using _01_LampshadeQuery.Contracts.Product;
using CommnetManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore;
using Inventory.Infrastructure.EFCore.Persistence;
using Microsoft.EntityFrameworkCore;
using PM.Application.Contracts.Order;
using PM.Domain.ProductImageAgg;
using PM.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_LampshadeQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext,
            DiscountContext discountContext, CommentContext commentContext)
        {
            _context = context;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            throw new NotImplementedException();

            //var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductId, x.UnitPrice, x.InStock }).ToList();

            //var discounts = _discountContext.CustomerDiscounts
            //    .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            //    .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            //var product = _context.Products
            //    //.Include(x => x.Category)
            //    .Include(x => x.Images)
            //    .Select(x => new ProductQueryModel
            //    {
            //        Id = x.Id,
            //        //Category = x.Category.Title,
            //        Name = x.TitlePersian,
            //        Slug = x.Slug,
            //        //CategorySlug = x.Category.Slug,
            //        MetaDescription = x.MetaDescription,
            //        Pictures = MapProductPictures(x.Images)
            //    }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            //if (product == null)
            //    return new ProductQueryModel();

            //var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            //if (productInventory != null)
            //{
            //    product.IsInStock = productInventory.InStock;
            //    var price = productInventory.UnitPrice;
            //    product.Price = price.ToMoney();
            //    product.DoublePrice = price;
            //    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            //    if (discount != null)
            //    {
            //        var discountRate = discount.DiscountRate;
            //        product.DiscountRate = discountRate;
            //        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
            //        product.HasDiscount = discountRate > 0;
            //        var discountAmount = Math.Round((price * discountRate) / 100);
            //        product.PriceWithDiscount = (price - discountAmount).ToMoney();
            //    }
            //}

            //product.Comments = _commentContext.Comments
            //    .Where(x => !x.IsCanceled)
            //    .Where(x => x.IsConfirmed)
            //    .Where(x => x.Type == CommentType.Product)
            //    .Where(x => x.OwnerRecordId == product.Id)
            //    .Select(x => new CommentQueryModel
            //    {
            //        Id = x.Id,
            //        Message = x.Message,
            //        Name = x.Name,
            //        CreationDate = x.CreationDate.ToFarsi()
            //    }).OrderByDescending(x => x.Id).ToList();

            //return product;
        }

        private static List<ProductPictureQueryModel> MapProductPictures(List<ProductImage> pictures)
        {
            return pictures.Select(x => new ProductPictureQueryModel
            {
                ProductId = x.ProductId
            }).Where(x => !x.IsRemoved).ToList();
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.Inventories.Select(x => new { x.ProductVarietyId, x.UnitPrice }).ToList();
            var discounts = _discountContext.CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.DiscountRate, x.ProductId }).ToList();
            var products = _context.Products
                //.Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    //Category = product.Category.Title,
                    Name = product.TitlePersian,
                    Slug = product.Slug
                }).AsNoTracking().OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductVarietyId == product.Id);
                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToString();
                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = (price * discountRate) / 100;
                        product.PriceWithDiscount = (price - discountAmount).ToString();
                    }
                }
            }

            return products;

            throw new NotImplementedException();
        }

        public List<ProductQueryModel> Search(string value)
        {
            //var inventory = _inventoryContext.Inventory.Select(x =>
            //    new { x.ProductId, x.UnitPrice }).ToList();
            //var discounts = _discountContext.CustomerDiscounts
            //    .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
            //    .Select(x => new { x.DiscountRate, x.ProductId, x.EndDate }).ToList();

            //var query = _context.Products
            //    //.Include(x => x.Category)
            //    .Select(product => new ProductQueryModel
            //    {
            //        Id = product.Id,
            //        //Category = product.Category.Title,
            //        //CategorySlug = product.Category.Slug,
            //        Name = product.TitlePersian,
            //        Slug = product.Slug
            //    }).AsNoTracking();

            //if (!string.IsNullOrWhiteSpace(value))
            //    query = query.Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));

            //var products = query.OrderByDescending(x => x.Id).ToList();
            //;

            //foreach (var product in products)
            //{
            //    var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);
            //    if (productInventory != null)
            //    {
            //        var price = productInventory.UnitPrice;
            //        product.Price = price.ToMoney();
            //        var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
            //        if (discount == null) continue;

            //        var discountRate = discount.DiscountRate;
            //        product.DiscountRate = discountRate;
            //        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
            //        product.HasDiscount = discountRate > 0;
            //        var discountAmount = Math.Round((price * discountRate) / 100);
            //        product.PriceWithDiscount = (price - discountAmount).ToMoney();
            //    }
            //}

            //return products;
            throw new NotImplementedException();

        }

        public List<CartItem> CheckInventoryStatus(List<CartItem> cartItems)
        {
            //var inventory = _inventoryContext.Inventory.ToList();

            //foreach (var cartItem in cartItems.Where(cartItem =>
            //    inventory.Any(x => x.ProductId == cartItem.Id && x.InStock)))
            //{
            //    var itemInventory = inventory.Find(x => x.ProductId == cartItem.Id);
            //    cartItem.IsInStock = itemInventory.CalculateCurrentCount() >= cartItem.Count;
            //}

            //return cartItems;
            throw new NotImplementedException();

        }
    }
}