using Application.Adapters.Internals;
using Application.Adapters.Requests;

namespace Application.Interfaces;

public interface ICustommerValidation
{
    public List<FieldErrorInternalAdapter>? ValidateCustomerCard(CardIdentifierAdapter input);
}
