using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Model.Abstract
{
    public abstract record IdentifiableEntity<T>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // ids are generated in code
        public T Id { get; set; }

        protected IdentifiableEntity(T id) => this.Id = id;
    }
}
