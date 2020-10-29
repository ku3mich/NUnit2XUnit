namespace SyntaxConverters
{
    public abstract class MethodInvocationRewiterBase : MethodInvocationFactoryBase
    {
        protected override ISyntaxConverter CreateConverter(MethodInvocation param)
        {
            if (!RewriteCondition(param))
                return null;

            return CreateConverterInternal(param);
        }

        protected abstract ISyntaxConverter CreateConverterInternal(MethodInvocation param);
        protected abstract bool RewriteCondition(MethodInvocation param);
    }
}
