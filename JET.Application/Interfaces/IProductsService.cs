using JET.Domain.Entities.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JET.Application.Interfaces
{
    public interface IProductsService
    {
        Task<List<Products>> Get();
        Task<Products> Post(Products body);
    }
}
