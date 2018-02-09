using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if(products == null)
            {
                products = new List<Product>();
            }
        }

        public void Commit()
        {
            cache["products"] = products;
        }

        public void Insert(Product p)
        {
            products.Add(p);
        }

        public void Update(Product p)
        {
            Product _p = products.Find(x => x.Id == p.Id);
            if(_p != null)
            {
                _p = p;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public Product Find(string Id)
        {
            Product _p = products.Find(x => x.Id == Id);
            if(_p != null)
            {
                return _p;
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }

        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }

        public void Delete(string Id)
        {
            Product _p = products.Find(x => x.Id == Id);
            if(_p != null)
            {
                products.Remove(_p);
            }
            else
            {
                throw new Exception("Product not found.");
            }
        }
    }
}
