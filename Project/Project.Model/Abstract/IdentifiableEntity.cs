namespace Project.Model.Abstract
{
    public abstract record IdentifiableEntity<T>
    {
        public T Id { get; set; }

        public IdentifiableEntity(T id) => this.Id = id;
    }
}
