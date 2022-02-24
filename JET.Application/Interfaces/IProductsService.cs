using JET.Domain.Entities.Tables;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JET.Application.Interfaces
{
    public interface IProductsService
    {
        Task<Products> Create(Products body);
        Products GetById(int id);
        List<Products> GetAll(bool status, string search);
        Task<Products> Patch(JsonPatchDocument<Products> body);
        Task<Products> Delete(Products body);
    }
}
