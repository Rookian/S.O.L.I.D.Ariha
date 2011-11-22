namespace NHibernate
{
    using NHibernate.Collection;
    using NHibernate.Impl;
    using NHibernate.Intercept;
    using NHibernate.Proxy;
    using NHibernate.Type;
    using NHibernate.UserTypes;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Provides access to the full range of NHibernate built-in types.
    /// IType instances may be used to bind values to query parameters.
    /// Also a factory for new Blobs and Clobs.
    /// </summary>
    public static class NHibernateUtil
    {
        public static readonly NullableType AnsiChar = new AnsiCharType();

        /// <summary>
        /// NHibernate Ansi String type
        /// </summary>
        public static readonly NullableType AnsiString = new AnsiStringType();

        /// <summary>
        /// NHibernate binary type
        /// </summary>
        public static readonly NullableType Binary = new BinaryType();

        /// <summary>
        /// NHibernate binary blob type
        /// </summary>
        public static readonly NullableType BinaryBlob = new BinaryBlobType();

        /// <summary>
        /// NHibernate boolean type
        /// </summary>
        public static readonly NullableType Boolean = new BooleanType();

        /// <summary>
        /// NHibernate byte type
        /// </summary>
        public static readonly NullableType Byte = new ByteType();

        /// <summary>
        /// NHibernate character type
        /// </summary>
        public static readonly NullableType Character = new CharType();

        /// <summary>
        /// NHibernate class type
        /// </summary>
        public static readonly NullableType Class = new TypeType();

        /// <summary>
        /// NHibernate class meta type for association of kind <code>any</code>.
        /// </summary>
        /// <seealso cref="T:NHibernate.Type.AnyType" />
        public static readonly IType ClassMetaType = new NHibernate.Type.ClassMetaType();

        private static readonly Dictionary<Type, IType> clrTypeToNHibernateType = new Dictionary<Type, IType>();

        /// <summary>
        /// NHibernate Culture Info type
        /// </summary>
        public static readonly NullableType CultureInfo = new CultureInfoType();

        /// <summary>
        /// NHibernate date type
        /// </summary>
        public static readonly NullableType Date = new DateType();

        /// <summary>
        /// NHibernate date type
        /// </summary>
        public static readonly NullableType DateTime = new DateTimeType();

        /// <summary>
        /// NHibernate decimal type
        /// </summary>
        public static readonly NullableType Decimal = new DecimalType();

        /// <summary>
        /// NHibernate double type
        /// </summary>
        public static readonly NullableType Double = new DoubleType();

        /// <summary>
        /// NHibernate Guid type.
        /// </summary>
        public static readonly NullableType Guid = new GuidType();

        /// <summary>
        /// NHibernate System.Int16 (short in C#) type
        /// </summary>
        public static readonly NullableType Int16 = new Int16Type();

        /// <summary>
        /// NHibernate System.Int32 (int in C#) type
        /// </summary>
        public static readonly NullableType Int32 = new Int32Type();

        /// <summary>
        /// NHibernate System.Int64 (long in C#) type
        /// </summary>
        public static readonly NullableType Int64 = new Int64Type();

        /// <summary>
        /// NHibernate System.Object type
        /// </summary>
        public static readonly IType Object = new AnyType();

        /// <summary>
        /// NHibernate System.SByte type
        /// </summary>
        public static readonly NullableType SByte = new SByteType();

        /// <summary>
        /// NHibernate serializable type
        /// </summary>
        public static readonly NullableType Serializable = new SerializableType();

        /// <summary>
        /// NHibernate System.Single (float in C#) Type
        /// </summary>
        public static readonly NullableType Single = new SingleType();

        /// <summary>
        /// NHibernate String type
        /// </summary>
        public static readonly NullableType String = new StringType();

        /// <summary>
        /// NHibernate string clob type
        /// </summary>
        public static readonly NullableType StringClob = new StringClobType();

        /// <summary>
        /// NHibernate Ticks type
        /// </summary>
        public static readonly NullableType Ticks = new TicksType();

        /// <summary>
        /// NHibernate Time type
        /// </summary>
        public static readonly NullableType Time = new TimeType();

        /// <summary>
        /// NHibernate Ticks type
        /// </summary>
        public static readonly NullableType TimeSpan = new TimeSpanType();

        /// <summary>
        /// NHibernate Timestamp type
        /// </summary>
        public static readonly NullableType Timestamp = new TimestampType();

        /// <summary>
        /// NHibernate TrueFalse type
        /// </summary>
        public static readonly NullableType TrueFalse = new TrueFalseType();

        /// <summary>
        /// NHibernate System.UInt16 (ushort in C#) type
        /// </summary>
        public static readonly NullableType UInt16 = new UInt16Type();

        /// <summary>
        /// NHibernate System.UInt32 (uint in C#) type
        /// </summary>
        public static readonly NullableType UInt32 = new UInt32Type();

        /// <summary>
        /// NHibernate System.UInt64 (ulong in C#) type
        /// </summary>
        public static readonly NullableType UInt64 = new UInt64Type();

        /// <summary>
        /// NHibernate YesNo type
        /// </summary>
        public static readonly NullableType YesNo = new YesNoType();

        static NHibernateUtil()
        {
            FieldInfo[] fields = typeof (NHibernateUtil).GetFields();
            foreach (FieldInfo info in fields)
            {
                if (typeof (IType).IsAssignableFrom(info.FieldType))
                {
                    IType type = (IType) info.GetValue(null);
                    clrTypeToNHibernateType[type.ReturnedClass] = type;
                }
            }
        }

        /// <summary>
        /// A NHibernate serializable type
        /// </summary>
        /// <param name="metaType">a type mapping <see cref="T:NHibernate.Type.IType" /> to a single column</param>
        /// <param name="identifierType">the entity identifier type</param>
        /// <returns></returns>
        public static IType Any(IType metaType, IType identifierType)
        {
            return new AnyType(metaType, identifierType);
        }

        /// <summary>
        /// A NHibernate persistent object (entity) type
        /// </summary>
        /// <param name="persistentClass">a mapped entity class</param>
        /// <returns></returns>
        [Obsolete("use NHibernate.Entity instead")]
        public static IType Association(Type persistentClass)
        {
            return new ManyToOneType(persistentClass.FullName);
        }

        /// <summary>
        /// Close an <see cref="T:System.Collections.IEnumerable" /> returned by NHibernate immediately,
        /// instead of waiting until the session is closed or disconnected.
        /// </summary>
        public static void Close(IEnumerable enumerable)
        {
            EnumerableImpl impl = enumerable as EnumerableImpl;
            if (impl == null)
            {
                throw new ArgumentException("Not a NHibernate enumerable", "enumerable");
            }
            impl.Dispose();
        }

        /// <summary>
        /// Close an <see cref="T:System.Collections.IEnumerator" /> obtained from an <see cref="T:System.Collections.IEnumerable" />
        /// returned by NHibernate immediately, instead of waiting until the session is
        /// closed or disconnected.
        /// </summary>
        public static void Close(IEnumerator enumerator)
        {
            EnumerableImpl impl = enumerator as EnumerableImpl;
            if (impl == null)
            {
                throw new ArgumentException("Not a NHibernate enumerator", "enumerator");
            }
            impl.Dispose();
        }

        /// <summary>
        /// A NHibernate custom type
        /// </summary>
        /// <param name="userTypeClass">a class that implements UserType</param>
        /// <returns></returns>
        public static IType Custom(Type userTypeClass)
        {
            if (typeof (ICompositeUserType).IsAssignableFrom(userTypeClass))
            {
                return new CompositeCustomType(userTypeClass, null);
            }
            return new CustomType(userTypeClass, null);
        }

        /// <summary> A Hibernate persistent object (entity) type. </summary>
        /// <param name="entityName">a mapped entity class </param>
        public static IType Entity(string entityName)
        {
            return new ManyToOneType(entityName);
        }

        /// <summary>
        /// A NHibernate persistent object (entity) type
        /// </summary>
        /// <param name="persistentClass">a mapped entity class</param>
        /// <returns></returns>
        public static IType Entity(Type persistentClass)
        {
            return new ManyToOneType(persistentClass.FullName);
        }

        /// <summary>
        /// A NHibernate persistent enum type
        /// </summary>
        /// <param name="enumClass"></param>
        /// <returns></returns>
        public static IType Enum(Type enumClass)
        {
            return new PersistentEnumType(enumClass);
        }

        /// <summary>
        /// Get the true, underlying class of a proxied persistent class. This operation
        /// will initialize a proxy by side-effect.
        /// </summary>
        /// <param name="proxy">a persistable object or proxy</param>
        /// <returns>the true class of the instance</returns>
        public static Type GetClass(object proxy)
        {
            if (proxy is INHibernateProxy)
            {
                return ((INHibernateProxy) proxy).HibernateLazyInitializer.GetImplementation().GetType();
            }
            return proxy.GetType();
        }

        /// <summary>
        /// A NHibernate serializable type
        /// </summary>
        /// <param name="serializableClass"></param>
        /// <returns></returns>
        public static IType GetSerializable(Type serializableClass)
        {
            return new SerializableType(serializableClass);
        }

        /// <summary>
        /// Guesses the IType of this object
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static IType GuessType(object obj)
        {
            Type type = (obj as Type) ?? obj.GetType();
            return GuessType(type);
        }

        /// <summary>
        /// Guesses the IType by the type
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static IType GuessType(Type type)
        {
            if (type.FullName.StartsWith(typeof (Nullable<>).FullName))
            {
                type = type.GetGenericArguments()[0];
            }
            if (clrTypeToNHibernateType.ContainsKey(type))
            {
                return clrTypeToNHibernateType[type];
            }
            if (type.IsEnum)
            {
                return Enum(type);
            }
            if (typeof (IUserType).IsAssignableFrom(type) || typeof (ICompositeUserType).IsAssignableFrom(type))
            {
                return Custom(type);
            }
            return Entity(type);
        }

        /// <summary>
        /// Force initialization of a proxy or persistent collection.
        /// </summary>
        /// <param name="proxy">a persistable object, proxy, persistent collection or null</param>
        /// <exception cref="T:NHibernate.HibernateException">if we can't initialize the proxy at this time, eg. the Session was closed</exception>
        public static void Initialize(object proxy)
        {
            if (proxy != null)
            {
                if (proxy is INHibernateProxy)
                {
                    ((INHibernateProxy) proxy).HibernateLazyInitializer.Initialize();
                }
                else if (proxy is IPersistentCollection)
                {
                    ((IPersistentCollection) proxy).ForceInitialization();
                }
            }
        }

        /// <summary>
        /// Is the proxy or persistent collection initialized?
        /// </summary>
        /// <param name="proxy">a persistable object, proxy, persistent collection or null</param>
        /// <returns>true if the argument is already initialized, or is not a proxy or collection</returns>
        public static bool IsInitialized(object proxy)
        {
            if (proxy is INHibernateProxy)
            {
                return !((INHibernateProxy) proxy).HibernateLazyInitializer.IsUninitialized;
            }
            if (proxy is IPersistentCollection)
            {
                return ((IPersistentCollection) proxy).WasInitialized;
            }
            return true;
        }

        /// <summary> 
        /// Check if the property is initialized. If the named property does not exist
        /// or is not persistent, this method always returns <tt>true</tt>. 
        /// </summary>
        /// <param name="proxy">The potential proxy </param>
        /// <param name="propertyName">the name of a persistent attribute of the object </param>
        /// <returns> 
        /// true if the named property of the object is not listed as uninitialized;
        /// false if the object is an uninitialized proxy, or the named property is uninitialized 
        /// </returns>
        public static bool IsPropertyInitialized(object proxy, string propertyName)
        {
            object implementation;
            if (proxy is INHibernateProxy)
            {
                ILazyInitializer hibernateLazyInitializer = ((INHibernateProxy) proxy).HibernateLazyInitializer;
                if (hibernateLazyInitializer.IsUninitialized)
                {
                    return false;
                }
                implementation = hibernateLazyInitializer.GetImplementation();
            }
            else
            {
                implementation = proxy;
            }
            if (FieldInterceptionHelper.IsInstrumented(implementation))
            {
                IFieldInterceptor interceptor = FieldInterceptionHelper.ExtractFieldInterceptor(implementation);
                return ((interceptor == null) || interceptor.IsInitializedField(propertyName));
            }
            return true;
        }
    }
}