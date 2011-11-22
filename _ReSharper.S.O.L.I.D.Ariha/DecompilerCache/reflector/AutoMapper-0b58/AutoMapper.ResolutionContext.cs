namespace AutoMapper
{
    using System;
    using System.Collections.Generic;

    public class ResolutionContext : IEquatable<ResolutionContext>
    {
        private readonly int? _arrayIndex;
        private readonly Type _destinationType;
        private readonly object _destinationValue;
        private readonly Dictionary<ResolutionContext, object> _instanceCache;
        private readonly ResolutionContext _parent;
        private readonly AutoMapper.PropertyMap _propertyMap;
        private readonly Type _sourceType;
        private readonly object _sourceValue;
        private readonly AutoMapper.TypeMap _typeMap;

        private ResolutionContext(ResolutionContext context, object sourceValue)
        {
            this._arrayIndex = context._arrayIndex;
            this._typeMap = context._typeMap;
            this._propertyMap = context._propertyMap;
            this._sourceType = context._sourceType;
            this._sourceValue = sourceValue;
            this._destinationValue = context._destinationValue;
            this._parent = context;
            this._destinationType = context._destinationType;
            this._instanceCache = context._instanceCache;
        }

        private ResolutionContext(ResolutionContext context, object sourceValue, Type sourceType)
        {
            this._arrayIndex = context._arrayIndex;
            this._typeMap = context._typeMap;
            this._propertyMap = context._propertyMap;
            this._sourceType = sourceType;
            this._sourceValue = sourceValue;
            this._destinationValue = context._destinationValue;
            this._parent = context;
            this._destinationType = context._destinationType;
            this._instanceCache = context._instanceCache;
        }

        public ResolutionContext(AutoMapper.TypeMap typeMap, object source, Type sourceType, Type destinationType)
            : this(typeMap, source, null, sourceType, destinationType)
        {
        }

        private ResolutionContext(ResolutionContext context, AutoMapper.TypeMap memberTypeMap, object sourceValue,
                                  Type sourceType, Type destinationType)
        {
            this._typeMap = memberTypeMap;
            this._sourceValue = sourceValue;
            this._parent = context;
            if (memberTypeMap != null)
            {
                this._destinationType = memberTypeMap.DestinationType;
                this._sourceType = memberTypeMap.SourceType;
            }
            else
            {
                this._destinationType = destinationType;
                this._sourceType = sourceType;
            }
            this._instanceCache = context._instanceCache;
        }

        private ResolutionContext(ResolutionContext context, object sourceValue, object destinationValue,
                                  AutoMapper.TypeMap memberTypeMap, AutoMapper.PropertyMap propertyMap)
        {
            this._typeMap = memberTypeMap;
            this._propertyMap = propertyMap;
            this._sourceType = memberTypeMap.SourceType;
            this._sourceValue = sourceValue;
            this._destinationValue = destinationValue;
            this._parent = context;
            this._destinationType = memberTypeMap.DestinationType;
            this._instanceCache = context._instanceCache;
        }

        private ResolutionContext(ResolutionContext context, object sourceValue, object destinationValue,
                                  Type sourceType, AutoMapper.PropertyMap propertyMap)
        {
            this._propertyMap = propertyMap;
            this._sourceType = sourceType;
            this._sourceValue = sourceValue;
            this._destinationValue = destinationValue;
            this._parent = context;
            this._destinationType = propertyMap.DestinationProperty.MemberType;
            this._instanceCache = context._instanceCache;
        }

        public ResolutionContext(AutoMapper.TypeMap typeMap, object source, object destination, Type sourceType,
                                 Type destinationType)
        {
            this._typeMap = typeMap;
            this._sourceValue = source;
            this._destinationValue = destination;
            if (typeMap != null)
            {
                this._sourceType = typeMap.SourceType;
                this._destinationType = typeMap.DestinationType;
            }
            else
            {
                this._sourceType = sourceType;
                this._destinationType = destinationType;
            }
            this._instanceCache = new Dictionary<ResolutionContext, object>();
        }

        private ResolutionContext(ResolutionContext context, object sourceValue, AutoMapper.TypeMap typeMap,
                                  Type sourceType, Type destinationType, int arrayIndex)
        {
            this._arrayIndex = new int?(arrayIndex);
            this._typeMap = typeMap;
            this._propertyMap = context._propertyMap;
            this._sourceType = sourceType;
            this._sourceValue = sourceValue;
            this._parent = context;
            this._destinationType = destinationType;
            this._instanceCache = context._instanceCache;
        }

        public int? ArrayIndex
        {
            get { return this._arrayIndex; }
        }

        public Type DestinationType
        {
            get { return this._destinationType; }
        }

        public object DestinationValue
        {
            get { return this._destinationValue; }
        }

        public Dictionary<ResolutionContext, object> InstanceCache
        {
            get { return this._instanceCache; }
        }

        public bool IsSourceValueNull
        {
            get { return object.Equals(null, this._sourceValue); }
        }

        public string MemberName
        {
            get
            {
                if (this._propertyMap == null)
                {
                    return string.Empty;
                }
                if (this._arrayIndex.HasValue)
                {
                    return (this._propertyMap.DestinationProperty.Name + this._arrayIndex.Value);
                }
                return this._propertyMap.DestinationProperty.Name;
            }
        }

        public ResolutionContext Parent
        {
            get { return this._parent; }
        }

        public AutoMapper.PropertyMap PropertyMap
        {
            get { return this._propertyMap; }
        }

        public Type SourceType
        {
            get { return this._sourceType; }
        }

        public object SourceValue
        {
            get { return this._sourceValue; }
        }

        public AutoMapper.TypeMap TypeMap
        {
            get { return this._typeMap; }
        }

        #region IEquatable<ResolutionContext> Members

        public bool Equals(ResolutionContext other)
        {
            if (object.ReferenceEquals(null, other))
            {
                return false;
            }
            return (object.ReferenceEquals(this, other) ||
                    (((object.Equals(other._typeMap, this._typeMap) &&
                       object.Equals(other._sourceType, this._sourceType)) &&
                      object.Equals(other._destinationType, this._destinationType)) &&
                     object.Equals(other._sourceValue, this._sourceValue)));
        }

        #endregion

        public ResolutionContext CreateElementContext(AutoMapper.TypeMap elementTypeMap, object item,
                                                      Type sourceElementType, Type destinationElementType,
                                                      int arrayIndex)
        {
            return new ResolutionContext(this, item, elementTypeMap, sourceElementType, destinationElementType,
                                         arrayIndex);
        }

        public ResolutionContext CreateMemberContext(AutoMapper.TypeMap memberTypeMap, object memberValue,
                                                     object destinationValue, Type sourceMemberType,
                                                     AutoMapper.PropertyMap propertyMap)
        {
            if (memberTypeMap == null)
            {
                return new ResolutionContext(this, memberValue, destinationValue, sourceMemberType, propertyMap);
            }
            return new ResolutionContext(this, memberValue, destinationValue, memberTypeMap, propertyMap);
        }

        public ResolutionContext CreateTypeContext(AutoMapper.TypeMap memberTypeMap, object sourceValue, Type sourceType,
                                                   Type destinationType)
        {
            return new ResolutionContext(this, memberTypeMap, sourceValue, sourceType, destinationType);
        }

        public ResolutionContext CreateValueContext(object sourceValue)
        {
            return new ResolutionContext(this, sourceValue);
        }

        public ResolutionContext CreateValueContext(object sourceValue, Type sourceType)
        {
            return new ResolutionContext(this, sourceValue, sourceType);
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != typeof (ResolutionContext))
            {
                return false;
            }
            return this.Equals((ResolutionContext) obj);
        }

        public AutoMapper.PropertyMap GetContextPropertyMap()
        {
            AutoMapper.PropertyMap map = this._propertyMap;
            for (ResolutionContext context = this._parent;
                 (map == null) && (context != null);
                 context = context._parent)
            {
                map = context._propertyMap;
            }
            return map;
        }

        public AutoMapper.TypeMap GetContextTypeMap()
        {
            AutoMapper.TypeMap map = this._typeMap;
            for (ResolutionContext context = this._parent;
                 (map == null) && (context != null);
                 context = context._parent)
            {
                map = context._typeMap;
            }
            return map;
        }

        public override int GetHashCode()
        {
            int num = (this._typeMap != null) ? this._typeMap.GetHashCode() : 0;
            num = (num*0x18d) ^ ((this._sourceType != null) ? this._sourceType.GetHashCode() : 0);
            num = (num*0x18d) ^ ((this._destinationType != null) ? this._destinationType.GetHashCode() : 0);
            num = (num*0x18d) ^ (this._arrayIndex.HasValue ? this._arrayIndex.Value : 0);
            return ((num*0x18d) ^ ((this._sourceValue != null) ? this._sourceValue.GetHashCode() : 0));
        }

        public static ResolutionContext New<TSource>(TSource sourceValue)
        {
            return new ResolutionContext(null, sourceValue, typeof (TSource), null);
        }

        public override string ToString()
        {
            return string.Format("Trying to map {0} to {1}.", this.SourceType.Name, this.DestinationType.Name);
        }
    }
}