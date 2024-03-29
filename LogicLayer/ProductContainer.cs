﻿using InterfaceLayer;
using InterfaceLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public class ProductContainer
    {
        private readonly IProductContainer iProductContainer;

        public ProductContainer (IProductContainer iProd)
        {
            iProductContainer = iProd;
        }
        /// <summary>
        /// Get all Products.
        /// </summary>
        /// <returns>A List of Products</returns>
        public List<Product> GetAll()
        {
            return iProductContainer.GetAll().ConvertAll(productDTO => new Product(productDTO));
        }
        /// <summary>
        /// Get all products of a certain type (Food, Drink, etc.).
        /// </summary>
        /// <param name="type">The type of Product (Food, Drink)</param>
        /// <returns>A List of Products where the type is corresponding with the type given.</returns>
        public List<Product> GetAllOfType(int type)
        {
            return iProductContainer.GetAllOfType(type).ConvertAll(x => new Product(x));
        }
        /// <summary>
        /// Get a certain Product
        /// </summary>
        /// <param name="id">The Id of the Product.</param>
        /// <returns>A Product corresponding with the given Id.</returns>
        public Product GetProduct(int id)
        {
            return new Product(iProductContainer.GetProduct(id));
        }
        /// <summary>
        /// Create a new Product
        /// </summary>
        /// <param name="product">Product to be Created.</param>
        /// <returns>The newly create Product.</returns>
        public bool CreateProduct(Product product)
        {
            return iProductContainer.CreateProduct(product.ToDTOWithoutId());
        }

        /// <summary>
        /// Delete the Product.
        /// </summary>
        /// <param name="id">The id of the Product thats has to be deleted.</param>
        /// <returns>Succesfulness (bool) of deleting the Product.</returns>
        public bool DeleteProduct(int id)
        {
            return iProductContainer.DeleteProduct(id);
        }

        public List<ProductType> GetAllTypes()
        {
            return iProductContainer.GetAllTypes().ConvertAll(typeDTO => new ProductType(typeDTO));
        }

        public bool CheckProduct(Product product)
        {
            if (product == null) return false;
            if (product.Name == "") return false;
            if (Math.Round(product.Price, 2) <= 0) return false;
            if (product.Type < 1 || product.Type > 3) return false;
            return true;
        }
    }

}
