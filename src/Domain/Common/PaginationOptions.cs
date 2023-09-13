namespace ExampleProject.Domain.Common;

public class PaginationOptions
{
    private int _pageNumber;
    private int _pageSize;

    public PaginationOptions(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public PaginationOptions()
    {
        PageNumber = 1;
        PageSize = 10;
    }
    
    public int PageNumber
    {
        get => _pageNumber;
        set 
        {
            if (value < 1)
            {
                _pageNumber = 1;
            }
            _pageNumber = value;
        }
    }

    public int PageSize
    {
        get => _pageSize;
        set
        {
            if (value < 1)
            {
                _pageSize = 10;
            }
            _pageSize = value;
        }
    }
}