using Specification.Api.Entities;
using Specification.Api.Mdels;
using Tools.Specification.Abstraction.Interfaces;

namespace Specification.Api.Specifications;

public record SpecModelEntityByDateRange(
    DateTimeOffset? Begin,
    DateTimeOffset? End)
    : ISpec<ModelEntity>,
        ISpec<ModelDto>;
