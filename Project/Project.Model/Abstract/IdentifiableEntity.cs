namespace Project.Model.Abstract
{
    public abstract record IdentifiableEntity<T>
    {
        public T Id { get; set; }

        protected IdentifiableEntity(T id) => this.Id = id;
    }
}
