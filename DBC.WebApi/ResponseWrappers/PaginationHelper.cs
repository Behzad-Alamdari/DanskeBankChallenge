using DBC.Models;
using System;
using System.Collections.Generic;

namespace DBC.WebApi.ResponseWrappers
{
    public static class PaginationHelper
    {
        public static PagedResponse<List<T>> CreatePagedReponse<T>(List<T> pagedData, Pagination pagination, int totalRecords, IUriService uriService, string route)
        {
            var respose = new PagedResponse<List<T>>(pagedData, pagination.PageNumber, pagination.PageSize);

            var totalPages = totalRecords / (double)pagination.PageSize;
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));

            respose.NextPage =
                pagination.PageNumber >= 1 && pagination.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new Pagination(pagination.PageNumber + 1, pagination.PageSize), route)
                : null;
            respose.PreviousPage =
                pagination.PageNumber - 1 >= 1 && pagination.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new Pagination(pagination.PageNumber - 1, pagination.PageSize), route)
                : null;
            respose.FirstPage = uriService.GetPageUri(new Pagination(1, pagination.PageSize), route);
            respose.LastPage = uriService.GetPageUri(new Pagination(roundedTotalPages, pagination.PageSize), route);
            respose.TotalPages = roundedTotalPages;
            respose.TotalRecords = totalRecords;
            return respose;
        }
    }
}
