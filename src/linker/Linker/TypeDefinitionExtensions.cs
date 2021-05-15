using System;
using Mono.Cecil;

namespace Mono.Linker
{
	public static class TypeDefinitionExtensions
	{
		public static TypeReference GetEnumUnderlyingType (this TypeDefinition enumType)
		{
			foreach (var field in enumType.Fields) {
				if (!field.IsStatic) {
					return field.FieldType;
				}
			}

			throw new MissingFieldException ($"Enum type '{enumType.FullName}' is missing instance field");
		}

		public static bool IsMulticastDelegate (this TypeDefinition td)
		{
			return td.BaseType?.Name == "MulticastDelegate" && td.BaseType.Namespace == "System";
		}

		public static bool IsSerializable (this TypeDefinition td)
		{
			return (td.Attributes & TypeAttributes.Serializable) != 0;
		}

		public static bool IsCompilerGenerated (this TypeDefinition td)
		{
			if (!td.HasCustomAttributes)
				return false;

			foreach (var ca in td.CustomAttributes) {
				var caType = ca.AttributeType;
				if (caType.Name == "CompilerGeneratedAttribute" && caType.Namespace == "System.Runtime.CompilerServices")
					return true;
			}

			return false;
		}
	}
}