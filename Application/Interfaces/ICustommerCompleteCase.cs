using Application.Adapters.Requests;
using Application.Adapters.Responses;

namespace Application.Interfaces;

public interface ICustommerCompleteCase
{
    public CustommerRetriveAdapter GetCompleteData(CardIdentifierAdapter inputApi);
}
