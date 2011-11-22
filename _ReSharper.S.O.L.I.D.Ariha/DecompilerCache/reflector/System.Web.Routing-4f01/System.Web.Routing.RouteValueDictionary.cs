namespace System.Web.Routing
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Reflection;
  using System.Runtime.InteropServices;
  using System.Security.Permissions;
  using System.Web;

  /// <summary>
  /// Represents a case-insensitive collection of key/value pairs that are used in various places within the routing framework, such as when defining the default values for a route or when generating a URL that is based on a route.
  /// </summary>
  [AspNetHostingPermission(SecurityAction.InheritanceDemand, Level=AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.LinkDemand, Level=AspNetHostingPermissionLevel.Minimal)]
  public class RouteValueDictionary : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
  {
    private Dictionary<string, object> _dictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class that is empty. 
    /// </summary>
    public RouteValueDictionary()
    {
      this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds elements from the specified collection. 
    /// </summary>
    /// <param name="dictionary">
    /// A collection whose elements are copied to the new collection.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="dictionary" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="dictionary" /> contains one or more duplicate keys.
    /// </exception>
    public RouteValueDictionary(IDictionary<string, object> dictionary)
    {
      this._dictionary = new Dictionary<string, object>(dictionary, StringComparer.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Web.Routing.RouteValueDictionary" /> class and adds values based on properties from the specified object. 
    /// </summary>
    /// <param name="values">
    /// An object that contains properties that will be added as elements to the new collection.
    /// </param>
    public RouteValueDictionary(object values)
    {
      this._dictionary = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
      this.AddValues(values);
    }

    /// <summary>
    /// Adds the specified value to the dictionary by using the specified key.
    /// </summary>
    /// <param name="key">
    /// The key of the element to add.
    /// </param>
    /// <param name="value">
    /// The value of the element to add.
    /// </param>
    public void Add(string key, object value)
    {
      this._dictionary.Add(key, value);
    }

    private void AddValues(object values)
    {
      if (values != null)
      {
        foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(values))
        {
          object obj2 = descriptor.GetValue(values);
          this.Add(descriptor.Name, obj2);
        }
      }
    }

    /// <summary>
    /// Removes all keys and values from the dictionary.
    /// </summary>
    public void Clear()
    {
      this._dictionary.Clear();
    }

    /// <summary>
    /// Determines whether the dictionary contains the specified key.
    /// </summary>
    /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">
    /// The key to locate in the dictionary.
    /// </param>
    public bool ContainsKey(string key)
    {
      return this._dictionary.ContainsKey(key);
    }

    /// <summary>
    /// Determines whether the dictionary contains a specific value.
    /// </summary>
    /// <returns>true if the dictionary contains an element with the specified value; otherwise, false.
    /// </returns>
    /// <param name="value">
    /// The value to locate in the dictionary.
    /// </param>
    public bool ContainsValue(object value)
    {
      return this._dictionary.ContainsValue(value);
    }

    /// <summary>
    /// Returns an enumerator that you can use to iterate through the dictionary.
    /// </summary>
    /// <returns>
    /// A structure for reading data in the dictionary.
    /// </returns>
    public Dictionary<string, object>.Enumerator GetEnumerator()
    {
      return this._dictionary.GetEnumerator();
    }

    /// <summary>
    /// Removes the value that has the specified key from the dictionary.
    /// </summary>
    /// <returns>true if the element is found and removed; otherwise, false. This method returns false if <paramref name="key" /> is not found in the dictionary.
    /// </returns>
    /// <param name="key">
    /// The key of the element to remove.
    /// </param>
    public bool Remove(string key)
    {
      return this._dictionary.Remove(key);
    }

    void ICollection<KeyValuePair<string, object>>.Add(KeyValuePair<string, object> item)
    {
      this._dictionary.Add(item);
    }

    bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
    {
      return this._dictionary.Contains(item);
    }

    void ICollection<KeyValuePair<string, object>>.CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      this._dictionary.CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
    {
      return this._dictionary.Remove(item);
    }

    IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that you can use to iterate through the dictionary.
    /// </summary>
    /// <returns>
    /// A structure for reading data in the dictionary.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    /// <summary>
    /// Gets a value that indicates whether a value is associated with the specified key.
    /// </summary>
    /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">
    /// The key of the value to get.
    /// </param>
    /// <param name="value">
    /// When this method returns, contains the value that is associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
    /// </param>
    public bool TryGetValue(string key, out object value)
    {
      return this._dictionary.TryGetValue(key, out value);
    }

    /// <summary>
    /// Gets the number of key/value pairs that are in the collection.
    /// </summary>
    /// <returns>
    /// The number of key/value pairs that are in the collection.
    /// </returns>
    public int Count
    {
      get
      {
        return this._dictionary.Count;
      }
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <returns>
    /// The value that is associated with the specified key.
    /// </returns>
    /// <param name="key">
    /// The key of the value to get or set.
    /// </param>
    public object this[string key]
    {
      get
      {
        object obj2;
        this.TryGetValue(key, out obj2);
        return obj2;
      }
      set
      {
        this._dictionary[key] = value;
      }
    }

    /// <summary>
    /// Gets a collection that contains the keys in the dictionary.
    /// </summary>
    /// <returns>
    /// A collection that contains the keys in the dictionary.
    /// </returns>
    public Dictionary<string, object>.KeyCollection Keys
    {
      get
      {
        return this._dictionary.Keys;
      }
    }

    bool ICollection<KeyValuePair<string, object>>.IsReadOnly
    {
      get
      {
        return this._dictionary.IsReadOnly;
      }
    }

    ICollection<string> IDictionary<string, object>.Keys
    {
      get
      {
        return this._dictionary.Keys;
      }
    }

    ICollection<object> IDictionary<string, object>.Values
    {
      get
      {
        return this._dictionary.Values;
      }
    }

    /// <summary>
    /// Gets a collection that contains the values in the dictionary.
    /// </summary>
    /// <returns>
    /// A collection that that contains the values in the dictionary.
    /// </returns>
    public Dictionary<string, object>.ValueCollection Values
    {
      get
      {
        return this._dictionary.Values;
      }
    }
  }
}
