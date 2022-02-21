using JET.Application.Interfaces;
using JET.Domain.Entities.Tables;
using JET.Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JET.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _repository;

        public ProductsService(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Products> Create(ProductCreateOrUpdate body)
        {
            try
            {
                var entity = new Products()
                {
                    ProductName = body.ProductName,
                    Description = body.Description,
                    Stock = body.Stock,
                    Status = body.Status,
                    Price = body.Price
                };

                return await _repository.Create(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Products GetById(int id)
        {
            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Products> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Products> Patch(JsonPatchDocument<Products> body)
        {
            try
            {
                return await _repository.Patch(body);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<Products> Delete(Products entity)
        {
            try
            {
                return await _repository.Delete(entity);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
