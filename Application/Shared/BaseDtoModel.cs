#nullable disable

namespace Application.Shared
{
    public abstract class BaseDtoModel<T>
    {
        public T Id { get; set; }
    }
}