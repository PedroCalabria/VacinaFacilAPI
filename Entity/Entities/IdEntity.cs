namespace VacinaFacil.Entity.Entities
{
    public abstract class IdEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
