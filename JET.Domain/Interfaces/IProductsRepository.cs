using JET.Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JET.Domain.Interfaces
{
    public interface IProductsRepository
    {
        Task<List<Products>> Get(int? id);
        Task<Products> Post(Products body);
    }
}
