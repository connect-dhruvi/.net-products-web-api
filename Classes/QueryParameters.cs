using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace web_api.Classes
{
    public class QueryParameters
    {
        const int _maxSize = 100;
        internal object MinPrice;
        private int _size = 20;


        public int Page { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Size
        {
            get { return _size; }
            set { _size = Math.Min(_maxSize, value); }

        }

        public string SortBy { get; set; } = "Id";
        public string _sortOrder = "asc";
        public string SortOrder
        {
            get { return _sortOrder; }
            set
            {
                if (value == "asc" || value == "desc")
                {
                     _sortOrder = value;
                }
            }
        }

    }
}
