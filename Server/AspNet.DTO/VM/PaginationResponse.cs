using System;
using System.Collections.Generic;
using System.Text;

namespace AspNet.DTO.VM
{
    public class PaginationResponse
    {
        public Pagination Pagination { get; set; }
        public Object Data { get; set; }
    }

    public class Pagination
    {
        public string id { get; set; }
        private int _currentPage { get; set; } = 1;
        private int _itemsPerPage { get; set; } = 10;
        public int totalItems { get; set; } = 0;

        public int currentPage
        {
            get
            {
                return _currentPage;
            }

            set
            {
                if (value > 0)
                {
                    _currentPage = value;
                }
                else
                {
                    _currentPage = 1;
                }
            }
        }

        public int itemsPerPage
        {
            get
            {
                return _itemsPerPage;
            }

            set
            {
                if (value > 0)
                {
                    _itemsPerPage = value;
                }
                else
                {
                    _itemsPerPage = 10;
                }
            }
        }
    }

    public class PostFilterPagination
    {
        public PostFilter postFilter { get; set; }
        public Pagination Pagination { get; set; }
    }

    public class PostFilter
    {
        public string User { get; set; }

    }


}
