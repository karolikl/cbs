using doLittle.Concepts;

namespace Concepts
{
    public class HealthRiskReadableId : ConceptAs<int>
    {
        public static readonly HealthRiskReadableId NotSet = -1;

        public static implicit operator HealthRiskReadableId(int id)
        {
            return new HealthRiskReadableId { Value = id };
        }

    }
}
