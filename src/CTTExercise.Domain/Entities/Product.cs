namespace CTTExercise.Domain.Entities
{
    using System;
    using System.Collections.Generic;

    public class Product
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public required int Stock { get; init; }
        public required string Description { get; init; }
        public required List<Guid> Categories { get; init; }
        public required decimal Price { get; init; }
    }
}
