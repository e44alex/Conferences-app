using Backend.Common.Data;

namespace GraphQL.Common.Models.Payload.Base
{
    public class SessionPayloadBase : Abstract.Payload
    {
        protected SessionPayloadBase(Session session)
        {
            Session = session;
        }

        protected SessionPayloadBase(IReadOnlyList<UserError> errors)
            : base(errors)
        {
        }

        public Session? Session { get; }
    }
}