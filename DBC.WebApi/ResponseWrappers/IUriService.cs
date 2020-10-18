using DBC.Models;
using System;

namespace DBC.WebApi.ResponseWrappers
{
    public interface IUriService
    {
        public Uri GetPageUri(Pagination pagination, string route);
    }
}
