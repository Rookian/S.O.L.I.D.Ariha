namespace System.Web.Mvc
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics;
  using System.Globalization;
  using System.Reflection;
  using System.Runtime.CompilerServices;
  using System.Runtime.InteropServices;
  using System.Threading;
  using System.Web.Mvc.Resources;

  /// <summary>Represents a container that is used to pass data between a controller and a view.</summary>
  public class ViewDataDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private readonly Dictionary<string, object> _innerDictionary;
    private object _model;
    private System.Web.Mvc.ModelMetadata _modelMetadata;
    private readonly ModelStateDictionary _modelState;
    private System.Web.Mvc.TemplateInfo _templateMetadata;

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.ViewDataDictionary" /> class.</summary>
    public ViewDataDictionary() : this(null)
    {
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.ViewDataDictionary" /> class by using the specified model.</summary>
    /// <param name="model">The model.</param>
    public ViewDataDictionary(object model)
    {
      this._innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
      this._modelState = new ModelStateDictionary();
      this.Model = model;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Web.Mvc.ViewDataDictionary" /> class by using the specified dictionary.</summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <exception cref="T:System.ArgumentNullException">The <paramref name="dictionary" /> parameter is null.</exception>
    public ViewDataDictionary(ViewDataDictionary dictionary)
    {
      this._innerDictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
      this._modelState = new ModelStateDictionary();
      if (dictionary == null)
      {
        throw new ArgumentNullException("dictionary");
      }
      foreach (KeyValuePair<string, object> pair in dictionary)
      {
        this._innerDictionary.Add(pair.Key, pair.Value);
      }
      foreach (KeyValuePair<string, System.Web.Mvc.ModelState> pair2 in dictionary.ModelState)
      {
        this.ModelState.Add(pair2.Key, pair2.Value);
      }
      this.Model = dictionary.Model;
      this.TemplateInfo = dictionary.TemplateInfo;
      this._modelMetadata = dictionary._modelMetadata;
    }

    /// <summary>Adds the specified item to the collection.</summary>
    /// <param name="item">The object to add to the collection.</param>
    /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
    public void Add(KeyValuePair<string, object> item)
    {
      this._innerDictionary.Add(item);
    }

    /// <summary>Adds an element to the collection using the specified key and value .</summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add.</param>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.IDictionary`2" /> object is read-only.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    /// <exception cref="T:System.ArgumentException">An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2" /> object.</exception>
    public void Add(string key, object value)
    {
      this._innerDictionary.Add(key, value);
    }

    /// <summary>Removes all items from the collection.</summary>
    /// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Collections.Generic.ICollection`1" /> object is read-only.</exception>
    public void Clear()
    {
      this._innerDictionary.Clear();
    }

    /// <summary>Determines whether the collection contains the specified item.</summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.</returns>
    /// <param name="item">The object to locate in the collection.</param>
    public bool Contains(KeyValuePair<string, object> item)
    {
      return this._innerDictionary.Contains(item);
    }

    /// <summary>Determines whether the collection contains an element that has the specified key.</summary>
    /// <returns>true if the collection contains an element that has the specified key; otherwise, false.</returns>
    /// <param name="key">The key of the element to locate in the collection.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    public bool ContainsKey(string key)
    {
      return this._innerDictionary.ContainsKey(key);
    }

    /// <summary>Copies the elements of the collection to an array, starting at a particular index.</summary>
    /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="array" /> is null.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// <paramref name="arrayIndex" /> is less than 0.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="array" /> is multidimensional.-or- <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.-or- The number of elements in the source collection is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.-or- Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      this._innerDictionary.CopyTo(array, arrayIndex);
    }

    /// <summary>Evaluates the specified expression.</summary>
    /// <returns>The results of the evaluation.</returns>
    /// <param name="expression">The expression.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="expression" /> parameter is null or empty.</exception>
    public object Eval(string expression)
    {
      ViewDataInfo viewDataInfo = this.GetViewDataInfo(expression);
      if (viewDataInfo == null)
      {
        return null;
      }
      return viewDataInfo.Value;
    }

    /// <summary>Evaluates the specified expression by using the specified format.</summary>
    /// <returns>The results of the evaluation.</returns>
    /// <param name="expression">The expression.</param>
    /// <param name="format">The format.</param>
    public string Eval(string expression, string format)
    {
      object obj2 = this.Eval(expression);
      if (obj2 == null)
      {
        return string.Empty;
      }
      if (string.IsNullOrEmpty(format))
      {
        return Convert.ToString(obj2, CultureInfo.CurrentCulture);
      }
      return string.Format(CultureInfo.CurrentCulture, format, new object[] { obj2 });
    }

    /// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return this._innerDictionary.GetEnumerator();
    }

    /// <summary>Returns information about the view data as defined by the <paramref name="expression" /> parameter.</summary>
    /// <returns>An object that contains the view data information that is defined by the <paramref name="expression" /> parameter.</returns>
    /// <param name="expression">A set of key/value pairs that define the view-data information to return.</param>
    /// <exception cref="T:System.ArgumentException">The <paramref name="expression" /> parameter is either null or empty.</exception>
    public ViewDataInfo GetViewDataInfo(string expression)
    {
      if (string.IsNullOrEmpty(expression))
      {
        throw new ArgumentException(MvcResources.Common_NullOrEmpty, "expression");
      }
      return ViewDataEvaluator.Eval(this, expression);
    }

    /// <summary>Removes the first occurrence of a specified object from the collection.</summary>
    /// <returns>true if <paramref name="item" /> was successfully removed from the collection; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the collection.</returns>
    /// <param name="item">The object to remove from the collection.</param>
    /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
    public bool Remove(KeyValuePair<string, object> item)
    {
      return this._innerDictionary.Remove(item);
    }

    /// <summary>Removes the element from the collection using the specified key.</summary>
    /// <returns>true if the element is successfully removed; otherwise, false. This method also returns false if <paramref name="key" /> was not found in the original collection.</returns>
    /// <param name="key">The key of the element to remove.</param>
    /// <exception cref="T:System.NotSupportedException">The collection is read-only.</exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    public bool Remove(string key)
    {
      return this._innerDictionary.Remove(key);
    }

    /// <summary>Sets the data model to use for the view.</summary>
    /// <param name="value">The data model to use for the view.</param>
    protected virtual void SetModel(object value)
    {
      this._model = value;
    }

    /// <summary>Returns an enumerator that can be used to iterate through the collection.</summary>
    /// <returns>An enumerator that can be used to iterate through the collection.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this._innerDictionary.GetEnumerator();
    }

    /// <summary>Attempts to retrieve the value that is associated with the specified key.</summary>
    /// <returns>true if the collection contains an element with the specified key; otherwise, false.</returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">When this method returns, the value that is associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="key" /> is null.</exception>
    public bool TryGetValue(string key, out object value)
    {
      return this._innerDictionary.TryGetValue(key, out value);
    }

    /// <summary>Gets the number of elements in the collection.</summary>
    /// <returns>The number of elements in the collection.</returns>
    public int Count
    {
      get
      {
        return this._innerDictionary.Count;
      }
    }

    /// <summary>Gets a value that indicates whether the collection is read-only.</summary>
    /// <returns>true if the collection is read-only; otherwise, false.</returns>
    public bool IsReadOnly
    {
      get
      {
        return this._innerDictionary.IsReadOnly;
      }
    }

    /// <summary>Gets or sets the item that is associated with the specified key.</summary>
    /// <returns>The value of the selected item.</returns>
    /// <param name="key">The key.</param>
    public object this[string key]
    {
      get
      {
        object obj2;
        this._innerDictionary.TryGetValue(key, out obj2);
        return obj2;
      }
      set
      {
        this._innerDictionary[key] = value;
      }
    }

    /// <summary>Gets a collection that contains the keys of this dictionary.</summary>
    /// <returns>A collection that contains the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
    public ICollection<string> Keys
    {
      get
      {
        return this._innerDictionary.Keys;
      }
    }

    /// <summary>Gets or sets the model that is associated with the view data.</summary>
    /// <returns>The model that is associated with the view data.</returns>
    public object Model
    {
      get
      {
        return this._model;
      }
      set
      {
        this._modelMetadata = null;
        this.SetModel(value);
      }
    }

    /// <summary>Gets or sets information about the model.</summary>
    /// <returns>Information about the model.</returns>
    public virtual System.Web.Mvc.ModelMetadata ModelMetadata
    {
      get
      {
        Func<object> modelAccessor = null;
        if ((this._modelMetadata == null) && (this._model != null))
        {
          if (modelAccessor == null)
          {
            modelAccessor = delegate {
              return this._model;
            };
          }
          this._modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(modelAccessor, this._model.GetType());
        }
        return this._modelMetadata;
      }
      set
      {
        this._modelMetadata = value;
      }
    }

    /// <summary>Gets the state of the model.</summary>
    /// <returns>The state of the model.</returns>
    public ModelStateDictionary ModelState
    {
      get
      {
        return this._modelState;
      }
    }

    /// <summary>Gets or sets an object that encapsulates information about the current template context.</summary>
    /// <returns>An object that contains information about the current template.</returns>
    public System.Web.Mvc.TemplateInfo TemplateInfo
    {
      get
      {
        if (this._templateMetadata == null)
        {
          this._templateMetadata = new System.Web.Mvc.TemplateInfo();
        }
        return this._templateMetadata;
      }
      set
      {
        this._templateMetadata = value;
      }
    }

    /// <summary>Gets a collection that contains the values in this dictionary.</summary>
    /// <returns>A collection that contains the values of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.</returns>
    public ICollection<object> Values
    {
      get
      {
        return this._innerDictionary.Values;
      }
    }

    internal static class ViewDataEvaluator
    {
      public static ViewDataInfo Eval(ViewDataDictionary vdd, string expression)
      {
        return EvalComplexExpression(vdd, expression);
      }

      private static ViewDataInfo EvalComplexExpression(object indexableObject, string expression)
      {
        foreach (ExpressionPair pair in GetRightToLeftExpressions(expression))
        {
          string left = pair.Left;
          string right = pair.Right;
          ViewDataInfo propertyValue = GetPropertyValue(indexableObject, left);
          if (propertyValue != null)
          {
            if (string.IsNullOrEmpty(right))
            {
              return propertyValue;
            }
            if (propertyValue.Value != null)
            {
              ViewDataInfo info2 = EvalComplexExpression(propertyValue.Value, right);
              if (info2 != null)
              {
                return info2;
              }
            }
          }
        }
        return null;
      }

      private static ViewDataInfo GetIndexedPropertyValue(object indexableObject, string key)
      {
        IDictionary<string, object> dictionary = indexableObject as IDictionary<string, object>;
        object obj2 = null;
        bool flag = false;
        if (dictionary != null)
        {
          flag = dictionary.TryGetValue(key, out obj2);
        }
        else
        {
          TryGetValueDelegate delegate2 = TypeHelpers.CreateTryGetValueDelegate(indexableObject.GetType());
          if (delegate2 != null)
          {
            flag = delegate2(indexableObject, key, out obj2);
          }
        }
        if (flag)
        {
          ViewDataInfo info = new ViewDataInfo();
          info.Container = indexableObject;
          info.Value = obj2;
          return info;
        }
        return null;
      }

      private static ViewDataInfo GetPropertyValue(object container, string propertyName)
      {
        ViewDataInfo indexedPropertyValue = GetIndexedPropertyValue(container, propertyName);
        if (indexedPropertyValue != null)
        {
          return indexedPropertyValue;
        }
        ViewDataDictionary dictionary = container as ViewDataDictionary;
        if (dictionary != null)
        {
          container = dictionary.Model;
        }
        if (container == null)
        {
          return null;
        }
        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(container).Find(propertyName, true);
        if (descriptor == null)
        {
          return null;
        }
        ViewDataInfo info2 = new ViewDataInfo(delegate {
          return descriptor.GetValue(container);
        });
        info2.Container = container;
        info2.PropertyDescriptor = descriptor;
        return info2;
      }

      private static IEnumerable<ExpressionPair> GetRightToLeftExpressions(string expression)
      {
        <GetRightToLeftExpressions>d__2 d__ = new <GetRightToLeftExpressions>d__2(-2);
        d__.<>3__expression = expression;
        return d__;
      }

      [CompilerGenerated]
      private sealed class <GetRightToLeftExpressions>d__2 : IEnumerable<ViewDataDictionary.ViewDataEvaluator.ExpressionPair>, IEnumerable, IEnumerator<ViewDataDictionary.ViewDataEvaluator.ExpressionPair>, IEnumerator, IDisposable
      {
        private int <>1__state;
        private ViewDataDictionary.ViewDataEvaluator.ExpressionPair <>2__current;
        public string <>3__expression;
        private int <>l__initialThreadId;
        public int <lastDot>5__3;
        public string <postExpression>5__5;
        public string <subExpression>5__4;
        public string expression;

        [DebuggerHidden]
        public <GetRightToLeftExpressions>d__2(int <>1__state)
        {
          this.<>1__state = <>1__state;
          this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
        }

        private bool MoveNext()
        {
          switch (this.<>1__state)
          {
            case 0:
              this.<>1__state = -1;
              this.<>2__current = new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(this.expression, string.Empty);
              this.<>1__state = 1;
              return true;

            case 1:
              this.<>1__state = -1;
              this.<lastDot>5__3 = this.expression.LastIndexOf('.');
              this.<subExpression>5__4 = this.expression;
              this.<postExpression>5__5 = string.Empty;
              break;

            case 2:
              this.<>1__state = -1;
              this.<lastDot>5__3 = this.<subExpression>5__4.LastIndexOf('.');
              break;

            default:
              goto Label_00EB;
          }
          if (this.<lastDot>5__3 > -1)
          {
            this.<subExpression>5__4 = this.expression.Substring(0, this.<lastDot>5__3);
            this.<postExpression>5__5 = this.expression.Substring(this.<lastDot>5__3 + 1);
            this.<>2__current = new ViewDataDictionary.ViewDataEvaluator.ExpressionPair(this.<subExpression>5__4, this.<postExpression>5__5);
            this.<>1__state = 2;
            return true;
          }
        Label_00EB:
          return false;
        }

        [DebuggerHidden]
        IEnumerator<ViewDataDictionary.ViewDataEvaluator.ExpressionPair> IEnumerable<ViewDataDictionary.ViewDataEvaluator.ExpressionPair>.GetEnumerator()
        {
          ViewDataDictionary.ViewDataEvaluator.<GetRightToLeftExpressions>d__2 d__;
          if ((Thread.CurrentThread.ManagedThreadId == this.<>l__initialThreadId) && (this.<>1__state == -2))
          {
            this.<>1__state = 0;
            d__ = this;
          }
          else
          {
            d__ = new ViewDataDictionary.ViewDataEvaluator.<GetRightToLeftExpressions>d__2(0);
          }
          d__.expression = this.<>3__expression;
          return d__;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
          return this.System.Collections.Generic.IEnumerable<System.Web.Mvc.ViewDataDictionary.ViewDataEvaluator.ExpressionPair>.GetEnumerator();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
          throw new NotSupportedException();
        }

        void IDisposable.Dispose()
        {
        }

        ViewDataDictionary.ViewDataEvaluator.ExpressionPair IEnumerator<ViewDataDictionary.ViewDataEvaluator.ExpressionPair>.Current
        {
          [DebuggerHidden]
          get
          {
            return this.<>2__current;
          }
        }

        object IEnumerator.Current
        {
          [DebuggerHidden]
          get
          {
            return this.<>2__current;
          }
        }
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct ExpressionPair
      {
        public readonly string Left;
        public readonly string Right;
        public ExpressionPair(string left, string right)
        {
          this.Left = left;
          this.Right = right;
        }
      }
    }
  }
}
