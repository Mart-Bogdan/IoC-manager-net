using System;
using System.Reflection;

namespace Innahema.Ioc.Manager.Windsor.Utils
{
    /// <summary>
    /// Створює обгортку, яка дозволяє конструювати дженерік методи підчас використання.
    /// Такий виклик не дуже швидкий, і якщо потрібна швидкість, скористайтесь лібою https://github.com/Mart-Bogdan/Invokator
    /// В даному випадку метод буде викликатись лише один раз.
    /// </summary>
    internal class GenericMethodWrapper
    {
        /// <summary>
        /// Gnenric method rederence to be able to create MethodRef at run-time, knowing Type
        /// </summary>
        private readonly MethodInfo _genericMethodInfo;

        public GenericMethodWrapper(Delegate methodDelegate)
        {
            var methodInfo = methodDelegate.Method;

            _genericMethodInfo = methodInfo.GetGenericMethodDefinition();
        }

        public MethodInfo MakeGenericMethod(Type tArgument)
        {
            return _genericMethodInfo.MakeGenericMethod(tArgument);
        }
    }
}
