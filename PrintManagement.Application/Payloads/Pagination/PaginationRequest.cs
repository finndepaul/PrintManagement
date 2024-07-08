using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagement.Application.Payloads.Pagination
{
	public class PaginationRequest
	{
        public int PageSize { get; set; }
		public int PageNumber { get; set; }
		public int TotalCount { get; set; }
		public int TotalPage
		{
			get
			{
				if (PageSize == 0) return 0;
				var total = TotalCount / PageSize;
				if (TotalCount % PageSize != 0) // trường hợp 9 bản ghi thì ghi phân trang sẽ bị thiếu 1 bản ghi 
					total++;
				return total;
			}
		}
	}
}
