// Licensed to the.NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Mono.Linker
{
	public class TypeHierarchy
	{
		[Flags]
		public enum TypeAlias
		{
			SystemType = 1,
			SystemReflectionIReflect = 1 << 1,
			SystemISerializable = 1 << 2,

			All = SystemType | SystemISerializable | SystemReflectionIReflect
		}

		readonly Dictionary<TypeDefinition, TypeAlias> _cache = new Dictionary<TypeDefinition, TypeAlias> ();

		TypeAlias GetDescription (TypeDefinition type)
		{
			if (_cache.TryGetValue (type, out var desc))
				return desc;

			if (type.Name == "IReflect" && type.Namespace == "System.Reflection") {
				desc |= TypeAlias.SystemReflectionIReflect;
			}

			if (type.Name == "Type" && type.Namespace == "System") {
				desc |= TypeAlias.SystemType;
			}

			if (type.HasInterfaces) {
				foreach (var iface in type.Interfaces) {
					if (iface.InterfaceType.Name == "IReflect" && iface.InterfaceType.Namespace == "System.Reflection") {
						desc |= TypeAlias.SystemReflectionIReflect;
						continue;
					}

					if (iface.InterfaceType.Name == "ISerializable" && iface.InterfaceType.Namespace == "System.Runtime.Serialization") {
						desc |= TypeAlias.SystemISerializable;
						continue;
					}
				}
			}

			if (desc != TypeAlias.All) {
				TypeDefinition baseType = type.BaseType?.Resolve ();
				if (baseType != null)
					desc |= GetDescription (baseType);
			}

			_cache.Add (type, desc);
			return desc;
		}

		public bool ContainsType (TypeDefinition type, TypeAlias types)
		{
			return (GetDescription (type) & types) != 0;
		}

		public bool ContainsType (TypeReference type, TypeAlias types)
		{
			TypeDefinition td = type.Resolve ();
			if (td == null)
				return false;

			return ContainsType (td, types);
		}
	}
}
