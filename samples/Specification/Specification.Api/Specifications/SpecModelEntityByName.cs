using Specification.Api.Entities;
using Specification.Api.Mdels;
using Tools.Specification.Abstraction.Interfaces;

namespace Specification.Api.Specifications;

public record SpecModelEntityByName(string Name)
    : ISpec<ModelEntity>,
        ISpec<ModelDto>;