using System;
using System.Runtime.Serialization;
using Mono.Linker.Tests.Cases.Expectations.Assertions;
using Mono.Linker.Tests.Cases.Expectations.Metadata;

namespace Mono.Linker.Tests.Cases.BCLFeatures
{
	[Reference ("System.Runtime.Serialization.Formatters.dll")]
	public class ISerializableCtors
	{
		public static void Main ()
		{
			new System.Runtime.Serialization.ObjectManager (null, new StreamingContext ());
			
			object c = new C ();
			object cc = new CC ();
			Type d = typeof (D);
		}

		[Kept]
		[KeptInterface (typeof (ISerializable))]
		class C : ISerializable
		{
			[Kept]
			public C ()
			{
			}

			[Kept]
			private C (SerializationInfo info, StreamingContext context)
			{
			}

			[Kept] // Because of BCL dependency
			public void GetObjectData (SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException ();
			}
		}

		[Kept]
		[KeptBaseType (typeof (C))]
		class CC : C
		{
			[Kept]
			public CC ()
			{
			}

			[Kept]
			private CC (SerializationInfo info, StreamingContext context)
			{
			}
		}

		[Kept]
		[KeptInterface (typeof (ISerializable))]
		class D : ISerializable
		{
			[Kept]
			private D (SerializationInfo info, StreamingContext context)
			{
			}

			public void Foo ()
			{
			}

			[Kept] // Because of BCL dependency
			public void GetObjectData (SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException ();
			}
		}
	}
}
