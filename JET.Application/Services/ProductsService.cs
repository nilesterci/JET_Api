using JET.Application.Interfaces;
using JET.Domain.Entities.Tables;
using JET.Domain.Interfaces;
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
        public async Task<List<Products>> Get(int? id)
        {
            try
            {
                return await _repository.Get(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Products> Post(Products body)
        {
            try
            {
                return await _repository.Post(body);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
