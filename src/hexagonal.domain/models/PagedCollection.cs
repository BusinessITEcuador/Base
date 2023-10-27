namespace hexagonal.domain.models
{
    public class PagedCollection<T>
    {
        public int Offset { get; set; }

        public int Limit { get; set; }

        public int Size { get; set; }

        public T[] Items { get; set; }
    }
}