using System;
using System.Runtime.Serialization;
using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.BCLFeatures
{
	public class ISerializableCtorsRemoved
	{
		public static void Main ()
		{
			C c = new C ();

			object o = null;
			if (o is ISerializable)
				return;
		}

		[Kept]
		[KeptInterface (typeof (ISerializable))]
		class C : ISerializable
		{
			[Kept]
			public C ()
			{
			}

			public C (SerializationInfo info, StreamingContext context)
			{
			}

			[Kept]
			public void GetObjectData (SerializationInfo info, StreamingContext context)
			{
				throw new NotImplementedException ();
			}
		}
	}
}
