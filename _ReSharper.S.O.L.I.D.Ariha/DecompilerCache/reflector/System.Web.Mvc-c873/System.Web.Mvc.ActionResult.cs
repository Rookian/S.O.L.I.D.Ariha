namespace System.Web.Mvc
{
  using System;

  /// <summary>Encapsulates the result of an action method and is used to perform a framework-level operation on behalf of the action method.</summary>
  public abstract class ActionResult
  {
    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.ActionResult" /> class.</summary>
    protected ActionResult()
    {
    }

    /// <summary>Enables processing of the result of an action method by a custom type that inherits from the <see cref="T:System.Web.Mvc.ActionResult" /> class.</summary>
    /// <param name="context">The context in which the result is executed. The context information includes the controller, HTTP content, request context, and route data.</param>
    public abstract void ExecuteResult(ControllerContext context);
  }
}
