using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmcureNPD.Business.Models
{
    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public int page { get; set; }
        public int pages { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTableResponseModel
    {
        public DataTableResponseModel(int _draw, int _recordsTotal, int _recordsFiltered, dynamic _data)
        {
            this.draw = _draw;
            this.recordsTotal = _recordsTotal;
            this.recordsFiltered = _recordsFiltered;
            this.Data = _data;
        }

        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public dynamic Data { get; set; }
    }
    public class PendingNotification
    {
        public int Count { get; set; }
       
    }
}
