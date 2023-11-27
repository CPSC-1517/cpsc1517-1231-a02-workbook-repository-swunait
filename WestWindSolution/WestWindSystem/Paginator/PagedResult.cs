using System.Collections.Generic;
using System.ComponentModel;


namespace WestWindSystem.Paginator
{
    public class PagedResult<T> : PagedResultBase where T : class
    {
        public T[] Results { get; set; }
    }
}