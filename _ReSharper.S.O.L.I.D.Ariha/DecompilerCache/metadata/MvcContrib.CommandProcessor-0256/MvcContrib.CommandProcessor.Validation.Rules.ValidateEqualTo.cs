// Type: MvcContrib.CommandProcessor.Validation.Rules.ValidateEqualTo
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

namespace MvcContrib.CommandProcessor.Validation.Rules
{
    public class ValidateEqualTo : AbstractCrossReferenceValidationRule<object>
    {
        protected virtual string ErrorMessage { get; }
        protected override string IsValidCore(object toCheck, object toCompare);
    }
}
