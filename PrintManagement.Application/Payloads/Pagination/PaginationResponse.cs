using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.Pagination
{
	public class PaginationResponse<T>
	{
		public PaginationResponse(PaginationRequest paginationRequest, IEnumerable<T> data)
		{
			PaginationRequest = paginationRequest;
			Data = data;
		}

		public PaginationRequest PaginationRequest { get; set; }
        public IEnumerable<T> Data { get; set; }
		public static List<T> ToPageResult(PaginationRequest request, List<T> data)
		{
			request.PageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
			data = data.Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).ToList();
			return data;
		}
	}
}
