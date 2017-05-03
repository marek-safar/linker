﻿using Mono.Linker.Tests.Cases.Expectations.Assertions;

namespace Mono.Linker.Tests.Cases.Generics {
	class MethodWithParameterWhichHasGenericParametersAndOverrideUsesADifferentNameForGenericParameterNestedCase2 {
		public static void Main (string [] args)
		{
			Derived<int, int> tmp = new Derived<int, int> ();
			tmp.Method<int> (null);
		}

		[Kept]
		public class Base<TSource> {

			[KeptMember (".ctor()")]
			public abstract class Nested<T1, T2, T3> {
				[Kept]
				public abstract TResult1 Method<TResult1> (System.Func<TSource, TResult1> arg);
			}
		}

		[KeptMember (".ctor()")]
		[KeptBaseType ("Mono.Linker.Tests.Cases.Generics.MethodWithParameterWhichHasGenericParametersAndOverrideUsesADifferentNameForGenericParameterNestedCase2/Base`1/Nested`3<TResult1,System.Int32,System.Int32,System.String>")]
		public class Derived<TSource, TResult1> : Base<TResult1>.Nested<int, int, string> {
			[Kept]
			public override TResult2 Method<TResult2> (System.Func<TResult1, TResult2> arg)
			{
				return arg (default (TResult1));
			}
		}
	}
}