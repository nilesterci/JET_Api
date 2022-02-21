using JET.Domain.Entities.Tables;
using JET.Domain.Interfaces;
using JET.Infra.Data.Context;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JET.Infra.Data.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Products> Create(Products body)
        {
            try
            {
                await _context.Products.AddAsync(body);
                _context.SaveChanges();

                return body;
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
                IQueryable<Products> query = _context.Products;

                return query.FirstOrDefault(x => x.Id == id);
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
                IQueryable<Products> query = _context.Products;

                return query.ToList();
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
                IQueryable<Products> query = _context.Products;

                await _context.SaveChangesAsync();

                return _context.Products.FirstOrDefault();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Products> Delete(Products entity)
        {
            try
            {
                IQueryable<Products> query = _context.Products;

                _context.Remove(entity);

                _context.SaveChanges();

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
