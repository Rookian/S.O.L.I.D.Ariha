// Type: MvcContrib.CommandProcessor.Validation.Rules.AbstractCrossReferenceValidationRule`1
// Assembly: MvcContrib.CommandProcessor, Version=2.0.96.0, Culture=neutral
// Assembly location: G:\S.O.L.I.D.Ariha\S.O.L.I.D.Ariha\Libraries\MVCContrib\MvcContrib.CommandProcessor.dll

using MvcContrib.CommandProcessor.Validation;

namespace MvcContrib.CommandProcessor.Validation.Rules
{
    public abstract class AbstractCrossReferenceValidationRule<TTarget> : ICrossReferencedValidationRule,
                                                                          IValidationRule
    {
        #region ICrossReferencedValidationRule Members

        public string IsValid(object toCheck);
        public object ToCompare { get; set; }
        public virtual bool StopProcessing { get; }

        #endregion

        protected abstract string IsValidCore(TTarget toCheck, TTarget toCompare);
        protected string Success();
    }
}
