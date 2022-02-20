using JET.Domain.Entities.Tables;
using JET.Domain.Interfaces;
using JET.Infra.Data.Context;
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

        public async Task<List<Products>> Get(int? id)
        {
            try
            {
                IQueryable<Products> query = _context.Products;

                //query = query.Skip((filtro.page - 1) * filtro.pageSize);

                //query = query.Take(filtro.pageSize);

                if (id != null)
                    query = query.Where(x => x.Id == id);

                //if (!String.IsNullOrEmpty(filtro.search))
                //query = query.Where(x => x.nome_produto.Contains(filtro.search) || x.observacao.Contains(filtro.search) || x.tipo.Contains(filtro.search));

                return await query.ToListAsync();
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
                await _context.Products.AddAsync(body);
                _context.SaveChanges();

                return body;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
