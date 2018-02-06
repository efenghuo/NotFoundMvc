namespace HttpErrorMvc
{
    using System;
    using System.Web.Mvc;

    internal static class ActionInvokerSelector
    {
        private static readonly Func<IActionInvoker, IActionInvoker> Mvc3Invoker =
           originalActionInvoker => new ActionInvokerWrapper(originalActionInvoker);

        private static readonly Func<IActionInvoker, IActionInvoker> Mvc4Invoker =
            originalActionInvoker => new GlobalErrorAsyncControllerActionInvoker();

        public static Func<IActionInvoker, IActionInvoker> Current { get; } = typeof(Controller).Assembly.GetName().Version.Major <= 3 ? Mvc3Invoker : Mvc4Invoker;
    }
}