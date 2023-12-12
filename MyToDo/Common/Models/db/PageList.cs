namespace MyToDo.Common.Models.db
{
    public class PageList<T>
    {
        public int total { get; set; }

        public List<T> list { get; set; }

        public int pageNum { get; set; }

        public int pageSize { get; set; }

        public int size { get; set; }

        public int startRow { get; set; }

        public int endRow { get; set; }

        public int pages { get; set; }

        public int prePage { get; set; }

        public int nextPage { get; set; }

        public bool isFirstPage { get; set; }

        public bool isLastPage { get; set; }

        public bool hasPreviousPage { get; set; }

        public bool hasNextPage { get; set; }

        public int navigatePages { get; set; }

        public List<int> navigatePageNums { get; set; }

        public int navigateFirstPage { get; set; }

        public int navigateLastPage { get; set; }
    }
}
