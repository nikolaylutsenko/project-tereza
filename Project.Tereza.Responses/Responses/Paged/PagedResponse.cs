using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Tereza.Responses.Responses;

namespace Project.Tereza.Responses.Responses.Paged;
public class PagedResponse<T> where T : class
{
    private const int MaxSize = 50;

    public int Skip { get; set; }

    // user can take only MaxSize items at one request
    private int _take;
    public int Take
    {
        get { return _take; }
        set { _take = (value > MaxSize) ? MaxSize : value; }
    }

    public int TotalItems { get; set; }

    public IEnumerable<T>? Data { get; set; }
}