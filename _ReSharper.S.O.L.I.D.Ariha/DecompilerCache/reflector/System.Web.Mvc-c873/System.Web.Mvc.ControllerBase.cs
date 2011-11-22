namespace System.Web.Mvc
{
  using System;
  using System.Globalization;
  using System.Runtime.CompilerServices;
  using System.Web.Mvc.Async;
  using System.Web.Mvc.Resources;
  using System.Web.Routing;

  /// <summary>Represents the base class for all MVC controllers.</summary>
  public abstract class ControllerBase : IController
  {
    private readonly SingleEntryGate _executeWasCalledGate = new SingleEntryGate();
    private TempDataDictionary _tempDataDictionary;
    private bool _validateRequest = true;
    private IValueProvider _valueProvider;
    private ViewDataDictionary _viewDataDictionary;
    [CompilerGenerated]
    private System.Web.Mvc.ControllerContext <ControllerContext>k__BackingField;

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.ControllerBase" /> class.</summary>
    protected ControllerBase()
    {
    }

    /// <summary>Executes the specified request context.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="requestContext" /> parameter is null.</exception>
    protected virtual void Execute(RequestContext requestContext)
    {
      if (requestContext == null)
      {
        throw new ArgumentNullException("requestContext");
      }
      this.VerifyExecuteCalledOnce();
      this.Initialize(requestContext);
      this.ExecuteCore();
    }

    /// <summary>Executes the request.</summary>
    protected abstract void ExecuteCore();
    /// <summary>Initializes the specified request context.</summary>
    /// <param name="requestContext">The request context.</param>
    protected virtual void Initialize(RequestContext requestContext)
    {
      this.ControllerContext = new System.Web.Mvc.ControllerContext(requestContext, this);
    }

    /// <summary>Executes the specified request context.</summary>
    /// <param name="requestContext">The request context.</param>
    void IController.Execute(RequestContext requestContext)
    {
      this.Execute(requestContext);
    }

    internal void VerifyExecuteCalledOnce()
    {
      if (!this._executeWasCalledGate.TryEnter())
      {
        throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, MvcResources.ControllerBase_CannotHandleMultipleRequests, new object[] { base.GetType() }));
      }
    }

    /// <summary>Gets or sets the controller context.</summary>
    /// <returns>The controller context.</returns>
    public System.Web.Mvc.ControllerContext ControllerContext
    {
      [CompilerGenerated]
      get
      {
        return this.<ControllerContext>k__BackingField;
      }
      [CompilerGenerated]
      set
      {
        this.<ControllerContext>k__BackingField = value;
      }
    }

    /// <summary>Gets or sets the dictionary for temporary data.</summary>
    /// <returns>The dictionary for temporary data.</returns>
    public TempDataDictionary TempData
    {
      get
      {
        if ((this.ControllerContext != null) && this.ControllerContext.IsChildAction)
        {
          return this.ControllerContext.ParentActionViewContext.TempData;
        }
        if (this._tempDataDictionary == null)
        {
          this._tempDataDictionary = new TempDataDictionary();
        }
        return this._tempDataDictionary;
      }
      set
      {
        this._tempDataDictionary = value;
      }
    }

    /// <summary>Gets or sets a value that indicates whether request validation is enabled for this request.</summary>
    /// <returns>true if request validation is enabled for this request; otherwise, false. The default is true.</returns>
    public bool ValidateRequest
    {
      get
      {
        return this._validateRequest;
      }
      set
      {
        this._validateRequest = value;
      }
    }

    /// <summary>Gets or sets the value provider for the controller.</summary>
    /// <returns>The value provider for the controller.</returns>
    public IValueProvider ValueProvider
    {
      get
      {
        if (this._valueProvider == null)
        {
          this._valueProvider = ValueProviderFactories.Factories.GetValueProvider(this.ControllerContext);
        }
        return this._valueProvider;
      }
      set
      {
        this._valueProvider = value;
      }
    }

    /// <summary>Gets or sets the dictionary for view data.</summary>
    /// <returns>The dictionary for the view data.</returns>
    public ViewDataDictionary ViewData
    {
      get
      {
        if (this._viewDataDictionary == null)
        {
          this._viewDataDictionary = new ViewDataDictionary();
        }
        return this._viewDataDictionary;
      }
      set
      {
        this._viewDataDictionary = value;
      }
    }
  }
}
